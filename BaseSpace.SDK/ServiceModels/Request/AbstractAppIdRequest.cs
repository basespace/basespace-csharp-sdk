// Copyright AB SCIEX 2014. All rights reserved.

using System.Runtime.Serialization;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
	public abstract class AbstractAppIdRequest<TResult, TSortFieldEnumType> : AbstractResourceRequest<TResult>
		where TResult : class
		where TSortFieldEnumType : struct
	{
		protected AbstractAppIdRequest()
		{
		}

		protected AbstractAppIdRequest(string appId, int? offset = null, int? limit = null, SortDirection? sortDir = null,
			TSortFieldEnumType? sortBy = null)
		{
			Offset = offset;
			Limit = limit;
			SortDir = sortDir;
			SortBy = sortBy;
			AppId = appId;
		}


		[DataMember]
		public int? Offset { get; set; }

		[DataMember]
		public int? Limit { get; set; }

		[DataMember]
		public SortDirection? SortDir { get; set; }

		[DataMember]
		public TSortFieldEnumType? SortBy { get; set; }

		[DataMember]
		public string AppId { get; set; }
	}
}