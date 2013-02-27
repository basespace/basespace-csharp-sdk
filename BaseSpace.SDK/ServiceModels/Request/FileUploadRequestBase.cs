using System;
using System.IO;
using ServiceStack.ServiceClient.Web;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
	public abstract class FileUploadRequestBase<TResult> : AbstractResourceRequest<TResult>
		where TResult : class
	{
	    private readonly FileInfo fileInfo;

		protected FileUploadRequestBase(string id, string name, string directory, string resourceIdentifierInUri)
		{
            HttpMethod = HttpMethods.PUT;

			Id = id;
			Name = name;
		    fileInfo = new FileInfo(name);

			if (directory != null)
				Directory = directory;

			ResourceIdentifierInUri = resourceIdentifierInUri;
		}

		public string Name { get; set; }

		public string Directory { get; set; }

		public bool? MultiPart { get; set; }

		public string ResourceIdentifierInUri { get; set; }

        internal override Func<TResult> GetFunc(ServiceClientBase client)
        {
            return () => client.PostFileWithRequest<TResult>(GetUrl(), fileInfo, this);
        }

        protected override string GetUrl()
        {
            return string.Format("{0}/{1}/{2}/files", Version, ResourceIdentifierInUri, Id);
        }
	}
}
