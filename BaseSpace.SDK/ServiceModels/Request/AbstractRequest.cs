using System;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceModel;
namespace Illumina.BaseSpace.SDK.ServiceModels
{
	public abstract class AbstractRequest<TReturn>
		where TReturn : class
	{
		protected AbstractRequest()
		{
			HttpMethod = HttpMethods.GET;
		}

		protected HttpMethods HttpMethod { get; set; }

		protected string Version
		{
			get { return "v1pre3"; }
		}

		internal virtual Func<TReturn> GetSendFunc(ServiceClientBase client)
		{
		    return RequestHelpers.GetSendFunc<TReturn>(client, HttpMethod, GetUrl(), this);
		}

		internal string GetName()
		{
			return String.Format("{0}:{1}", HttpMethod, GetUrl());
		}

        internal virtual string GetInfoLogMessage()
        {
            // No logging is done by default for now. Override in subclass to log something
            return string.Empty;
        }

        internal virtual string GetDebugLogMessage()
        {
            // Logging requests by default as debug level
            return GetName();
        }

		protected abstract string GetUrl();
	}

    public static class RequestHelpers
    {
        internal static Func<TReturn> GetSendFunc<TReturn>(ServiceClientBase client, HttpMethods httpMethod, string requestUrl, object requestObj)
        {
            return () => client.Send<TReturn>(httpMethod.ToString(), requestUrl, requestObj);
        }
    }
}
