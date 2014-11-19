using System;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Web.Util;
using Common.Logging;
using Illumina.BaseSpace.SDK.Deserialization;
using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Types;
using Illumina.TerminalVelocity;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceModel.Serialization;
using ServiceStack.Text;

namespace Illumina.BaseSpace.SDK
{
    public class JsonWebClient : IWebClient
    {
        internal JsonServiceClient client;

        private ILog logger;

        private IClientSettings settings;

        public virtual void RespawnClient()
        {
            if (_clientFactoryMethod != null)
                _clientFactoryMethod();
        }


        internal readonly Action _clientFactoryMethod; 


        public JsonWebClient(IClientSettings settings, IRequestOptions defaultOptions = null)
        {
            _clientFactoryMethod = () =>
            {
                if (settings == null)
                {
                    throw new ArgumentNullException("settings");
                }

                this.settings = settings;
                DefaultRequestOptions = defaultOptions ?? new RequestOptions();
                logger = LogManager.GetCurrentClassLogger();

                // call something on this object so it gets initialized in single threaded context
                HttpEncoder.Default.SerializeToString();

                //need to add the following call for Mono -- https://bugzilla.xamarin.com/show_bug.cgi?id=12565
                if (Helpers.IsRunningOnMono())
                {
                    HttpEncoder.Current = HttpEncoder.Default;
                }

                HttpEncoder.Current.SerializeToString();

                client = new JsonServiceClient(settings.BaseSpaceApiUrl);
                client.LocalHttpWebRequestFilter += WebRequestFilter;

                if (settings.TimeoutMin > 0)
                    client.Timeout = TimeSpan.FromMinutes(settings.TimeoutMin);
            };

            _clientFactoryMethod();
        }

        static JsonWebClient()
        {
            // setting this just to make sure it's not set in Linux
            JsonDataContractDeserializer.Instance.UseBcl = false;
            // BaseSpace uses this format for DateTime
            JsConfig.DateHandler = JsonDateHandler.ISO8601;

            JsConfig<Uri>.DeSerializeFn = s => new Uri(s, s.StartsWith("http") ? UriKind.Absolute : UriKind.Relative);

            // setup custom deserializers
            JsConfig<IContentReference<IAbstractResource>>.RawDeserializeFn = ReferenceDeserializer.JsonToReference;
            
            JsConfig<PropertyCompact>.RawDeserializeFn = PropertyDeserializer.JsonToPropertyCompact;
            JsConfig<Property>.RawDeserializeFn = PropertyDeserializer.JsonToProperty;

            JsConfig<INotification<object>>.RawDeserializeFn = MiscDeserializers.NotificationDeserializer;
            JsConfig<PropertyItemsResourceList>.RawDeserializeFn = PropertyDeserializer.JsonToPropertyItemsResourceList;     
        }

        public IWebProxy WebProxy
        {
            get { return client.Proxy; }
            set { client.Proxy = value; }
        }

        public IRequestOptions DefaultRequestOptions { get; set; }

        public TReturn Send<TReturn>(AbstractRequest<TReturn> request, IRequestOptions options = null)
            where TReturn : class
        {
            try
            {
                if (logger.IsDebugEnabled)
                {
                    var debugMessage = request.GetDebugLogMessage();
                    if (!string.IsNullOrWhiteSpace(debugMessage))
                    {
                        logger.Debug(debugMessage);
                    }
                }

                if (logger.IsInfoEnabled)
                {
                    var infoMessage = request.GetInfoLogMessage();
                    if (!string.IsNullOrEmpty(infoMessage))
                    {
                        logger.Info(infoMessage);
                    }
                }

                TReturn result = null;
                options = options ?? DefaultRequestOptions;

                RetryLogic.DoWithRetry(options.RetryAttempts, request.GetName(), () => result = request.GetSendFunc(client)(), logger
                    , retryIntervalBaseSecs:options.RetryPowerBase,retryHandler: (exc) =>
                    {
                        RespawnClient();
                        return RetryLogic.GenericRetryHandler(exc);
                    });
                return result;
            }
            catch (WebServiceException webx)
            {
                string errorCode = string.Empty;
                if (!string.IsNullOrEmpty(webx.ErrorCode))
                {
                    errorCode = string.Format(" ({0})", webx.ErrorCode);
                }
                var msg = string.Format("{0} status: {1} ({2}) Message: {3}{4}", request.GetName(), webx.StatusCode, webx.StatusDescription, webx.ErrorMessage, errorCode);
                throw new BaseSpaceException(msg, webx.ErrorCode, webx);
            }
            catch (Exception x)
            {
                throw new BaseSpaceException(request.GetName() + " failed", string.Empty, x);
            }
        }

        private void WebRequestFilter(HttpWebRequest req)
        {
            if (settings.Authentication != null)
            {
                settings.Authentication.UpdateHttpHeader(req);
            }
        }
    }
}