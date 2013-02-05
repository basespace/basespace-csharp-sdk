﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetSampleRequest : AbstractResourceRequest
    {
        /// <summary>
        /// Get specific sample
        /// </summary>
        /// <param name="id">Sample Id</param>
       public GetSampleRequest(string id)
        {
            Id = id;
        }
    }
}
