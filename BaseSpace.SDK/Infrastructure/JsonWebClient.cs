using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Util;
using Common.Logging;
using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Types;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceModel.Serialization;
using ServiceStack.Text;

namespace Illumina.BaseSpace.SDK
{
    public class JsonWebClient : IWebClient
    {
		private JsonServiceClient client;

	    private ILog logger;

		private IClientSettings settings;

        public IRequestOptions DefaultRequestOptions { get; protected set; }

        private const int CONNECTION_COUNT = 8; //TODO: Is this the right place?

        private static JsonSerializer<Notification<Agreement>> agreementSerializer = new JsonSerializer<Notification<Agreement>>();

        private static JsonSerializer<Notification<ScheduledDowntime>> scheduledSerializer = new JsonSerializer<Notification<ScheduledDowntime>>();

        static JsonWebClient()
        {
            ChangeSerializationOptions();
        }

        public JsonWebClient(IClientSettings settings, IRequestOptions defaultOptions = null)
        {
            logger = LogManager.GetCurrentClassLogger();

            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }
            this.settings = settings;
            // call something on this object so it gets initialized in single threaded context
            HttpEncoder.Default.SerializeToString();
            HttpEncoder.Current.SerializeToString();
            if (defaultOptions == null)
            {
                defaultOptions = new RequestOptions(settings.BaseSpaceApiUrl);
            }
           
            SetDefaultRequestOptions(defaultOptions);
        }

		private void WebRequestFilter(HttpWebRequest req)
		{
			if (DefaultRequestOptions.Authentication != null)
				DefaultRequestOptions.Authentication.UpdateHttpHeader(req);
			else
				settings.Authentication.UpdateHttpHeader(req);
		}

        static void ChangeSerializationOptions()
        {
            // setting this just to make sure it's not set in Linux
            JsonDataContractDeserializer.Instance.UseBcl = false;
            // BaseSpace uses this format for DateTime
            JsConfig.DateHandler = JsonDateHandler.ISO8601;

            JsConfig<Uri>.DeSerializeFn = ParseUri;
            //handle complex parsing of references
            JsConfig<IContentReference<IAbstractResource>>.RawDeserializeFn = ResourceDeserializer;

            JsConfig<INotification<object>>.RawDeserializeFn = NotificationDeserializer;

        }

		public void SetDefaultRequestOptions(IRequestOptions options)
		{
			DefaultRequestOptions = options;
		}

		public TReturn Send<TReturn>(AbstractRequest<TReturn> request, IRequestOptions options = null)
			where TReturn : class
		{
			return WrapResult(request, options ?? DefaultRequestOptions, logger, DefaultRequestOptions.RetryAttempts);
		}

		private TReturn WrapResult<TReturn>(AbstractRequest<TReturn> request, IRequestOptions options, ILog logger, uint maxRetry)
			where TReturn : class
		{
			try
			{
				TReturn result = null;
				RetryLogic.DoWithRetry(maxRetry, request.GetName(), () =>
				{
					result = request.GetFunc(client, options)();
				}
			, logger);
				return result;
			}
			catch (WebServiceException wex)
			{
				throw new BaseSpaceException<TReturn>(request.GetName() + " failed", wex);
			}
		}

        private static Uri ParseUri(string s)
        {
            UriKind uriKind = (s.StartsWith("http") || s.StartsWith("https")) ? UriKind.Absolute : UriKind.Relative;
            return new Uri(s, uriKind);
        }

		//private static TReturn Execute<TReturn>(JsonServiceClient client, RestRequest request, ILog log)
		//	where TReturn : class
		//{
		//	client.BaseUri = request.Options.BaseUrl.TrimEnd('/') + "/";  //make sure we only have a single slash on end always
            

		//	var fileRestRequest = request as FileRestRequest;
		//	if (fileRestRequest != null)
		//	{
		//		Func<TReturn> funcFile = () => client.PostFileWithRequest<TReturn>(fileRestRequest.RelativeOrAbsoluteUrl,
		//																		   fileRestRequest.FileInfo,
		//																		   fileRestRequest.Request);

		//		return WrapResult(funcFile, log, fileRestRequest.Options.RetryAttempts, fileRestRequest.Name);
		//	}
		//	var restRequest = request as StreamingRestRequest;
		//	if (restRequest != null)
		//	{
		//		var sr = restRequest;

		//		Func<TReturn> funcStream =
		//			() => client.PostFileWithRequest<TReturn>(restRequest.RelativeOrAbsoluteUrl, sr.Stream,
		//													  sr.FileName,
		//													  restRequest.Request);

		//		return WrapResult(funcStream, log, restRequest.Options.RetryAttempts, restRequest.Name);
		//	}

		//	Func<TReturn> func =
		//		() => client.Send<TReturn>(request.Method.ToString(), request.RelativeOrAbsoluteUrl, request.Request);

		//	return WrapResult(func, log, request.Options.RetryAttempts, request.Name);
		//}

        internal static TReturn WrapResult<TReturn>(Func<TReturn> func, ILog logger, uint maxRetry, string name)
            where TReturn : class
        {
            try
            {
                TReturn result = null;
                RetryLogic.DoWithRetry(maxRetry, name, () => { result = func(); }, logger);
                return result;
            }
            catch (Exception wex)
            {
                throw new BaseSpaceException(name + " failed", wex);
            }
        }

        internal static INotification<object> NotificationDeserializer(string source)
        {
            //determine type, then use appropriate deserializer
            var asValues = JsonSerializer.DeserializeFromString<Dictionary<string, string>>(source);
            string type = asValues["Type"];

            object o = null;

            switch (type.ToLower())
            {
                case "agreement":
                    o = agreementSerializer.DeserializeFromString(source).Item;
                    break;
                case "scheduleddowntime":
                    o = scheduledSerializer.DeserializeFromString(source).Item;
                    break;
            }
            return new Notification<object> { Item = o, Type = type };
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