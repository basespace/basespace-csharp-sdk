using System;
using System.Collections.Specialized;
using System.Linq;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK
{
    public static class RequestUrlExtensions
    {
        #region VerificationCode
        public static Uri BuildRequestUri(this VerificationCode verificationCode, BaseSpaceClientSettings settings)
        {
            var authentication = settings.Authentication as OAuth2Authentication;

            NameValueCollection queryPairs =
                new NameValueCollection
                    {
                        {"client_id", authentication.AppId},
                        {"response_type", "device_code"},// responseType},
                        {"scope", VerificationCode.AccessCreateBrowseGlobal}
                    };

            return new Uri(string.Format("{0}/{1}/oauthv2/deviceauthorization?{2}", settings.BaseSpaceApiUrl, settings.Version, ToQueryString(queryPairs)));
        }

        public static void FromJson(this VerificationCode verificationCode, string json)
        {
            verificationCode.VerificationUri = new Uri(ParseJson("verification_uri", json));
            verificationCode.VerificationWithCodeUri = new Uri(ParseJson("verification_with_code_uri", json));
            verificationCode.ExpiresIn = Int32.Parse(ParseJson("expires_in", json));
            verificationCode.UserCode = ParseJson("user_code", json);
            verificationCode.DeviceCode = ParseJson("device_code", json);
            verificationCode.Interval = 1;// Int32.Parse(ParseJSON("interval", json)); TODO: fix parser
        }
        #endregion VerificationCode

        #region AccessToken
        public static Uri BuildRequestUri(this AccessToken accessToken, VerificationCode verificationCode, BaseSpaceClientSettings settings)
        {
            var authentication = settings.Authentication as OAuth2Authentication;

            NameValueCollection queryPairs =
                new NameValueCollection
                {
                    {"client_id", authentication.AppId},
                    {"client_secret", authentication.AppSecret},
                    {"code", verificationCode.DeviceCode},
                    {"grant_type", "device"}
                };

            return new Uri(string.Format("{0}/{1}/oauthv2/token?{2}", settings.BaseSpaceApiUrl, settings.Version, ToQueryString(queryPairs)));
        }

        public static void FromJson(this AccessToken accessToken, string json)
        {
            accessToken.TokenString = ParseJson("access_token", json);
            if (json.Contains("expires_in"))  // TODO: do a better job of handling the case where the values do not exist
                accessToken.ExpiresIn = Int32.Parse(ParseJson("expires_in", json));
            accessToken.Error = ParseJson("error", json);
            accessToken.ErrorDescription = ParseJson("error_description", json);
        }
        #endregion AccessToken

        #region WebRequest helpers
        // poor man's json parser...prefer using a DTO
        public static string ParseJson(string getField, string jsonData)
        {
            string returnValue = string.Empty;
            if (!jsonData.Contains(getField))
                return returnValue;

            returnValue = jsonData.Substring((jsonData.IndexOf(getField) + getField.Length + 3));
            if (returnValue.IndexOf(",") < 0)
                returnValue = returnValue.Substring(0, returnValue.IndexOf("}") - 1);
            else
                returnValue = returnValue.Substring(0, returnValue.IndexOf(",") - 1);

            return returnValue;
        }

        public static string ToQueryString(NameValueCollection source)
        {
            //return HttpUtility.UrlEncode(string.Join("&", source.AllKeys
            return string.Join("&", source.AllKeys
                                          .SelectMany(key => source.GetValues(key)
                                                                   .Select(value => string.Format("{0}={1}", Uri.EscapeDataString(key), Uri.EscapeDataString(value))))
                                          .ToArray());
        }
        #endregion WebRequest
    }
}
