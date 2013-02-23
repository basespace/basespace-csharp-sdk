
namespace Illumina.BaseSpace.SDK.ServiceModels
{
	public abstract class FileUploadRequestBase<TResult> : AbstractResourceRequest<TResult>
		where TResult : class
	{
		protected FileUploadRequestBase(string id, string name, string directory, string resourceIdentifierInUri)
		{
			Id = id;
			Name = name;

			if (directory != null)
				Directory = directory;

			ResourceIdentifierInUri = resourceIdentifierInUri;
		}

		public string Name { get; set; }

		public string Directory { get; set; }

		public bool? MultiPart { get; set; }

		public string ResourceIdentifierInUri { get; set; }
	}
}
