using System.Runtime.Serialization;
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

        [DataMember]
        public int? Offset { get; set; }

        [DataMember]
        public int? Limit { get; set; }

        [DataMember]
        public SortDirection? SortDir { get; set; }

        [DataMember]
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

        [DataMember]
        public int? Offset { get; set; }

        [DataMember]
        public int? Limit { get; set; }

        [DataMember]
        public SortDirection? SortDir { get; set; }

        [DataMember]
        public TSortFieldEnumType? SortBy { get; set; }
    }
}
