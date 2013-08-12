using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Util;
using System.Linq;
using Common.Logging;
using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Types;
using ServiceStack.Common.Extensions;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceModel.Serialization;
using ServiceStack.Text;

namespace Illumina.BaseSpace.SDK
{
    public class JsonWebClient : IWebClient
    {
        private readonly JsonServiceClient client;

        private readonly ILog logger;

        private readonly IClientSettings settings;

        private static readonly JsonSerializer<Notification<Agreement>> agreementSerializer = new JsonSerializer<Notification<Agreement>>();

        private static readonly JsonSerializer<Notification<ScheduledDowntime>> scheduledSerializer = new JsonSerializer<Notification<ScheduledDowntime>>();

        public JsonWebClient(IClientSettings settings, IRequestOptions defaultOptions = null)
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
            HttpEncoder.Current.SerializeToString();

            client = new JsonServiceClient(settings.BaseSpaceApiUrl);
            client.LocalHttpWebRequestFilter += WebRequestFilter;
        }

        static JsonWebClient()
        {
            // setting this just to make sure it's not set in Linux
            JsonDataContractDeserializer.Instance.UseBcl = false;
            // BaseSpace uses this format for DateTime
            JsConfig.DateHandler = JsonDateHandler.ISO8601;

            JsConfig<Uri>.DeSerializeFn = s => new Uri(s, s.StartsWith("http") ? UriKind.Absolute : UriKind.Relative);
            //handle complex parsing of references
            JsConfig<IContentReference<IAbstractResource>>.RawDeserializeFn = ResourceDeserializer;

            JsConfig<Property>.RawDeserializeFn = RawDeserializeFn;
            
            JsConfig<INotification<object>>.RawDeserializeFn = NotificationDeserializer;
        }


        private static Property RawDeserializeFn(string s)
        {
            var json = JsonObject.Parse(s);
            var property = new Property()
                               {
                                   Description = json.Get("Description"),
                                   Href = json.Get<Uri>("Href"),
                                   Type = json.Get("Type"),
                                   Name = json.Get("Name"),
                                   HrefItems = json.Get<Uri>("HrefItems"),
                                   ItemsDisplayedCount = json.Get<int?>("ItemsDisplayedCount"),
                                   ItemsTotalCount = json.Get<int?>("ItemsTotalCount")
                               };

            var simpleType = property.GetSimpleType();
            if (json.ContainsKey("Content"))
            {
                switch (simpleType)
                {
                    case Property.TYPE_STRING:
                        property.Content = new PropertyContentLiteral(property.Type, json.Get("Content"));
                        break;
                    default:
                        property.Content = DeserializePropertyReference(simpleType, json.Child("Content"));
                        break;
                }
            }

            if (json.ContainsKey("Items"))
            {
                switch (simpleType)
                {
                    case Property.TYPE_STRING:
                        property.Items = json.Get<string[]>("Items").Select(i => new PropertyContentLiteral(simpleType, i)).ToArray();
                        break;
                    default:
                        property.Items = json.ArrayObjects("Items").Select(itemj => DeserializePropertyReference(simpleType, itemj.ToJson())).Where(x => x != null).ToArray();
                        break;
                }
            }

            return property;
        }

        internal static IPropertyContent DeserializePropertyReference(string type, string json)
        {
            IPropertyContent ret = null;
            if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(json))
            {
                return null;
            }
            switch (type)
            {
                case Property.TYPE_PROJECT:
                    ret = JsonSerializer.DeserializeFromString<ProjectCompact>(json);
                    break;
            }
            return ret;
        }

        internal static IPropertyContent PropertyDeserializer(string source)
        {
            var asValues = JsonSerializer.DeserializeFromString<Dictionary<string, string>>(source);
            var z = JsonObject.Parse(source);
            return null;
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
                if (!string.IsNullOrWhiteSpace(request.GetLogMessage()))
                    logger.Info(request.GetLogMessage());
                TReturn result = null;
                options = options ?? DefaultRequestOptions;

                RetryLogic.DoWithRetry(options.RetryAttempts, request.GetName(), () => result = request.GetSendFunc(client)(), logger);
                return result;
            }
            catch (WebServiceException webx)
            {
                var msg = string.Format("{0} status: {1} ({2}) Message: {3}", request.GetName(), webx.StatusCode,
                                        webx.StatusDescription, webx.ErrorMessage);
                throw new BaseSpaceException(msg, webx);
            }
            catch (Exception x)
            {
                throw new BaseSpaceException(request.GetName() + " failed", x);
            }
        }

        private void WebRequestFilter(HttpWebRequest req)
        {
            settings.Authentication.UpdateHttpHeader(req);
        }

        private static INotification<object> NotificationDeserializer(string source)
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
                    return JsonSerializer.DeserializeFromString<ContentReference<FileCompact>>(source);

                case "appresult":
                    return JsonSerializer.DeserializeFromString<ContentReference<AppResultCompact>>(source);

                case "sample":
                    return JsonSerializer.DeserializeFromString<ContentReference<SampleCompact>>(source);

                case "project":
                    return JsonSerializer.DeserializeFromString<ContentReference<ProjectCompact>>(source);

                case "run":
                    return JsonSerializer.DeserializeFromString<ContentReference<RunCompact>>(source);
            }

            return null;
        }
    }
}