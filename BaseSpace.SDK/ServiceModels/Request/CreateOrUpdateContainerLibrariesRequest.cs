using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class CreateOrUpdateContainerLibrariesRequest : AbstractRequest<CreateOrUpdateContainerLibrariesResponse>
    {
        public CreateOrUpdateContainerLibrariesRequest(IEnumerable<SampleLibraryRequest> libraries, string containerId)
        {
            Libraries = libraries;
            ContainerId = containerId;
            HttpMethod = HttpMethods.POST;
        }

        public IEnumerable<SampleLibraryRequest> Libraries { get; set; }
        public string ContainerId { get; set; }

        protected override string GetUrl()
        {
            return string.Format("{0}/librarycontainers/{1}/libraries", Version, ContainerId);
        }
  
    }
}