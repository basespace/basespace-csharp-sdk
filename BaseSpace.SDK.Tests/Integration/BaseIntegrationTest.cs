using System;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using Common.Logging;
using Xunit;


namespace Illumina.BaseSpace.SDK.Tests.Integration
{
    public class BaseIntegrationTest
    {
        public BaseIntegrationTest()
        {
            //configure console logging
            // create properties
            var properties = new Common.Logging.Configuration.NameValueCollection();
            properties["showDateTime"] = "true";

            _lazy = new Lazy<IBaseSpaceClient>(CreateRealClient);

            // set Adapter
#if !NETCOREAPP
            LogManager.Adapter = new Common.Logging.Simple.DebugLoggerFactoryAdapter(properties);
            Debug.Listeners.Add(new DefaultTraceListener());
#endif
        }

        private readonly Lazy<IBaseSpaceClient> _lazy;

        public IBaseSpaceClient Client
        {
            get { return _lazy.Value; }
        }

        private ILog _log;
        protected ILog Log
        {
            get
            {
                _log = _log ?? LogManager.GetCurrentClassLogger();
                return _log;
            }
        }


        public static string GetConfigValue(string key)
        {
            var module = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ".dll";
            var config = ConfigurationManager.OpenExeConfiguration(module);
            var data = config.AppSettings.Settings[key];
            return data?.Value;
        }


        // Note: prefer access through the Client property!
        protected virtual IBaseSpaceClient CreateRealClient()
        {
            //string apiKey =  GetConfigValue("basespace:api-key");
            //string apiSecret =  GetConfigValue("basespace:api-secret");
            string apiUrl =  GetConfigValue("basespace:api-url");
            string apiBillingUrl =  GetConfigValue("basespace:api-billing-url");
            string webUrl =  GetConfigValue("basespace:web-url");
            string version =  GetConfigValue("basespace:api-version");
            
            var settings = new BaseSpaceClientSettings
				{
					Authentication = GetAuthentication(),
					BaseSpaceApiUrl = apiUrl, 
                    BaseSpaceBillingApiUrl = apiBillingUrl,
					BaseSpaceWebsiteUrl = webUrl, 
					Version = version
				};

			return new BaseSpaceClient(settings);
        }

        protected virtual IClientSettings BuildSettings()
        {
            string apiUrl =  GetConfigValue("basespace:api-url");
            string webUrl =  GetConfigValue("basespace:web-url");
            string apiBillingUrl =  GetConfigValue("basespace:api-billing-url");
            string version =  GetConfigValue("basespace:api-version");

            return new BaseSpaceClientSettings
            {
                Authentication = GetAuthentication(),
                BaseSpaceApiUrl = apiUrl,
                BaseSpaceBillingApiUrl = apiBillingUrl,
                BaseSpaceWebsiteUrl = webUrl,
                Version = version
            };
        }

        protected virtual IAuthentication GetAuthentication()
        {
            string accessToken =  GetConfigValue("basespace:api-accesstoken");

            return new OAuth2Authentication(accessToken);
        }

        public static void AssertErrorResponse(Action function, string errorCode = null, HttpStatusCode? statusCode = null)
        {
            try
            {
                function();
            }
            catch (BaseSpaceException x)
            {
                if (!string.IsNullOrEmpty(errorCode))
                {
                    Assert.Equal(errorCode, x.ErrorCode);
                }
                if (statusCode != null)
                {
                    Assert.Equal(statusCode.Value, x.StatusCode);
                }
                return;
            }

            Assert.Equal("No exception thrown.", string.Empty);
        }
    }
}
