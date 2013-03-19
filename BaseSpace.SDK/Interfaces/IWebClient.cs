using System.Net;
using Illumina.BaseSpace.SDK.ServiceModels;

namespace Illumina.BaseSpace.SDK
{
    public interface IWebClient
    {
		IRequestOptions DefaultRequestOptions { get; set; }

        IWebProxy WebProxy { get; set; }

        TReturn Send<TReturn>(AbstractRequest<TReturn> request, IRequestOptions options = null)
			where TReturn : class;
    }
}
