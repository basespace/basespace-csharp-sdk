using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetContainerToLibraryMappingRequest : AbstractResourceRequest<GetContainerToLibraryMappingResponse>
    {
        /// <summary>
        /// Get Libraries in a container
        /// </summary>
        /// <param name="id">Container Id</param>
        public GetContainerToLibraryMappingRequest(string id)
            : base(id)
        {
        }

        protected override string GetUrl()
        {
            return String.Format("{0}/librarycontainers/{1}/libraries", Version, Id);
        }
    }
}
