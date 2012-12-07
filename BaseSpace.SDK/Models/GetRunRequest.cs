using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumina.BaseSpace.SDK.Models
{
    public class GetRunRequest
    {
        /// <summary>
        /// Get specific run
        /// </summary>
        /// <param name="id">Run Id</param>
        public GetRunRequest(string id)
        {
            Id = id;
        }

        public string Id { get; set; }

        public string GenerateUrl(string version)
        {
            return string.Format("{0}/runs/{1}", version, Id);
        }
    }
}
