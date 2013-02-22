using System;
using System.IO;
using System.Net;
using Common.Logging;
using ServiceStack.ServiceClient.Web;

namespace Illumina.BaseSpace.SDK
{
	internal class RestRequest<TReturn>
		where TReturn : class
	{
		public string Name { get; set; }

		public string RelativeOrAbsoluteUrl { get; set; }

		public HttpMethods Method { get; set; }

		public object Request { get; set; }

		public IRequestOptions Options { get; set; }

		public IRequestOptions DefaultRequestOptions { get; protected set; }

		protected JsonWebClient Client { get; private set; }

		public TReturn Send(JsonServiceClient client, ILog log)
		{
			return WrapResult(GetFunc(client), log, Options.RetryAttempts, Name);
		}

		protected virtual Func<TReturn> GetFunc(JsonServiceClient client)
		{
			return () => client.Send<TReturn>(Method.ToString(), RelativeOrAbsoluteUrl, Request);
		}

		private static TReturn WrapResult<TReturn>(Func<TReturn> func, ILog logger, uint maxRetry, string name)
			where TReturn : class
		{
			try
			{
				TReturn result = null;
				RetryLogic.DoWithRetry(maxRetry, name, () => { result = func(); }, logger);
				return result;
			}
			catch (WebServiceException wex)
			{
				throw new BaseSpaceException<TReturn>(name + " failed", wex);
			}
		}
	}

	internal class FileRestRequest<TReturn> : RestRequest<TReturn>
		where TReturn : class
	{
		public FileInfo FileInfo { get; set; }

		protected override Func<TReturn> GetFunc(JsonServiceClient client)
		{
			return () => Client.PostFileWithRequest<TReturn>(RelativeOrAbsoluteUrl, FileInfo, Request);
		}
	}

	internal class StreamingRestRequest<TReturn> : RestRequest<TReturn>
		where TReturn : class
	{
		public Stream Stream { get; set; }

		public string FileName { get; set; }

		protected override Func<TReturn> GetFunc(JsonServiceClient client)
		{
			return () => client.PostFileWithRequest<TReturn>(RelativeOrAbsoluteUrl, Stream, FileName, Request);
		}
	}
}
