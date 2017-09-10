using System;
using System.Net;

namespace Illumina.BaseSpace.SDK
{
	public class OAuth2Authentication : IAuthentication
	{		
		public OAuth2Authentication(string accessToken)
		{
            AccessToken = accessToken;
		}

		public OAuth2Authentication(string appId, string appSecret)
		{
			AppId = appId;
			AppSecret = appSecret;
		}

		public string AppId { get; private set; }

		public string AppSecret { get; private set; }

        public string AccessToken { get; private set; }

        public void UpdateHttpHeader(HttpWebRequest request)
		{
			UpdateHttpHeader(request.Headers, request.RequestUri, request.Method);
		}

        public void UpdateHttpHeader(WebHeaderCollection headers, Uri requestUri, string requestMethod)
		{
			if (headers != null && !string.IsNullOrEmpty(AccessToken))
			{
				headers.Add("Authorization", string.Format("Bearer {0}", AccessToken));
			}
		}
	}
}
