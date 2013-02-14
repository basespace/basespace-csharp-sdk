using System.Net;

namespace Illumina.BaseSpace.SDK
{
	public interface IAuthentication
	{
		void UpdateHttpHeader(HttpWebRequest request, IRequestOptions requestOptions);
	}

	public class OAuth1Authentication : IAuthentication
	{
		public OAuth1Authentication(string username, string password)
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

	public class OAuth2Authentication : IAuthentication
	{
		public OAuth2Authentication(string appId, string appSecret, string token)
		{
			
		}

		public void UpdateHttpHeader(HttpWebRequest request, IRequestOptions requestOptions)
		{
		}
	}

	//public class AuthenticationBridge
	//{
	//	public AuthenticationBridge(string username, string password)
	//	{
	//		Authentication = new OAuth1Authentication(username, password);
	//	}

	//	public AuthenticationBridge(string appId, string appSecret, string token)
	//	{
	//		Authentication = new OAuth2Authentication(appId, appSecret, token);
	//	}

	//	internal IAuthentication Authentication { get; set; }
	//}
}
