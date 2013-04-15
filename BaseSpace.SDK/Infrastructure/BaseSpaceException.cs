using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Helpers;
using ServiceStack.Service;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface.ServiceModel;
using ServiceStack.Text;

namespace Illumina.BaseSpace.SDK
{
    public class BaseSpaceException : ApplicationException
    {
        public HttpStatusCode StatusCode { get; set; }

        public IHasResponseStatus Response { get; private set; }

        public dynamic ResponseBodyJson { get; private set; }

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
            {
                Response = wse.ResponseDto as IHasResponseStatus;
                if (wse.ResponseBody != null)
                {
                    try
                    {
                        ResponseBodyJson = Json.Decode(wse.ResponseBody);
                    }
                    catch { }
                }
            }
        }
    }
}
