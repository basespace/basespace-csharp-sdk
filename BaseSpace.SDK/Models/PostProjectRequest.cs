using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumina.BaseSpace.SDK.Models
{
    public class PostProjectRequest
    {
        public PostProjectRequest(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public string GenerateUrl(string version)
        {
            return string.Format("{0}{1}", version, "/projects");
        }
    }
}
