using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

            var timer = new Stopwatch();

            while (triesLeft-- > 0)
            {
                timer.Start();
                var delay = (int)Math.Min(1800, Math.Pow(retryIntervalBaseSecs, whichAttempt++));

                try
                {
                   
                    logger.InfoFormat("operation starting: {0} attempt {1}", description, whichAttempt);
                    op();
                    timer.Stop();
                    logger.InfoFormat("{0} completed after {1} attempts and {2}ms", description, whichAttempt, timer.ElapsedMilliseconds);
                    return;
                }
                catch (WebServiceException exc)
                {
                    bool allowRetry = retryHandler(exc);

                    int statusCode = exc.StatusCode;
                    string message = exc.ErrorMessage;
                    if (!HandleException(description, logger, allowRetry, whichAttempt, delay, message, exc, statusCode, timer))
                        throw;

                    ex = exc;
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
                    if (!HandleException(description, logger, allowRetry, whichAttempt, delay, message, exc, statusCode, timer))
                        throw;
                    ex = exc;
                }
                catch (OperationCanceledException)
                {
                    timer.Stop();
                    logger.ErrorFormat("Operation canceled exception: {0}, do not retry, ran for {1}ms", description, timer.ElapsedMilliseconds);
                    return;
                }
                catch (Exception exc)
                {
                    HandleException(description, logger, true, whichAttempt, delay, exc.ToString(), exc, 0, timer);
                    ex = exc;
                }
            }
            if (ex != null)
            {
                if (timer.IsRunning)
                {
                    timer.Stop();
                }
                if (error != null)
                    error();
                throw new ApplicationException(string.Format("Maximum retries exceeded, total time {0}ms", timer.ElapsedMilliseconds) , ex);
            }
        }

        private static bool HandleException(string description, ILog logger, bool allowRetry, int whichAttempt, int delay,
                                           string message, Exception exc, int statusCode, Stopwatch timer)
        {
            if (allowRetry)
            {
                logger.ErrorFormat("Error while {0}, attempt {1}, elapsed {4}ms, retrying in {2} seconds: \r\n{3}", description,
                                   whichAttempt, delay, message, timer.ElapsedMilliseconds);
                System.Threading.Thread.Sleep(1000 * delay);
                return true;
            }
            timer.Stop();
            logger.ErrorFormat("HTTP Response code {0} : {1}, elapsed time {2}ms", statusCode, exc, timer.ElapsedMilliseconds);
            return false;
        }
    }
}
