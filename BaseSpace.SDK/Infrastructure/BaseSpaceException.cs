using System;
using System.Net;

#if NETSTANDARD
using ServiceStack;
#else
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface.ServiceModel;
#endif
using ServiceStack.Text;

namespace Illumina.BaseSpace.SDK
{
    public class BaseSpaceException : ApplicationException
    {
        public HttpStatusCode StatusCode { get; set; }

        public string ErrorCode { get; private set; }

        public IHasResponseStatus Response { get; private set; }

        public dynamic ResponseBodyJson { get; private set; }

        public BaseSpaceException()
        {
        }

        public BaseSpaceException(string message)
            : base(message)
        {

        }

        public BaseSpaceException(string message, string errorCode, Exception ex)
            : base(message, ex)
        {
            StatusCode = (HttpStatusCode)RetryLogic.GetStatusCode(ex);
            ErrorCode = errorCode;
            WebServiceException wse = ex as WebServiceException;
            if (wse != null)
            {
                Response = wse.ResponseDto as IHasResponseStatus;
                if (wse.ResponseBody != null)
                {
                    try
                    {
                        ResponseBodyJson = JsonObject.Parse(wse.ResponseBody);
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
}
