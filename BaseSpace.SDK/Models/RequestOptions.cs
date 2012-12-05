using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumina.BaseSpace.SDK.Models
{
    public class RequestOptions : IRequestOptions
    {
       
        public uint RetryAttempts { get;  set; }

        public string AuthCode { get;  set; }

        public string BaseUrl { get;  set; }
    }
}
