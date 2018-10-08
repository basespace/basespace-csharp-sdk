using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Threading;
using Common.Logging;
#if  NETSTANDARD || NETCOREAPP
using ServiceStack;
#else
using ServiceStack.ServiceClient.Web;
#endif

namespace Illumina.BaseSpace.SDK
{
    internal static class RetryLogic
    {
        public static ICollection<int> RetryableCodes = new ReadOnlyCollection<int>(new[] { 0, 413, 500, 503, 504 });

        public static Func<Exception,bool> GenericRetryHandler = (wse) =>
                                                                     {
                                                                         var statusCode = GetStatusCode(wse);
                                                                         return RetryableCodes.Contains(statusCode);
                                                                     };

        public static int GetStatusCode(Exception exception)
        {
            int statusCode = 0;
            if (exception == null)
                return statusCode;

            if (exception.GetType() == typeof (WebServiceException))
                statusCode = ((WebServiceException) exception).StatusCode;
            else if (exception.GetType() == typeof (WebException))
            {
                var we = (WebException) exception;
                if (we.Response != null)
                {
                    var hwr = we.Response as HttpWebResponse;
                    if (hwr != null)
                    {
                        statusCode = (int) hwr.StatusCode;
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
                    string errorCode = exc.ErrorCode;
                    if (!HandleException(description, logger, allowRetry, whichAttempt, delay, message, errorCode, exc, statusCode, timer))
                        throw;

                    ex = exc;
                }
                catch (WebException exc)
                {
                    bool allowRetry = retryHandler(exc);
                    int statusCode = 0;
                    string message = exc.ToString();
                    string errorCode = string.Empty;
                    if (exc.Response != null)
                    {
                        var wr = ((HttpWebResponse)exc.Response);
                        statusCode = (int)wr.StatusCode;
                        message = wr.StatusDescription;
                    }
                    if (!HandleException(description, logger, allowRetry, whichAttempt, delay, message, errorCode, exc, statusCode, timer))
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
                    HandleException(description, logger, true, whichAttempt, delay, exc.ToString(), string.Empty, exc, 0, timer);
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
                logger.ErrorFormat("Maximum retries exceeded, total time {0}ms", timer.ElapsedMilliseconds);
                throw ex;
            }
        }

        private static bool HandleException(string description, ILog logger, bool allowRetry, int whichAttempt, int delay,
                                           string message, string errorCode, Exception exc, int statusCode, Stopwatch timer)
        {
            if (allowRetry)
            {
                logger.ErrorFormat("Error while {0}, attempt {1}, elapsed {4}ms, retrying in {2} seconds: \r\n{3}", description,
                                   whichAttempt, delay, message, timer.ElapsedMilliseconds);
                Thread.Sleep(1000 * delay);
                return true;
            }
            timer.Stop();
            logger.ErrorFormat("{0} HTTP Response code {1} : ({2}) {3} (elapsed time {4}ms)", description, statusCode, errorCode, message ?? exc.ToString(), timer.ElapsedMilliseconds);
            return false;
        }
    }
}
