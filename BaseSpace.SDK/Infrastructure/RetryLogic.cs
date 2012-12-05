using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using Common.Logging;
using ServiceStack.Service;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface.ServiceModel;

namespace Illumina.BaseSpace.SDK
{
    public static class RetryLogic
    {
        public static ICollection<int> RetryableCodes = new ReadOnlyCollection<int>(new[] { 413, 500, 503, 504 });

        public static Func<Exception, bool> GenericRetryHandler = (wse) =>
        {
            var statusCode = GetStatusCode(wse);
            if (wse.Message.ToLower().Contains("timed out"))
                return true;
            if (wse.Message.ToLower().Contains("interrupted"))
                return true;
            if (wse.Message.ToLower().Contains("nameresolutionfailure"))
                return true;
            if (wse.Message.ToLower().Contains("connectfailure"))
                return true;
            if (wse.Message.ToLower().Contains("connection reset by peer"))
                return true;
            if (wse.Message.ToLower().Contains("receivefailure"))
                return true;

            //  --> (Inner exception 1) System.ApplicationException: Non retryable web exception 
            //  ---> System.Net.WebException: An error occurred performing a WebClient request. 
            //  ---> System.IO.IOException: IO exception during Write. 
            //  ---> System.NullReferenceException: Object reference not set to an instance of an object
            if (wse.Message.ToLower().Contains("an error occurred performing a webclient request"))
                return true;


            return RetryableCodes.Contains(statusCode);
        };

        public static int GetStatusCode(Exception exception)
        {
            int statusCode = 0;
            if (exception == null)
                return statusCode;

            if (exception.GetType() == typeof(WebServiceException))
                statusCode = ((WebServiceException)exception).StatusCode;
            else if (exception.GetType() == typeof(WebException))
            {
                var we = (WebException)exception;
                if (we.Response != null)
                {
                    var hwr = we.Response as HttpWebResponse;
                    if (hwr != null)
                    {
                        statusCode = (int)hwr.StatusCode;
                    }
                }
            }
            return statusCode;
        }

        public static void DoWithRetry(uint maxAttempts, string description, Action op, ILog logger,
                                       double retryIntervalBaseSecs = 5, Action error = null,
                                        Func<Exception, bool> retryHandler = null)
        {
          
            retryIntervalBaseSecs = Math.Max(1, retryIntervalBaseSecs);
            var triesLeft = maxAttempts;
            int whichAttempt = 0;
            Exception ex = null;
            if (retryHandler == null)
                retryHandler = GenericRetryHandler;

            while (triesLeft-- > 0)
            {
                var delay = (int)Math.Min(1800, Math.Pow(retryIntervalBaseSecs, whichAttempt++));

                try
                {
                    op();
                    logger.InfoFormat("{0} completed after {1} attempts", description, whichAttempt);
                    return;
                }
                catch (WebServiceException exc)
                {
                    bool allowRetry = retryHandler(exc);
                    int statusCode = exc.StatusCode;
                    string message = exc.ErrorMessage;

                    ex = HandleException(description, logger, allowRetry, whichAttempt, delay, message, exc, statusCode);
                }
                catch (WebException exc)
                {
                    bool allowRetry = retryHandler(exc);
                    int statusCode = 0;
                    string message = exc.ToString();
                    if (exc.Response != null)
                    {
                        var wr = ((HttpWebResponse)exc.Response);
                        statusCode = (int)wr.StatusCode;
                        message = wr.StatusDescription;
                    }
                    ex = HandleException(description, logger, allowRetry, whichAttempt, delay, message, exc, statusCode);
                }
                catch (OperationCanceledException)
                {
                    logger.ErrorFormat("Operation canceled exception, do not retry");
                    return;
                }
                catch (Exception exc)
                {
                    ex = HandleException(description, logger, true, whichAttempt, delay, exc.ToString(), exc, 0); ;
                }
            }
            if (ex != null)
            {
                if (error != null)
                    error();
                throw new ApplicationException("Maximum retries exceeded", ex);
            }
        }

        private static Exception HandleException(string description, ILog logger, bool allowRetry, int whichAttempt, int delay,
                                           string message, Exception exc, int statusCode)
        {
            if (allowRetry)
            {
                logger.ErrorFormat("Error while {0}, attempt {1}, retrying in {2} seconds: \r\n{3}", description,
                                   whichAttempt, delay, message);
                System.Threading.Thread.Sleep(1000 * delay);
                return exc;
            }
            else
            {
                logger.ErrorFormat("HTTP Response code {0} : {1}", statusCode, exc);
                var code = HttpStatusCode.InternalServerError;
                Enum.TryParse(statusCode.ToString(), out code);
                throw new BaseSpaceException(code, message);
            }
        }
    }
}
