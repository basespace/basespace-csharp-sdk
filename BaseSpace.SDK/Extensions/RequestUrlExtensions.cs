using Illumina.BaseSpace.SDK.ServiceModels;
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

        public static string BuildUrl(this PostProjectRequest req, string version)
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

        public static string BuildUrl(this PostAppResultRequest req, string version)
        {
            return string.Format("{0}/projects/{1}/appresults", version, req.ProjectId);
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
        
        #endregion

        #region Variants
        
        #endregion

        #region Coverage

        #endregion


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
