using System;
using System.Runtime.Serialization;
namespace Illumina.BaseSpace.SDK.ServiceModels
{
	[DataContract]
	public class OAuthDeviceAccessTokenRequest : AbstractRequest<OAuthDeviceAccessTokenResponse>
	{
		public OAuthDeviceAccessTokenRequest (string clientId, string clientSecret, string deviceCode)
		{
			GrantType = "device";
			ClientId = clientId;
			ClientSecret = clientSecret;
			DeviceCode = deviceCode;
			HttpMethod = HttpMethods.POST;
		}

		[DataMember(Name = "client_id")]
		public string ClientId { get; set; }

		[DataMember(Name = "client_secret")]
		public string ClientSecret { get; set; }

		[DataMember(Name = "code")]
		public string DeviceCode { get; set; }

		[DataMember(Name = "grant_type")]
		public string GrantType { get; set; }

		protected override string GetUrl()
		{
			return string.Format("{0}/oauthv2/token", Version);
		}

        internal override string GetLogMessage()
        {
            return "";
        }
	}
}

