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

		internal Func<TReturn> GetFunc(JsonServiceClient client, IRequestOptions options)
		{
			var httpMethod = HttpMethod.ToString();

			return () => client.Send<TReturn>(httpMethod, GetUrl(), (httpMethod == "GET") ? null : this);
		}

		internal string GetName()
		{
			return String.Format("{0} request to {1} ", HttpMethod, GetUrl());
		}

		protected abstract string GetUrl();
	}

	public abstract class AbstractResourceRequest<TReturn> : AbstractRequest<TReturn> 
		where TReturn : class
    {
		protected AbstractResourceRequest()
		{
		}

		protected AbstractResourceRequest(string id)
			: this()
		{
			Id = id;
		}

        public string Id { get; set; }
    }
}
