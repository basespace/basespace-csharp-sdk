using System;
using NUnit.Framework;
using Illumina.BaseSpace.SDK.ServiceModels;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Illumina.BaseSpace.SDK.MonoTouch.Tests.Integration
{
    [TestFixture]
    public class OAuthTests : BaseIntegrationTest
    {
        [Test]
        public void CanStartDeviceAuth ()
        {
            OAuthDeviceAuthResponse response = null;
            try
            {
                response = Client.BeginOAuthDeviceAuth (new OAuthDeviceAuthRequest (SettingsDict ["basespace:api-key"].ToString (), "browse global"));
            }
            catch (BaseSpaceException bse)
            {
                Log.Debug(bse.Message);
                Assert.Fail(bse.Message);
            }
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.DeviceCode);
            Assert.IsNotNull(response.VerificationUri);
            Assert.IsNotNull(response.ExpiresIn);
            Assert.IsNotNull(response.Interval);
            Assert.IsNotNull(response.UserCode);
            Assert.IsNotNull(response.VerificationUriWithCode);

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

        [Test]
        public void CanFinishDeviceAuthWithErrorStatus()
        {
            OAuthDeviceAuthResponse verificationResponse = null;
            OAuthDeviceAccessTokenResponse tokenResponse = null;
            try
            {
                verificationResponse = Client.BeginOAuthDeviceAuth(new OAuthDeviceAuthRequest (SettingsDict ["basespace:api-key"].ToString (), "browse global"));
                tokenResponse = Client.FinishOAuthDeviceAuth (
                    new OAuthDeviceAccessTokenRequest (SettingsDict ["basespace:api-key"].ToString (),
                                                   SettingsDict ["basespace:api-secret"].ToString (), verificationResponse.DeviceCode));
            }
            catch (Exception ex)
            {
                Log.Debug(ex.Message);
                Assert.Fail(ex.Message);
            }

            Assert.IsNotNull(tokenResponse);
            Assert.IsNotNull(tokenResponse.Error);
            Assert.IsNotNull(tokenResponse.ErrorMessage);
            Assert.AreEqual("authorization_pending", tokenResponse.Error);

            Log.DebugFormat(@"
			error: {0}
			error message: {1}
			", tokenResponse.Error, tokenResponse.ErrorMessage);
        }

        [Ignore]//step thru to make it work, by going to the verification url and accepting!
        [Test]
        public void CanFinishDeviceAuthWithSuccess()
        {
            OAuthDeviceAuthResponse verificationResponse = null;
            OAuthDeviceAccessTokenResponse tokenResponse = null;
            try
            {
                verificationResponse = Client.BeginOAuthDeviceAuth(new OAuthDeviceAuthRequest (SettingsDict ["basespace:api-key"].ToString (), "browse global"));

                // pause the debugger here and go accept the verification url

                tokenResponse = Client.FinishOAuthDeviceAuth (
                    new OAuthDeviceAccessTokenRequest (SettingsDict ["basespace:api-key"].ToString (),
                                                   SettingsDict ["basespace:api-secret"].ToString (), verificationResponse.DeviceCode));
            }
            catch (Exception ex)
            {
                Log.Debug(ex.Message);
                Assert.Fail(ex.Message);
            }

            Assert.IsNotNull(tokenResponse);
            Assert.IsNotNull(tokenResponse.AccessToken);

            Log.DebugFormat("Access Token: {0}", tokenResponse.AccessToken);
        }
    }
}

