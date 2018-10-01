using System.Configuration;
using Xunit;
using Illumina.BaseSpace.SDK.ServiceModels;

namespace Illumina.BaseSpace.SDK.Tests.Integration
{
	public class OAuthTests : BaseIntegrationTest
	{
        [Fact]
        public void CanStartDeviceAuth()
        {
            OAuthDeviceAuthResponse response = null;
            response = Client.BeginOAuthDeviceAuth(new OAuthDeviceAuthRequest( GetConfigValue("basespace:api-key"), "browse global"));

            Assert.NotNull(response);
            Assert.NotNull(response.DeviceCode);
            Assert.NotNull(response.VerificationUri);
            Assert.NotNull(response.ExpiresIn);
            Assert.NotNull(response.Interval);
            Assert.NotNull(response.UserCode);
            Assert.NotNull(response.VerificationUriWithCode);

            Log.DebugFormat(@"
				device code: {0}
				verification uri: {1}
				expires in: {2}
				invterval: {3}
				user code: {4}
				verification uri with code: {5}
			",
                response.DeviceCode,
                response.VerificationUri,
                response.ExpiresIn,
                response.Interval,
                response.UserCode,
                response.VerificationUriWithCode);
        }

        [Fact]
        public void CanDoNativeAppAuth()
        {
            OAuthV2AccessTokenResponse response = null;
            response = Client.GetOAuthAccessToken(new OAuthV2AccessTokenRequest( GetConfigValue("basespace:client-id"),
                 GetConfigValue("basespace:client-secret"),
                 GetConfigValue("basespace:web-url"),
                 GetConfigValue("basespace:authorization-code")));
                
            Assert.NotNull(response);
            Assert.NotNull(response.AccessToken);
            Assert.NotNull(response.ExpiresIn);

            Log.DebugFormat(@"
				access token: {0}
				expires in: {1}
			",
                response.AccessToken,
                response.ExpiresIn);
        }

		[Fact]
		public void CanFinishDeviceAuthWithErrorStatus()
		{
			OAuthDeviceAuthResponse verificationResponse = null;
			OAuthDeviceAccessTokenResponse tokenResponse = null;
				verificationResponse = Client.BeginOAuthDeviceAuth(new OAuthDeviceAuthRequest( GetConfigValue("basespace:api-key"), "browse global"));
				tokenResponse = Client.FinishOAuthDeviceAuth (
					new OAuthDeviceAccessTokenRequest ( GetConfigValue("basespace:api-key"), 
				                                    GetConfigValue("basespace:api-secret"), verificationResponse.DeviceCode));

			Assert.NotNull(tokenResponse);
			Assert.NotNull(tokenResponse.Error);
			Assert.NotNull(tokenResponse.ErrorMessage);
			Assert.Equal("authorization_pending", tokenResponse.Error);

			Log.DebugFormat(@"
			error: {0}
			error message: {1}
			", tokenResponse.Error, tokenResponse.ErrorMessage);
		}

		//step thru to make it work, by going to the verification url and accepting!
		[Fact(Skip = "step through and browser interaction required")]
		public void CanFinishDeviceAuthWithSuccess()
		{
			OAuthDeviceAuthResponse verificationResponse = null;
			OAuthDeviceAccessTokenResponse tokenResponse = null;

				verificationResponse = Client.BeginOAuthDeviceAuth(new OAuthDeviceAuthRequest ( GetConfigValue("basespace:api-key"), "browse global"));

				// pause the debugger here and go accept the verification url

				tokenResponse = Client.FinishOAuthDeviceAuth (
					new OAuthDeviceAccessTokenRequest (GetConfigValue("basespace:api-key"),
					    GetConfigValue("basespace:api-secret"), verificationResponse.DeviceCode));
			
			Assert.NotNull(tokenResponse);
			Assert.NotNull(tokenResponse.AccessToken);

			Log.DebugFormat("Access Token: {0}", tokenResponse.AccessToken);
		}
	}
}

