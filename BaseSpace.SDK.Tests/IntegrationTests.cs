using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Illumina.BaseSpace.SDK.Tests
{
    public class IntegrationTests
    {
        public IBaseSpaceClient CreateRealClient()
        {
            string apiKey = ConfigurationManager.AppSettings.Get("basespace:api-key");
            string apiSecret = ConfigurationManager.AppSettings.Get("basespace:api-secret");
            string apiUrl = ConfigurationManager.AppSettings.Get("basespace:api-url");
            string webUrl = ConfigurationManager.AppSettings.Get("basespace:web-url");
            string version = ConfigurationManager.AppSettings.Get("basespace:api-version");
            var settings = new BaseSpaceClientSettings(){AppClientId = apiKey, AppClientSecret = apiSecret, BaseSpaceApiUrl = apiUrl, BaseSpaceWebsiteUrl = webUrl, Version ="v1pre3"};
            var client = new BaseSpaceClient(settings);
            return client;
        }
    }
}
