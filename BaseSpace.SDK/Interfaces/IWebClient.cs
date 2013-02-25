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
    }
}
