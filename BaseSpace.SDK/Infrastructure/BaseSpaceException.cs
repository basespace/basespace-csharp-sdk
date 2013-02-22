using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using ServiceStack.Service;
using ServiceStack.ServiceClient.Web;

namespace Illumina.BaseSpace.SDK
{
    public class BaseSpaceException<TResponse> : ApplicationException where TResponse: class
    {
        public HttpStatusCode StatusCode { get; set; }
       
        public TResponse Response { get; private set; }

        public BaseSpaceException()
        {
        }

        public BaseSpaceException(string message)
            : base(message)
        {

        }

        public BaseSpaceException(string message, WebServiceException wse) : base(message, wse)
        {
            StatusCode = (HttpStatusCode)wse.StatusCode;
            Response = (TResponse)wse.ResponseDto;
        }
        public BaseSpaceException(string message, Exception ex)
            : base(message, ex)
        {
        }
    }
}
