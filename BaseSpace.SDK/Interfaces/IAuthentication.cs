using System;
using System.Net;

namespace Illumina.BaseSpace.SDK
{
    public interface IAuthentication
    {
        void UpdateHttpHeader(HttpWebRequest request);
        void UpdateHttpHeader(WebHeaderCollection headers, Uri requestUri, string requestMethod);
    }
}
