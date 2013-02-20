using System.Net;

namespace Illumina.BaseSpace.SDK
{
	internal class OAuth2Authentication : IAuthentication
	{
		private readonly string accessToken;

		public OAuth2Authentication(string appId, string appSecret)
		{
			AppId = appId;
			AppSecret = appSecret;
		}

		internal OAuth2Authentication(string accessToken)
		{
			this.accessToken = accessToken;
		}

		public string AppId { get; private set; }

		public string AppSecret { get; private set; }

		public void UpdateHttpHeader(HttpWebRequest request)
		{
			UpdateHttpHeader(request.Headers);
		}

		internal void UpdateHttpHeader(WebHeaderCollection headers)
		{
			if (headers != null && !string.IsNullOrEmpty(accessToken))
			{
				headers.Add("Authorization", string.Format("Bearer {0}", accessToken));
			}
		}
	}
}
