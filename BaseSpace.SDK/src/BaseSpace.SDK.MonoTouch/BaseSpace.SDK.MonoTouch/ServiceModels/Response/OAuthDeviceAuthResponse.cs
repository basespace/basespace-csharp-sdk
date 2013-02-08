using System;
using System.Runtime.Serialization;
namespace Illumina.BaseSpace.SDK.ServiceModels
{
	[DataContract]
	public class OAuthDeviceAuthResponse
	{
		public OAuthDeviceAuthResponse ()
		{
		}

		[DataMember(Name = "device_code")]
		public string DeviceCode { get; set; }

		[DataMember(Name = "user_code")]
		public string UserCode { get; set; }

		[DataMember(Name = "verification_uri")]
		public string VerificationUri { get; set; }

		[DataMember(Name = "verification_with_code_uri")]
		public string VerificationUriWithCode { get; set; }

		[DataMember(Name = "expires_in")]
		public int ExpiresIn { get; set; }

		[DataMember(Name = "interval")]
		public int Interval { get; set; }
	}
}

