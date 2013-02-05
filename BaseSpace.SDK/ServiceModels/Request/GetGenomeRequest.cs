using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetGenomeRequest : AbstractResourceRequest
    {
        /// <summary>
        /// Get specific Genome
        /// </summary>
        /// <param name="id">Genome Id</param>
        public GetGenomeRequest(string id)
        {
            Id = id;
        }
    }

}