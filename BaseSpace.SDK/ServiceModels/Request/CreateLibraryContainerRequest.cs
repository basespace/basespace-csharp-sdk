using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class CreateLibraryContainerRequest : AbstractRequest<CreateLibraryContainerResponse>
    {
        public CreateLibraryContainerRequest(string libraryPrepId, string plateId, string containerType, bool indexByWell)
        {
            LibraryPrepId = libraryPrepId;
            UserContainerId = plateId;
            ContainerType = containerType;
            IndexByWell = indexByWell;
            HttpMethod = HttpMethods.POST;
        }
        
        public string ContainerType { get; set; }
        public string UserContainerId { get; set; }
        public string LibraryPrepId { get; set; }
        public string Notes { get; set; }
        public bool IndexByWell { get; set; }
        protected override string GetUrl()
        {
            return string.Format("{0}/librarycontainers", Version);
        }
    }
}

