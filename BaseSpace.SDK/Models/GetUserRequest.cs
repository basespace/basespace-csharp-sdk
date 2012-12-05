using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumina.BaseSpace.SDK.Models
{
    public class GetUserRequest : IBaseSpaceRequest
    {
        public GetUserRequest(bool currentUser)
        {
            Id = null;
        }
        public GetUserRequest(string id)
        {
            Id = id;
        }
        public string Id { get; set; }


        public string GenerateUrl(string version)
        {
            return string.Format("{0}/users/{1}", version, Id ?? "current");
        }
    }
}
