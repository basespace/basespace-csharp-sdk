using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumina.BaseSpace.SDK.Models
{
    public class GetProjectRequest : IBaseSpaceRequest
    {
        /// <summary>
        /// Get a specific project
        /// </summary>
        /// <param name="id">Project Id</param>
        public GetProjectRequest(string id)
        {
            Id = id;
        }

        public string Id { get; set; }

        public string GenerateUrl(string version)
        {
            return string.Format("{0}/projects/{1}", version, Id);
        }
    }
}
