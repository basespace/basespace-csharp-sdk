using System.Net;

namespace Illumina.BaseSpace.SDK
{
	public interface IAuthentication
	{
		void UpdateHttpHeader(HttpWebRequest request, IRequestOptions requestOptions);
	}

	internal class OAuth2Authentication : IAuthentication
	{
		public OAuth2Authentication(string appId, string appSecret, string token)
		{
		}

		public void UpdateHttpHeader(HttpWebRequest request, IRequestOptions requestOptions)
		{
			if (requestOptions != null && !string.IsNullOrEmpty(requestOptions.AuthCode))
			{
				request.Headers.Add("Authorization", string.Format("Bearer {0}", requestOptions.AuthCode));
			}
		}
	}
}
