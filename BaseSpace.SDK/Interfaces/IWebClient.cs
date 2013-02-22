using Illumina.BaseSpace.SDK.ServiceModels;

namespace Illumina.BaseSpace.SDK
{
    public interface IWebClient
    {
        /// <summary>
        /// Change the default request options for this instance of the client
        /// </summary>
        /// <param name="options"></param>
        void SetDefaultRequestOptions(IRequestOptions options);
        
        IRequestOptions DefaultRequestOptions { get; }

		TReturn Send<TReturn>(AbstractRequest<TReturn> request, IRequestOptions options = null)
			where TReturn : class;

		///// <summary>
		///// Send a HTTP request
		///// </summary>
		///// <typeparam name="TResponse"></typeparam>
		///// <param name="httpMethod"></param>
		///// <param name="relativeOrAbsoluteUrl"></param>
		///// <param name="request"></param>
		///// <param name="options"></param>
		///// <returns></returns>
		//TReturn Send<TReturn>(HttpMethods httpMethod, string relativeOrAbsoluteUrl, object request, IRequestOptions options = null)
		//	where TReturn : class;

		///// <summary>
		///// Post a file
		///// </summary>
		///// <typeparam name="TResponse"></typeparam>
		///// <param name="relativeOrAbsoluteUrl"></param>
		///// <param name="fileToUpload"></param>
		///// <param name="request"></param>
		///// <param name="options"></param>
		///// <returns></returns>
		//TReturn PostFileWithRequest<TReturn>(string relativeOrAbsoluteUrl, FileInfo fileToUpload, object request, IRequestOptions options = null)
		//	where TReturn : class;




	    ///// <summary>
	    ///// Post a stream
	    ///// </summary>
	    ///// <typeparam name="TResponse"></typeparam>
	    ///// <param name="relativeOrAbsoluteUrl"></param>
	    ///// <param name="fileToUpload"></param>
	    ///// <param name="request"></param>
	    ///// <param name="options"></param>
	    ///// <returns></returns>
	    //TReturn PostFileWithRequest<TReturn>(string relativeOrAbsoluteUrl, Stream fileToUpload, object request, string fileName, IRequestOptions options = null)
	    //	where TReturn : class;


    }
}
