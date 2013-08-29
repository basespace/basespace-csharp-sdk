using System;
using System.Collections.Specialized;
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
            var properties = new NameValueCollection();
            properties["showDateTime"] = "true";

            _lazy = new Lazy<IBaseSpaceClient>(CreateRealClient);

            // set Adapter
            LogManager.Adapter = new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter(properties);
            Debug.Listeners.Add(new DefaultTraceListener());
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

        // Note: prefer access through the Client property!
        protected virtual IBaseSpaceClient CreateRealClient()
        {
            //string apiKey = ConfigurationManager.AppSettings.Get("basespace:api-key");
            //string apiSecret = ConfigurationManager.AppSettings.Get("basespace:api-secret");
            string apiUrl = ConfigurationManager.AppSettings.Get("basespace:api-url");
            string webUrl = ConfigurationManager.AppSettings.Get("basespace:web-url");
            string version = ConfigurationManager.AppSettings.Get("basespace:api-version");
            
            var settings = new BaseSpaceClientSettings
				{
					Authentication = GetAuthentication(),
					BaseSpaceApiUrl = apiUrl, 
					BaseSpaceWebsiteUrl = webUrl, 
					Version = version
				};

			return new BaseSpaceClient(settings);
        }

        protected virtual IClientSettings BuildSettings()
        {
            string apiUrl = ConfigurationManager.AppSettings.Get("basespace:api-url");
            string webUrl = ConfigurationManager.AppSettings.Get("basespace:web-url");
            string version = ConfigurationManager.AppSettings.Get("basespace:api-version");

            return new BaseSpaceClientSettings
            {
                Authentication = GetAuthentication(),
                BaseSpaceApiUrl = apiUrl,
                BaseSpaceWebsiteUrl = webUrl,
                Version = version
            };
        }

        protected virtual IAuthentication GetAuthentication()
        {
            string accessToken = ConfigurationManager.AppSettings.Get("basespace:api-accesstoken");

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
