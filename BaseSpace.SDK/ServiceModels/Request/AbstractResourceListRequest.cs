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
    }
    // for nested resources
    public abstract class AbstractNestedResourceListRequest<TResult, TSortFieldEnumType> : AbstractRequest<TResult>
        where TResult : class
        where TSortFieldEnumType : struct
    {
        protected AbstractNestedResourceListRequest()
        {
        }

        public int? Offset { get; set; }

        public int? Limit { get; set; }

        public SortDirection? SortDir { get; set; }

        public TSortFieldEnumType? SortBy { get; set; }
    }
}
