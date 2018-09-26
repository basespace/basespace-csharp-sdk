using System;
using System.Net;
using Common.Logging;
using Illumina.BaseSpace.SDK.Deserialization;
using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Types;
using Illumina.TerminalVelocity;

#if NETSTANDARD
using ServiceStack;
#else
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceModel.Serialization;
#endif
using ServiceStack.Text;
using Property = Illumina.BaseSpace.SDK.Types.Property;

namespace Illumina.BaseSpace.SDK
{
    public class JsonWebClient : IWebClient
    {
        private readonly JsonServiceClient client;
        private readonly JsonServiceClient clientBilling;

        private readonly ILog logger;

        private readonly IClientSettings settings;


        public JsonWebClient(IClientSettings settings, IRequestOptions defaultOptions = null)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            this.settings = settings;
            DefaultRequestOptions = defaultOptions ?? new RequestOptions();
            logger = LogManager.GetCurrentClassLogger();

            client = new JsonServiceClient(settings.BaseSpaceApiUrl);

            if (settings.TimeoutMin > 0)
                client.Timeout = TimeSpan.FromMinutes(settings.TimeoutMin);

            clientBilling = new JsonServiceClient(settings.BaseSpaceBillingApiUrl);

#if NETSTANDARD
            client.RequestFilter += WebRequestFilter;
            clientBilling.RequestFilter += WebRequestFilter;

#else
           // call something on this object so it gets initialized in single threaded context
            System.Web.Util.HttpEncoder.Default.SerializeToString();

			//need to add the following call for Mono -- https://bugzilla.xamarin.com/show_bug.cgi?id=12565
			if (Helpers.IsRunningOnMono())
			{
                System.Web.Util.HttpEncoder.Current = System.Web.Util.HttpEncoder.Default;
			}

            System.Web.Util.HttpEncoder.Current.SerializeToString();
            client.LocalHttpWebRequestFilter += WebRequestFilter;
            clientBilling.LocalHttpWebRequestFilter += WebRequestFilter;
#endif           

        }

        static JsonWebClient()
        {
#if NETSTANDARD
            JsConfig.DateHandler = DateHandler.ISO8601;
#else
            // setting this just to make sure it's not set in Linux
            JsonDataContractDeserializer.Instance.UseBcl = false;
            // BaseSpace uses this format for DateTime
             JsConfig.DateHandler = JsonDateHandler.ISO8601;
#endif

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

                var clientForRequest = PickClientForApiName(request.GetApiName());

                RetryLogic.DoWithRetry(options.RetryAttempts, request.GetName(),
                    () => result = request.GetSendFunc(clientForRequest)(), logger);
                return result;
            }
            catch (WebServiceException webx)
            {
                string errorCode = string.Empty;
                if (!string.IsNullOrEmpty(webx.ErrorCode))
                {
                    errorCode = string.Format(" ({0})", webx.ErrorCode);
                }
                var msg = string.Format("{0} status: {1} ({2}) Message: {3}{4}", request.GetName(), webx.StatusCode,
                    webx.StatusDescription, webx.ErrorMessage, errorCode);
                throw new BaseSpaceException(msg, webx.ErrorCode, webx);
            }
            catch (Exception x)
            {
                throw new BaseSpaceException(request.GetName() + " failed", string.Empty, x);
            }
        }

        private JsonServiceClient PickClientForApiName(ApiNames apiName)
        {
            switch (apiName)
            {
                case ApiNames.BASESPACE:
                    return client;
                case ApiNames.BASESPACE_BILLING:
                    return clientBilling;
                default:
                    throw new ArgumentOutOfRangeException("apiName");
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