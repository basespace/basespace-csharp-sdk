using System;
using ServiceStack.ServiceClient.Web;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
	public abstract class AbstractRequest<TReturn>
		where TReturn : class
	{
		protected string DefaultHttpMethod { get; private set; }

		protected string Version { get; private set; }

		internal Func<TReturn> GetFunc(JsonServiceClient client, IRequestOptions options)
		{
			string httpMethod = options.HttpMethod ?? DefaultHttpMethod;

			return () => client.Send<TReturn>(httpMethod, GetUrl(), (httpMethod == "GET") ? null : this);
		}

		internal string GetName()
		{
			return String.Format("{0} request to {1} ", DefaultHttpMethod, GetUrl());
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
		{
			Id = id;
		}

        public string Id { get; set; }

		//protected string DefaultHttpMethod { get; private set; }

		//internal Func<TReturn> GetFunc(JsonServiceClient client, IRequestOptions options)
		//{
		//	string httpMethod = options.HttpMethod ?? DefaultHttpMethod;

		//	return () => client.Send<TReturn>(httpMethod, GetUrl(), (httpMethod == "GET") ? null : this);
		//}

		//internal string GetName()
		//{
		//	return String.Format("{0} request to {1} ", DefaultHttpMethod, GetUrl());
		//}

		//protected virtual string GetUrl()
		//{
		//	return String.Empty;
		//}
    }
}
