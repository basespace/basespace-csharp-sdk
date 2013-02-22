using System;
using ServiceStack.ServiceClient.Web;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public abstract class AbstractResourceRequest
    {
		protected AbstractResourceRequest()
		{
		}

		protected AbstractResourceRequest(string id)
		{
			Id = id;
		}

        public string Id { get; set; }

	    protected string DefaultHttpMethod { get; private set; }

	    internal Func<TReturn> GetFunc<TReturn>(JsonServiceClient client, IRequestOptions options)
			where TReturn : class
	    {
		    string httpMethod = options.HttpMethod ?? DefaultHttpMethod;

			return () => client.Send<TReturn>(httpMethod, GetUrl(), (httpMethod == "GET") ? null : this);
		}

		internal string GetName()
		{
			return String.Format("{0} request to {1} ", DefaultHttpMethod, GetUrl());
		}

		protected virtual string GetUrl()
		{
			return String.Empty;
		}
    }
}
