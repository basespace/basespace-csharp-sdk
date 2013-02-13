using System.Net;

namespace Illumina.BaseSpace.SDK
{
	internal interface IAuthentication
	{
		void UpdateHttpHeader(HttpWebRequest request);
	}

	internal class OAuth1Authentication : IAuthentication
	{
		public void UpdateHttpHeader(HttpWebRequest request)
		{
			throw new System.NotImplementedException();
		}
	}

	internal class OAuth2Authentication : IAuthentication
	{
		public void UpdateHttpHeader(HttpWebRequest request)
		{
			throw new System.NotImplementedException();
		}
	}
}
