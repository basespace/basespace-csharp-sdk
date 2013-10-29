using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumina.BaseSpace.SDK
{
    public interface IClientSettings
    {
        uint RetryAttempts { get; }
        string AppClientId { get; }
        string AppClientSecret { get; }
        string BaseSpaceWebsiteUrl { get; }
        string BaseSpaceApiUrl { get; }
        string AppRedirectUrl { get; }
        string Version { get; }
        string OauthAuthorizeUrl { get; }
        uint FileMultipartSizeThreshold { get; }

    }
}
