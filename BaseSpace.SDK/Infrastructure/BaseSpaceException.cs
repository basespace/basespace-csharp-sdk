using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using ServiceStack.Service;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface.ServiceModel;

namespace Illumina.BaseSpace.SDK
{
    public class BaseSpaceException : ApplicationException
    {
        public HttpStatusCode StatusCode { get; set; }

        public IHasResponseStatus Response { get; private set; }

        public BaseSpaceException()
        {
        }

        public BaseSpaceException(string message)
            : base(message)
        {

        }

        public BaseSpaceException(string message, Exception ex)
            : base(message, ex)
        {
            StatusCode = (HttpStatusCode)RetryLogic.GetStatusCode(ex);
            WebServiceException wse = ex as WebServiceException;
            if (wse != null)
                Response = wse.ResponseDto as IHasResponseStatus;
        }
    }
}
