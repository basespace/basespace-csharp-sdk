using System;
using System.Collections.Specialized;
using System.Configuration;
using Common.Logging;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.Tests.Integration
{
    public class BaseIntegrationTest
    {
        private ILog _log;
        protected ILog Log
        {
            get 
            { 
                _log = _log ?? LogManager.GetCurrentClassLogger();
                return _log;
            }
        }

        public BaseIntegrationTest()
        {
            //configure console logging
            // create properties
            var properties = new NameValueCollection();
            properties["showDateTime"] = "true";

            // set Adapter
            LogManager.Adapter = new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter(properties);
        }

		#region client singleton
        private static readonly Lazy<IBaseSpaceClient> _lazy =
            new Lazy<IBaseSpaceClient>(CreateRealClient);
        public static IBaseSpaceClient Instance { get { return _lazy.Value; } }
		#endregion client singleton

        public IBaseSpaceClient Client
        {
            get { return Instance; }
        }

        // Note: prefer access through the Client property!
        public static IBaseSpaceClient CreateRealClient()
        {
            string apiKey = ConfigurationManager.AppSettings.Get("basespace:api-key");
            string apiSecret = ConfigurationManager.AppSettings.Get("basespace:api-secret");
            string apiUrl = ConfigurationManager.AppSettings.Get("basespace:api-url");
            string webUrl = ConfigurationManager.AppSettings.Get("basespace:web-url");
            string version = ConfigurationManager.AppSettings.Get("basespace:api-version");
            string authCode = ConfigurationManager.AppSettings.Get("basespace:api-authcode");
            var settings = new BaseSpaceClientSettings
				{
					Authentication = new OAuth2Authentication(apiKey, apiSecret),
					BaseSpaceApiUrl = apiUrl, 
					BaseSpaceWebsiteUrl = webUrl, 
					Version = version
				};
            
			
			
			IBaseSpaceClient iBaseSpaceClient = new BaseSpaceClient(settings, new RequestOptions(apiUrl, new OAuth2Authentication(authCode)));
            return iBaseSpaceClient;
        }
    }
}
