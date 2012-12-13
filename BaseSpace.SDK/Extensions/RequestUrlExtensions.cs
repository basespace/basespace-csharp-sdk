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

        public static string BuildUrl(this GetUserProjectListRequest req, string version)
        {
            var urlWithDefaultQueryParameters = AddDefaultQueryParameters(string.Format("{0}/users/current/projects", version), req.Offset,
                                                 req.Limit, req.SortDir);
            return string.Format("{0}&{1}={2}&{3}={4}", urlWithDefaultQueryParameters, QueryParameters.SortBy, req.SortBy, QueryParameters.Name, req.Name);
        }

        public static string BuildUrl(this PostProjectRequest req, string version)
        {
            return string.Format("{0}{1}", version, "/projects");
        }

        private static string AddDefaultQueryParameters(string relativeUrl, int? offset, int? limit, SortDirection sortDir)
        {
            var url = relativeUrl.Contains("?") ? relativeUrl : string.Format("{0}?", relativeUrl);
            return string.Format("{0}&{1}={2}&{3}={4}&{5}={6}", url, QueryParameters.Offset, offset, QueryParameters.Limit,
                                 limit, QueryParameters.SortDir, sortDir);

        }

    }
}
