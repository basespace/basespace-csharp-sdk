using System;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public abstract class AbstractResourceListRequest<TResult, TSortFieldEnumType> : AbstractResourceRequest<TResult>
		where TResult : class 
		where TSortFieldEnumType : struct
    {
		protected AbstractResourceListRequest()
		{
		}

		protected AbstractResourceListRequest(string id)
			: base(id)
		{
		}

        public int? Offset { get; set; }

        public int? Limit { get; set; }

        public SortDirection? SortDir { get; set; }

        public TSortFieldEnumType? SortBy { get; set; }

		protected virtual string BuildUrl(string relativeUrl)
		{
			var url = HasFilters() && relativeUrl.Contains("?") ? relativeUrl : String.Format("{0}?", relativeUrl);

			url = UpdateUrl(Offset, url);
			url = UpdateUrl(SortDir, url);
			url = UpdateUrl(Limit, url);
			url = UpdateUrl(SortBy, url);

			return url;
		}

		protected virtual bool HasFilters()
		{
			return (Offset.HasValue || Limit.HasValue || SortDir.HasValue);
		}

		protected static string UpdateUrl<T>(T? property, string url)
			where T : struct
		{
			return (!property.HasValue) ? url :
				String.Format("{0}&{1}={2}", url, QueryParameters.Offset, property.Value);
		}

		protected static string UpdateUrl<T>(T property, string url)
			where T : class
		{
			return (property == null) ? url :
				String.Format("{0}&{1}={2}", url, QueryParameters.Offset, property);
		}
    }
}
