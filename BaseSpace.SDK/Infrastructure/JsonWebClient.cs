using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Util;
using Common.Logging;
using Illumina.BaseSpace.SDK.Types;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceModel.Serialization;
using ServiceStack.Text;

namespace Illumina.BaseSpace.SDK
{
    public partial class JsonWebClient
    {
        protected JsonServiceClient Client;
        public IRequestOptions DefaultRequestOptions { get; protected set; }
        protected ILog Logger = LogManager.GetCurrentClassLogger();
        private const int CONNECTION_COUNT = 8; //TODO: Is this the right place?

        [ThreadStatic]
        protected static IRequestOptions CurrentRequestOptions = null; //available per thread

        static JsonWebClient()
        {
            ChangeSerializationOptions();
        }

        public JsonWebClient(IClientSettings settings, IRequestOptions defaultOptions = null)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }
            Settings = settings;
            // call something on this object so it gets initialized in single threaded context
            HttpEncoder.Default.SerializeToString();
            HttpEncoder.Current.SerializeToString();
            if (defaultOptions == null)
            {
                defaultOptions = new RequestOptions(settings.BaseSpaceApiUrl);
            }
           
            SetDefaultRequestOptions(defaultOptions);

            Client = new JsonServiceClient(DefaultRequestOptions.BaseUrl);
            Client.LocalHttpWebRequestFilter += WebRequestFilter;
        }

        private void WebRequestFilter(HttpWebRequest req)
        {
	        Settings.Authentication.UpdateHttpHeader(req, CurrentRequestOptions);


        }

        protected IClientSettings Settings { get; set; }

        protected static void ChangeSerializationOptions()
        {
            // setting this just to make sure it's not set in Linux
            JsonDataContractDeserializer.Instance.UseBcl = false;
            // BaseSpace uses this format for DateTime
            JsConfig.DateHandler = JsonDateHandler.ISO8601;

            JsConfig<Uri>.DeSerializeFn = ParseUri;
            //handle complex parsing of references
            JsConfig<IContentReference<IAbstractResource>>.RawDeserializeFn = ResourceDeserializer;
        }

        private static Uri ParseUri(string s)
        {
            UriKind uriKind = (s.StartsWith("http") || s.StartsWith("https")) ? UriKind.Absolute : UriKind.Relative;
            return new Uri(s, uriKind);
        }

        internal static Task<TResult> ExecuteTask<TResult>(JsonServiceClient client, RestRequest request, ILog log)
            where TResult : class
        {
            var tcs = new TaskCompletionSource<TResult>();
            WaitCallback asyncWork = _ => Execute(client, request, log, tcs);

            ThreadPool.QueueUserWorkItem(asyncWork);

            return tcs.Task;
        }

        internal static void Execute<TResult>(JsonServiceClient client, RestRequest request, ILog log,
                                              TaskCompletionSource<TResult> tcs)
            where TResult : class
        {
            try
            {
                tcs.SetResult(Execute<TResult>(client, request, log));
            }
            catch (Exception e)
            {
                tcs.SetException(e);
            }
            finally
            {
                CurrentRequestOptions = null;
            }
        }

        internal static TResult Execute<TResult>(JsonServiceClient client, RestRequest request, ILog log)
            where TResult : class
        {
            client.BaseUri = request.Options.BaseUrl.TrimEnd('/') + "/";  //make sure we only have a single slash on end always
            

            var fileRestRequest = request as FileRestRequest;
            if (fileRestRequest != null)
            {
                Func<TResult> funcFile = () => client.PostFileWithRequest<TResult>(fileRestRequest.RelativeOrAbsoluteUrl,
                                                                                   fileRestRequest.FileInfo,
                                                                                   fileRestRequest.Request);

                return WrapResult(funcFile, log, fileRestRequest.Options.RetryAttempts, fileRestRequest.Name);
            }
            var restRequest = request as StreamingRestRequest;
            if (restRequest != null)
            {
                var sr = restRequest;

                Func<TResult> funcStream =
                    () => client.PostFileWithRequest<TResult>(restRequest.RelativeOrAbsoluteUrl, sr.Stream,
                                                              sr.FileName,
                                                              restRequest.Request);

                return WrapResult(funcStream, log, restRequest.Options.RetryAttempts, restRequest.Name);
            }

            Func<TResult> func =
                () =>
                    {
                        CurrentRequestOptions = request.Options;  //we need to set the default options here
                        return client.Send<TResult>(request.Method.ToString(), request.RelativeOrAbsoluteUrl, request.Request);
                    };

            return WrapResult(func, log, request.Options.RetryAttempts, request.Name);
        }


        internal static TResult WrapResult<TResult>(Func<TResult> func, ILog logger, uint maxRetry, string name)
        {
            try
            {
                TResult result = default(TResult);
                RetryLogic.DoWithRetry(maxRetry, name, () => { result = func(); }, logger);
                return result;
            }
            catch (BaseSpaceException)
            {
                //todo: eventually do something here
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }


        internal static IContentReference<IAbstractResource> ResourceDeserializer(string source)
        {
            //determine type, then use appropriate deserializer
            var asValues = JsonSerializer.DeserializeFromString<Dictionary<string, string>>(source);

            string type = asValues["Type"];


            switch (type.ToLower())
            {
                case "file":
                    return JsonSerializer.DeserializeFromString<ReferenceWithFileContent>(source);

                case "appresult":
                    return JsonSerializer.DeserializeFromString<ReferenceWithAppResultContent>(source);

                case "sample":
                    return JsonSerializer.DeserializeFromString<ReferenceWithSampleContent>(source);

                case "project":
                    return JsonSerializer.DeserializeFromString<ReferenceWithProjectContent>(source);

                case "run":
                    return JsonSerializer.DeserializeFromString<ReferenceWithRunContent>(source);
            }

            return null;
        }

        internal class FileRestRequest : RestRequest
        {
            public FileInfo FileInfo { get; set; }
        }

        internal class RestRequest
        {
            public string Name { get; set; }
            public string RelativeOrAbsoluteUrl { get; set; }
            public HttpMethods Method { get; set; }
            public object Request { get; set; }
            public IRequestOptions Options { get; set; }
        }

        internal class StreamingRestRequest : RestRequest
        {
            public Stream Stream { get; set; }
            public string FileName { get; set; }
        }

        public void GetByteRange(Func<string> absoluteUrl, long start, long end, int chunkSize, int maxRetries, Action<byte[], long, long> dataHandler)
        {
            GetByteRange(absoluteUrl, start, end, dataHandler, chunkSize, maxRetries, Logger);
        }

        public static void GetByteRange(Func<string> absoluteUrl, long start, long end, Action<byte[], long, long> dataHandler, int chunkSize, int maxRetries, ILog Logger)
        {
            var len = end - start + 1;
            if (len > chunkSize)
                throw new ArgumentOutOfRangeException("Byte range requested is too large");
            RetryLogic.DoWithRetry(Convert.ToUInt32(maxRetries), string.Format("GetByteRange {0} -> {1} from {2}", start, end, absoluteUrl()), // may not be the actual url used
                () =>
                {
                    while (start < end + 1)
                    {
                        string url = absoluteUrl();
                        var webreq = HttpWebRequest.Create(url) as HttpWebRequest;
                        webreq.ServicePoint.ConnectionLimit = CONNECTION_COUNT;
                        webreq.ServicePoint.UseNagleAlgorithm = true;
                        webreq.Timeout = 200000;

                        Logger.InfoFormat("requesting {0}->{1}", start, end);
                        webreq.AddRange(start, end);

                        using (var resp = webreq.GetResponse() as HttpWebResponse)
                            start += CopyResponse(start, dataHandler, resp, chunkSize);
                    }
                },
            Logger);
        }

        private static int CopyResponse(long start, Action<byte[], long, long> dataHandler, WebResponse resp, int chunkSize)
        {
            using (var stm = resp.GetResponseStream())
            {
                var buffer = BufferPool.GetChunk(chunkSize);

                int totalRead = 0;
                try
                {
                    int read;
                    var length = (int)resp.ContentLength;
                    while ((read = stm.Read(buffer, totalRead, length - totalRead)) > 0)
                        totalRead += read;

                    dataHandler(buffer, start, totalRead);
                }
                finally
                {
                    BufferPool.ReleaseChunk(buffer);
                }
                return totalRead;
            }
        }

    }
}