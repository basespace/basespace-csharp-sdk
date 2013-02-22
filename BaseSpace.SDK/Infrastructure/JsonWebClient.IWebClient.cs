using System.IO;
using System.Threading.Tasks;

namespace Illumina.BaseSpace.SDK
{
    public partial class JsonWebClient : IWebClient
    {
        public void SetDefaultRequestOptions(IRequestOptions options)
        {
            DefaultRequestOptions = options;
        }

        public TReturn Send<TReturn>(HttpMethods httpMethod, string relativeOrAbsoluteUrl, object request,
                                         IRequestOptions options = null)
            where TReturn : class
        {
            var rr = new RestRequest()
            {
                Method = httpMethod,
                RelativeOrAbsoluteUrl = relativeOrAbsoluteUrl,
                Request = request,
                Options = options ?? DefaultRequestOptions,
                Name = string.Format("{0} request to {1} ", httpMethod, relativeOrAbsoluteUrl)
            };
            return Execute<TReturn>(Client, rr, Logger);
        }


        public TReturn PostFileWithRequest<TReturn>(string relativeOrAbsoluteUrl, FileInfo fileToUpload,
                                                        object request,
                                                        IRequestOptions options = null) 
            where TReturn : class
        {
            var rr = new FileRestRequest()
            {
                Method = HttpMethods.PUT,
                RelativeOrAbsoluteUrl = relativeOrAbsoluteUrl,
                Request = request,
                FileInfo = fileToUpload,
                Options = options ?? DefaultRequestOptions,
                Name = string.Format("File Put request to {0} for file {1}", relativeOrAbsoluteUrl, fileToUpload.FullName)
            };
            return Execute<TReturn>(Client, rr, Logger);
        }

        public TReturn PostFileWithRequest<TReturn>(string relativeOrAbsoluteUrl, Stream fileToUpload,
                                                        object request, string fileName,
                                                        IRequestOptions options = null)
            where TReturn : class
        {
            var rr = new StreamingRestRequest()
            {
                Method = HttpMethods.PUT,
                RelativeOrAbsoluteUrl = relativeOrAbsoluteUrl,
                Request = request,
                Stream = fileToUpload,
                FileName = fileName,
                Options = options ?? DefaultRequestOptions,
                Name = string.Format("File put request to {0} from stream with file name {1} ", relativeOrAbsoluteUrl, fileName)
            };
            return Execute<TReturn>(Client, rr, Logger);
        }
    }
}
