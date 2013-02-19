using System;
using System.Collections.Specialized;
using System.Linq;
using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.ServiceModels.Request;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK
{
    public static class RequestUrlExtensions
    {
        #region Users
        public static string BuildUrl(this GetUserRequest req, string version)
        {
            return string.Format("{0}/users/{1}", version, req.Id ?? "current");
        }
        #endregion

        #region Runs
        public static string BuildUrl(this GetRunRequest req, string version)
        {
            return string.Format("{0}/runs/{1}", version, req.Id);
        }

        public static string BuildUrl(this ListRunsRequest req, string version)
        {
            var urlWithParameters = AddDefaultQueryParameters(string.Format("{0}/users/current/runs", version), req.Offset,
                                                 req.Limit, req.SortDir);
            if (req.SortBy.HasValue)
            {
                urlWithParameters = string.Format("{0}&{1}={2}", urlWithParameters, QueryParameters.SortBy, req.SortBy);
            }
            if (!string.IsNullOrEmpty(req.Status))
            {
                urlWithParameters = string.Format("{0}&{1}={2}", urlWithParameters, RunSortByParameters.Status, req.Status);
            }
            return urlWithParameters;
        }
        #endregion
        #region Projects
        public static string BuildUrl(this GetProjectRequest req, string version)
        {
            return string.Format("{0}/projects/{1}", version, req.Id);
        }

        public static string BuildUrl(this ListProjectsRequest req, string version)
        {
            var urlWithParameters = AddDefaultQueryParameters(string.Format("{0}/users/current/projects", version), req.Offset,
                                                 req.Limit, req.SortDir);
            if (req.SortBy.HasValue)
            {
                urlWithParameters = string.Format("{0}&{1}={2}", urlWithParameters, QueryParameters.SortBy, req.SortBy);
            }
            if (!string.IsNullOrEmpty(req.Name))
            {
                urlWithParameters = string.Format("{0}&{1}={2}", urlWithParameters, QueryParameters.Name, req.Name);
            }
            return urlWithParameters;
        }

        public static string BuildUrl(this CreateProjectRequest req, string version)
        {
            return string.Format("{0}/projects", version);
        }
        #endregion
        #region AppSessions
        public static string BuildUrl(this GetAppSessionRequest req, string version)
        {
            return string.Format("{0}/appsessions/{1}", version, req.Id);
        }

        public static string BuildUrl(this UpdateAppSessionRequest req, string version)
        {
            return string.Format("{0}/appsessions/{1}", version, req.Id);
        }
        #endregion
        #region Samples
        public static string BuildUrl(this GetSampleRequest req, string version)
        {
            return string.Format("{0}/samples/{1}", version, req.Id);
        }

        public static string BuildUrl(this ListSamplesRequest req, string version)
        {
            var urlWithParameters = AddDefaultQueryParameters(string.Format("{0}/projects/{1}/samples", version, req.ProjectId), req.Offset,
                                                 req.Limit, req.SortDir);
            if (req.SortBy.HasValue)
                urlWithParameters = string.Format("{0}&{1}={2}", urlWithParameters, QueryParameters.SortBy, req.SortBy);

            return urlWithParameters;
        }
        #endregion
        #region AppResults
        public static string BuildUrl(this GetAppResultRequest req, string version)
        {
            return string.Format("{0}/appresults/{1}", version, req.Id);
        }

        public static string BuildUrl(this ListAppResultsRequest req, string version)
        {
            var urlWithParameters = AddDefaultQueryParameters(string.Format("{0}/projects/{1}/appresults", version, req.ProjectId), req.Offset,
                                                 req.Limit, req.SortDir);
            if (req.SortBy.HasValue)
            {
                urlWithParameters = string.Format("{0}&{1}={2}", urlWithParameters, QueryParameters.SortBy, req.SortBy);
            }

            return urlWithParameters;
        }

        public static string BuildUrl(this CreateAppResultRequest req, string version)
        {
            return string.Format("{0}/projects/{1}/appresults", version, req.ProjectId);
        }

        public static string BuildUrl(this UploadFileToAppResultRequest req, string version)
        {
            return string.Format("{0}/{1}/{2}/files", version, req.ResourceIdentifierInUri, req.Id);
        }
        
        #endregion
        #region Genomes
        public static string BuildUrl(this GetGenomeRequest req, string version)
        {
            return string.Format("{0}/genomes/{1}", version, req.Id);
        }

        public static string BuildUrl(this ListGenomeRequest req, string version)
        {
            var urlWithParameters = AddDefaultQueryParameters(string.Format("{0}/genomes", version), req.Offset,
                                                 req.Limit, req.SortDir);
            if (req.SortBy.HasValue)
            {
                urlWithParameters = string.Format("{0}&{1}={2}", urlWithParameters, QueryParameters.SortBy, req.SortBy);
            }

            return urlWithParameters;
        }

        #endregion


        #region Files
        public static string BuildUrl(this ListRunFilesRequest req, string version)
        {
            return string.Format("{0}/runs/{1}/files", version, req.Id);
        }

        public static string BuildUrl(this ListSampleFilesRequest req, string version)
        {
            return string.Format("{0}/samples/{1}/files", version, req.Id);
        }

        public static string BuildUrl(this ListAppResultFilesRequest req, string version)
        {
            return string.Format("{0}/appresults/{1}/files", version, req.Id);
        }

        public static string BuildUrl(this GetFileInformationRequest req, string version)
        {
            return string.Format("{0}/files/{1}", version, req.Id);
        }

        #endregion


        #region Variants
        public static string BuildUrl(this GetVariantHeaderRequest req, string version)
        {
            return string.Format("{0}/variantset/{1}", version, req.Id);
        }

        public static string BuildUrl(this ListVariantsRequest req, string version)
        {
            return string.Format("{0}/variantset/{1}/variants/{2}", version, req.Id, req.Chrom);
        }
        #endregion

        #region Coverage
        public static string BuildUrl(this GetCoverageRequest req, string version)
        {
            return string.Format("{0}/coverag/{1}/{2}", version, req.Id, req.Chrom);
        }
        
        public static string BuildUrl(this GetCoverageMetadataRequest req, string version)
        {
            return string.Format("{0}/coverag/{1}/{2}/meta", version, req.Id, req.Chrom);
        }
        #endregion

        #region FileDownload
        public static string BuildUrl(this FileContentRedirectMetaRequest req, string version)
        {
            return string.Format("{0}/files/{1}/content?Redirect={2}", version, req.Id, req.RedirectType);
        }
        #endregion
		#region OAuth
		public static string BuildUrl (this OAuthDeviceAuthRequest req, string version)
		{
			return string.Format("{0}/oauthv2/deviceauthorization", version);
		}

        public static string BuildUrl(this OAuthDeviceAccessTokenRequest req, string version)
        {
            return string.Format("{0}/oauthv2/token", version);
        }

        public static string BuildUrl(this OAuthV2AccessTokenRequest req, string version)
        {
            return string.Format("{0}/oauthv2/token", version);
        }
		#endregion

        #region VerificationCode
        public static Uri BuildRequestUri(this VerificationCode verificationCode, BaseSpaceClientSettings settings)
        {
            NameValueCollection queryPairs =
                new NameValueCollection
                    {
                        {"client_id", settings.AppClientId},
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
            NameValueCollection queryPairs =
                new NameValueCollection
			    {
			        {"client_id", settings.AppClientId},
			        {"client_secret", settings.AppClientSecret},
			        {"code", verificationCode.DeviceCode},
			        {"grant_type", "device"}
			    };

            return new Uri(string.Format("{0}/{1}/oauthv2/token?{2}", settings.BaseSpaceApiUrl, settings.Version, RequestUrlExtensions.ToQueryString(queryPairs)));
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

        private static string AddDefaultQueryParameters(string relativeUrl, int? offset, int? limit, SortDirection? sortDir)
        {
            var url = (offset.HasValue || limit.HasValue || sortDir.HasValue) && relativeUrl.Contains("?") ? relativeUrl : string.Format("{0}?", relativeUrl);
            if (offset.HasValue)
            {
                url = string.Format("{0}&{1}={2}", url, QueryParameters.Offset, offset.Value);
            }
            if (sortDir.HasValue)
            {
                url = string.Format("{0}&{1}={2}", url, QueryParameters.SortDir, sortDir.Value);
            }
            if (limit.HasValue)
            {
                url = string.Format("{0}&{1}={2}", url, QueryParameters.Limit, limit.Value);
            }
            return url;
        }
    }
}
