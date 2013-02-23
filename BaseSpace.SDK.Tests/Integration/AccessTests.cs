using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Illumina.BaseSpace.SDK.Types;
using Xunit;

namespace Illumina.BaseSpace.SDK.Tests.Integration
{
    public class AccessTests : BaseIntegrationTest
    {
        // the next line should only be enabled when running tests interactively since it requires personal attention to a browser that pops up
        [Fact]
        public void CreateWebRequestTest()
        {
            var client = CreateWebRequestClient();
            Assert.NotNull(client);
        }

        public static IBaseSpaceClient CreateWebRequestClient()
        {
            // construct the settings object from the config file
            string apiKey = ConfigurationManager.AppSettings.Get("basespace:api-key");
            string apiSecret = ConfigurationManager.AppSettings.Get("basespace:api-secret");
            string apiUrl = ConfigurationManager.AppSettings.Get("basespace:api-url");
            string webUrl = ConfigurationManager.AppSettings.Get("basespace:web-url");
            string version = ConfigurationManager.AppSettings.Get("basespace:api-version");
            var settings = new BaseSpaceClientSettings
	                           {
		                           Authentication = new OAuth2Authentication(apiKey, apiSecret),
								   BaseSpaceApiUrl = apiUrl, 
								   BaseSpaceWebsiteUrl = webUrl, 
								   Version = version
	                           };

            // first retrieve the verification code
            var verificationCode = FetchVerificationCode(settings);

            // initiate the steps that validate the verification code
            LaunchBrowser(verificationCode.VerificationWithCodeUri);

            // poll for the access token
            AccessToken accessToken = FetchAccessToken(verificationCode, settings);

            var client = new BaseSpaceClient(settings, new RequestOptions(apiUrl, new OAuth2Authentication(accessToken.TokenString)));

            // build and return the client
            return client;
        }

        private static VerificationCode FetchVerificationCode(BaseSpaceClientSettings settings)
        {
			//VerificationCode verificationCode = new VerificationCode();
			//var verificationUri = verificationCode.BuildRequestUri(settings);
			//if ((verificationUri == null) || string.IsNullOrEmpty(verificationUri.AbsoluteUri))
			//	return null; // TODO: throw exception

			//string jsonResponse = HttpPost(verificationUri, string.Empty);
			//verificationCode.FromJson(jsonResponse);

			//return verificationCode;

	        throw new NotImplementedException();
        }


        private static AccessToken FetchAccessToken(VerificationCode verificationCode, BaseSpaceClientSettings settings)
        {
			//AccessToken accessToken = null;
			//Int32 interval = verificationCode.Interval * 1000;

			//// TODO: use the new "await" instead of looping
			//// TODO: we should have a hard stop at some useful limit of time
			//while (accessToken == null)
			//{
			//	Thread.Sleep(interval);

			//	try
			//	{
			//		// TODO: wrap in try-catch with System.Net.WebException handler
			//		Uri authUri = accessToken.BuildRequestUri(verificationCode, settings);
			//		string jsonResponse = HttpPost(authUri, string.Empty);
			//		//string response = HttpPost(authVerificationCode.VerificationWithCodeUri.AbsoluteUri, string.Empty);
			//		// TODO: convert to using 200 response
			//		if ((!string.IsNullOrEmpty(jsonResponse) && !jsonResponse.Contains("DOCTYPE")))
			//		{
			//			accessToken = new AccessToken();
			//			accessToken.FromJson(jsonResponse);
			//			//// TODO: add sensitivity to return value, e.g., 404
			//			//switch(response.getClientResponseStatus())
			//			//{
			//			//    case BAD_REQUEST:
			//			//        AccessToken token = mapper.readValue(response.getEntity(string.class), AccessToken.class);
			//			//        if (token.getError().equalsIgnoreCase(ACCESS_DENIED))
			//			//        {
			//			//            throw new AccessDeniedException();
			//			//        }
			//			//        break;
			//			//    case OK:
			//			//        token = mapper.readValue(response.getEntity(string.class), AccessToken.class);
			//			//        accessToken = token.getAccessToken();
			//			//}
			//		}

			//	}
			//	catch (System.Net.WebException)
			//	{
			//		// ignore these exceptions
			//		continue;
			//		//throw;
			//	}
			//}
			//return accessToken;

	        throw new NotImplementedException();
        }

        // TODO: pay attention to the return type so we know whether to proceed?
        private static void LaunchBrowser(Uri path)
        {
            // TODO: do we need try-catch at both this and the above levels?
            try
            {
                var p = Process.Start(path.AbsoluteUri);
            }
            catch (Exception)
            {
                var msg = string.Format("A browser but cannot be opened on this computer.  One is required to continue.", path.AbsoluteUri);
                //dialogService.ShowMessage(msg, "Browser Error", DialogButton.Ok, DialogImage.Warning);
                //_log.Error(msg);
                throw;
            }
        }

        protected static String ToQueryString(NameValueCollection source)
        {
            //return HttpUtility.UrlEncode(String.Join("&", source.AllKeys
            return String.Join("&", source.AllKeys
                .SelectMany(key => source.GetValues(key)
                    .Select(value => String.Format("{0}={1}", Uri.EscapeDataString(key), Uri.EscapeDataString(value))))
                .ToArray());
        }

        // TODO: make a version that uses the ServiceStack style used throughout the SDK
        // TODO: make a version that uses the TPL from .NET 4.5 that supports (Http)WebRequest with the Task<xx> return type and "await"
        #region Http Verbs support
        // quick implementation from Scott Hanselman http://www.hanselman.com/blog/HTTPPOSTsAndHTTPGETsWithWebClientAndCAndFakingAPostBack.aspx
        public static string HttpGet(string uri, string accessToken) // TODO: take an AccessToken object instead of just the string value
        {
            WebRequest request = WebRequest.Create(uri);
            //request.Proxy = new WebProxy(ProxyString, true); //true means no proxy
            if (!string.IsNullOrEmpty(accessToken))
                request.Headers.Add("x-access-token", accessToken);

            // TODO: add sensitivity to return value, e.g., 404
            WebResponse response = request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            return sr.ReadToEnd().Trim();
        }

        public static string HttpPost(Uri uri, string Parameters)
        {
            WebRequest request = WebRequest.Create(uri.AbsoluteUri);
            //request.Proxy = new WebProxy(ProxyString, true);
            //Add these, as we're doing a POST
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            //We need to count how many bytes we're sending. Post'ed Faked Forms should be name=value&
            byte[] bytes = Encoding.ASCII.GetBytes(Parameters);
            request.ContentLength = bytes.Length;

            // this line is like the -k option in curl (and should be avoided)
            //ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);

            Stream os = request.GetRequestStream();
            os.Write(bytes, 0, bytes.Length); //Push it out there
            os.Close();
            WebResponse response = request.GetResponse();
            // TODO: add sensitivity to return value, e.g., 404
            if (response == null) return null;
            StreamReader sr = new StreamReader(response.GetResponseStream());
            return sr.ReadToEnd().Trim();
        }
        #endregion Http Verbs support
    }
}
