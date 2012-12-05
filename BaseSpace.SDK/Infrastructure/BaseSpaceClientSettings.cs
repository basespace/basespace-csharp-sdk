using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumina.BaseSpace.SDK
{
    public class BaseSpaceClientSettings : IClientSettings
    {
        public uint RetryAttempts { get;  set; }
        public string AppClientId { get;  set; }
        public string AppClientSecret { get;  set; }
        public string BaseSpaceWebsiteUrl { get;  set; }
        public string BaseSpaceApiUrl { get;  set; }
        public string AppRedirectUrl { get;  set; }
        public string Version { get;  set; }
        public string OauthAuthorizeUrl { get;  set; }
        public uint FileMultipartSizeThreshold { get;  set; }
    }
}
