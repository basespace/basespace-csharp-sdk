using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using Illumina.BaseSpace.SDK.Types;
using Common.Logging;
using MonoTouch.Foundation;
namespace Illumina.BaseSpace.SDK.MonoTouch.Tests.Integration
{
    public class BaseIntegrationTest
    {
        public BaseIntegrationTest()
        {
        }

		#region client singleton
        private static readonly Lazy<IBaseSpaceClient> _lazy =
            new Lazy<IBaseSpaceClient>(CreateRealClient);
        public static IBaseSpaceClient Instance { get { return _lazy.Value; } }
		public static readonly NSDictionary SettingsDict = new NSDictionary (NSBundle.MainBundle.PathForResource ("Settings.bundle/Root.plist", null));
		public static readonly ILog Log = LogManager.GetCurrentClassLogger();
		#endregion client singleton

        public IBaseSpaceClient Client
        {
            get { return Instance; }
        }

        // Note: prefer access through the Client property!
        public static IBaseSpaceClient CreateRealClient()
        {
            string apiKey = SettingsDict["basespace:api-key"].ToString();
			string apiSecret = SettingsDict["basespace:api-secret"].ToString();
			string apiUrl = SettingsDict["basespace:api-url"].ToString();
			string webUrl = SettingsDict["basespace:web-url"].ToString();
			string version = SettingsDict["basespace:api-version"].ToString();
			string authCode = SettingsDict["basespace:api-authcode"].ToString();
            var settings = new BaseSpaceClientSettings(){AppClientId = apiKey, AppClientSecret = apiSecret, BaseSpaceApiUrl = apiUrl, BaseSpaceWebsiteUrl = webUrl, Version =version};
            IBaseSpaceClient iBaseSpaceClient = new BaseSpaceClient(settings, new RequestOptions(apiUrl, authCode));
            return iBaseSpaceClient;
        }
    }
}
