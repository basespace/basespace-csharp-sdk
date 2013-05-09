using System;
using ServiceStack.ServiceClient.Web;

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
			return () => client.Send<TReturn>(HttpMethod.ToString(), GetUrl(), this);
		}

		internal string GetName()
		{
			return String.Format("{0} request to {1} ", HttpMethod, GetUrl());
		}

        internal virtual string GetLogMessage()
        {
            return "";
        }

		protected abstract string GetUrl();
	}
}
