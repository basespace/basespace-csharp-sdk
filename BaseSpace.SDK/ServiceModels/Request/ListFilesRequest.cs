namespace Illumina.BaseSpace.SDK.ServiceModels
{
	public abstract class ListFilesRequest<TResult, TSortFieldEnumType> : AbstractResourceListRequest<TResult, TSortFieldEnumType>
		where TResult : class
		where TSortFieldEnumType : struct
	{
		protected ListFilesRequest(string id)
			: base(id)
		{
		}

		public string Extensions { get; set; }
	}
}
