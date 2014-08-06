using System;
using System.IO;
using ServiceStack.ServiceClient.Web;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public abstract class FileUploadRequestBase<TResult> : AbstractResourceRequest<TResult>
        where TResult : class
    {
        protected FileUploadRequestBase(string id, string filePath, string directory, string resourceIdentifierInUri)
        {
            HttpMethod = HttpMethods.PUT;

            ThreadCount = 16;
            Id = id;
            FileInfo = new FileInfo(filePath);
            Name = FileInfo.Name;

            if (directory != null)
                Directory = directory;

            ResourceIdentifierInUri = resourceIdentifierInUri;
        }

        public string Name { get; set; }

        public string Directory { get; set; }

        public int ThreadCount { get; set; }

        private bool? isMultiPart;

        public bool? MultiPart
        {
            get { return isMultiPart; }
            set
            {
                isMultiPart = value;
                HttpMethod = (value.HasValue && value.Value) ? HttpMethods.POST : HttpMethods.PUT;
            }
        }

        internal FileInfo FileInfo { get; private set; }

        public string ResourceIdentifierInUri { get; set; }

        internal override Func<TResult> GetSendFunc(ServiceClientBase client)
        {
            return (MultiPart.HasValue && MultiPart.Value) ? base.GetSendFunc(client) :
                 () => client.PostFileWithRequest<TResult>(GetUrl(), FileInfo, this);
        }

        protected override string GetUrl()
        {
            return string.Format("{0}/{1}/{2}/files", Version, ResourceIdentifierInUri, Id);
        }
    }
}
