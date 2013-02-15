using System.Net;

namespace Illumina.BaseSpace.SDK
{
	public interface IAuthentication
	{
		void UpdateHttpHeader(HttpWebRequest request);
	}
}
