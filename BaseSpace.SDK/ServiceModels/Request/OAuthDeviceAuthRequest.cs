using System;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
	[DataContract]
	public class OAuthDeviceAuthRequest : AbstractRequest<OAuthDeviceAuthResponse>
	{
		public OAuthDeviceAuthRequest (string clientId, string scope)
		{
			ResponseType = "device_code";
			ClientId = clientId;
			Scope = scope;
			HttpMethod = HttpMethods.POST;
		}

		[DataMember(Name = "response_type")]
		public string ResponseType { get; private set; }

		[DataMember(Name = "client_id")]
		public string ClientId { get; set; }

		[DataMember(Name = "scope")]
		public string Scope { get; set; }

		protected override string GetUrl()
		{
			return string.Format("{0}/oauthv2/deviceauthorization", Version);
		}
        
        internal override string GetLogMessage()
        {
            return "";
        }
	}
}

