using System;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetLibraryContainerRequest : AbstractResourceRequest<GetLibraryContainerResponse>
    {
        /// <summary>
        /// Get specific Library Container
        /// </summary>
        /// <param name="id">Library Container Id</param>
        public GetLibraryContainerRequest(string id)
            : base(id)
        {
        }

        protected override string GetUrl()
        {
            return String.Format("{0}/librarycontainers/{1}", Version, Id);
        }
	}
}
