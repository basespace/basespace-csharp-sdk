using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class UpdatePoolToLibraryMappingRequest : AbstractResourceRequest<UpdatePoolToLibraryMappingResponse>
    {
        /// <summary>
        /// Update Pool to Library Mapping
        /// </summary>
        public UpdatePoolToLibraryMappingRequest(string id, IEnumerable<string> libraryIds)
            : base(id)
        {
            LibraryIds = libraryIds;
            HttpMethod = HttpMethods.PUT;
        }
        public IEnumerable<string> LibraryIds { get; set; }

        protected override string GetUrl()
        {
            return string.Format("{0}/librarypools/{1}/libraries", Version, Id);
        }
    }
}
