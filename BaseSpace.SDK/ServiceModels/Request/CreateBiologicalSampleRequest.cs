using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class CreateBiologicalSampleRequest : AbstractRequest<CreateBiologicalSampleResponse>
    {
        public CreateBiologicalSampleRequest(string sampleId, string name, string nucleicAcid)
        {
            UserSampleId = sampleId;
            Name = name;
            NucleicAcid = nucleicAcid;
            HttpMethod = HttpMethods.POST;
        }

        public string UserSampleId { get; set; }
        public string Name { get; set; }
        public string SpeciesId { get; set; }
        public string ProjectId { get; set; }
        public string NucleicAcid { get; set; }
        
        protected override string GetUrl()
        {
            return string.Format("{0}/biologicalsamples", Version);
        }
    }
}
