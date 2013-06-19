﻿using System;
using System.Collections.Generic;
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
            JsConfig<IResource>.RawDeserializeFn = ResourceDeserializer;

            JsConfig<INotification<object>>.RawDeserializeFn = NotificationDeserializer;
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
            catch (Exception wex)
            {
                throw new BaseSpaceException(request.GetName() + " failed", wex);
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

        internal static IResource ResourceDeserializer(string source)
        {
            //determine type, then use appropriate deserializer
            var asValues = JsonSerializer.DeserializeFromString<Dictionary<string, string>>(source);

            string type = asValues["Type"];

            switch (type.ToLower())
            {
                case "file":
                    return JsonSerializer.DeserializeFromString<ContentReferenceResource<FileCompact>>(source);

                case "appresult":
                    return JsonSerializer.DeserializeFromString<ContentReferenceResource<AppResultCompact>>(source);

                case "sample":
                    return JsonSerializer.DeserializeFromString<ContentReferenceResource<SampleCompact>>(source);

                case "project":
                    return JsonSerializer.DeserializeFromString<ContentReferenceResource<ProjectCompact>>(source);

                case "run":
                    return JsonSerializer.DeserializeFromString<ContentReferenceResource<RunCompact>>(source);

                case "string":
                    return JsonSerializer.DeserializeFromString<ContentSettingResource<string>>(source);

                case "string[]":
                    return JsonSerializer.DeserializeFromString<ContentSettingResource<string[]>>(source);

                case "integer":
                    return JsonSerializer.DeserializeFromString<ContentSettingResource<int>>(source);

            }

            return null;
        }
    }
}