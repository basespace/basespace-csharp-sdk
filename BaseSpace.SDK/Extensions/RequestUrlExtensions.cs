using System;
using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK
{
    public static class RequestUrlExtensions
    {
        public static string BuildUrl(this GetProjectRequest req, string version)
        {
            return string.Format("{0}/projects/{1}", version, req.Id);
        }

        public static string BuildUrl(this GetRunRequest req, string version)
        {
            return string.Format("{0}/runs/{1}", version, req.Id);
        }

        public static string BuildUrl(this GetUserRequest req, string version)
        {
            return string.Format("{0}/users/{1}", version, req.Id ?? "current");
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
            return string.Format("{0}{1}", version, "/projects");
        }

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
