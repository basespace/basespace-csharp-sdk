using System.Net;

namespace Illumina.BaseSpace.SDK
{
	internal class OAuth2Authentication : IAuthentication
	{
		private readonly string authCode;

		public OAuth2Authentication(string appId, string appSecret)
		{
			AppId = appId;
			AppSecret = appSecret;
		}

		internal OAuth2Authentication(string authCode)
		{
			this.authCode = authCode;
		}

		public string AppId { get; private set; }

		public string AppSecret { get; private set; }

		public void UpdateHttpHeader(HttpWebRequest request)
		{
			UpdateHttpHeader(request.Headers);
		}

		internal void UpdateHttpHeader(WebHeaderCollection headers)
		{
			if (headers != null && !string.IsNullOrEmpty(authCode))
			{
				headers.Add("Authorization", string.Format("Bearer {0}", authCode));
			}
		}
	}
}
