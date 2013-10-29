using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using ServiceStack.Service;

namespace Illumina.BaseSpace.SDK
{
    public class BaseSpaceException: Exception
    {
        public HttpStatusCode StatusCode { get; set; }
       
        public string Response { get; private set; }

        public BaseSpaceException()
        {
        }

        public BaseSpaceException(string message)
            : base(message)
        {

        }

		internal BaseSpaceException(HttpStatusCode status, string message, Exception innerException)
			: base(message, innerException)
		{
			StatusCode = status;
		}

        internal BaseSpaceException(HttpStatusCode status, string message)
            : base(message)
        {
            StatusCode = status;

        }
    }
}
