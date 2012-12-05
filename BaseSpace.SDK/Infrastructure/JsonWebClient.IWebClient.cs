using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Illumina.BaseSpace.SDK
{
    public partial class JsonWebClient : IWebClient
    {
        public void SetDefaultRequestOptions(IRequestOptions options)
        {
            DefaultRequestOptions = options;
        }

        public TResponse Send<TResponse>(HttpMethods httpMethod, string relativeOrAbsoluteUrl, object request,
                                         IRequestOptions options = null) where TResponse : class
        {
            var rr = new RestRequest()
            {
                Method = httpMethod,
                RelativeOrAbsoluteUrl = relativeOrAbsoluteUrl,
                Request = request,
                Options = options ?? DefaultRequestOptions
            };
            return Execute<TResponse>(Client, rr, Logger);
        }

        public Task<TResponse> SendAsync<TResponse>(HttpMethods httpMethod, string relativeOrAbsoluteUrl, object request,
                                         IRequestOptions options = null) where TResponse : class
        {
            var rr = new RestRequest()
            {
                Method = httpMethod,
                RelativeOrAbsoluteUrl = relativeOrAbsoluteUrl,
                Request = request,
                Options = options ?? DefaultRequestOptions
            };
            return ExecuteTask<TResponse>(Client, rr, Logger);
        }

        public TResponse PostFileWithRequest<TResponse>(string relativeOrAbsoluteUrl, FileInfo fileToUpload,
                                                        object request,
                                                        IRequestOptions options = null) where TResponse : class
        {
            var rr = new FileRestRequest()
            {
                Method = HttpMethods.PUT,
                RelativeOrAbsoluteUrl = relativeOrAbsoluteUrl,
                Request = request,
                FileInfo = fileToUpload,
                Options = options ?? DefaultRequestOptions
            };
            return Execute<TResponse>(Client, rr, Logger);
        }

        public Task<TResponse> PostFileWithRequestAsync<TResponse>(string relativeOrAbsoluteUrl, FileInfo fileToUpload,
                                                        object request,
                                                        IRequestOptions options = null) where TResponse : class
        {
            var rr = new FileRestRequest()
            {
                Method = HttpMethods.PUT,
                RelativeOrAbsoluteUrl = relativeOrAbsoluteUrl,
                Request = request,
                FileInfo = fileToUpload,
                Options = options ?? DefaultRequestOptions
            };
            return ExecuteTask<TResponse>(Client, rr, Logger);
        }

        public TResponse PostFileWithRequest<TResponse>(string relativeOrAbsoluteUrl, Stream fileToUpload,
                                                        object request, string fileName,
                                                        IRequestOptions options = null) where TResponse : class
        {
            var rr = new StreamingRestRequest()
            {
                Method = HttpMethods.PUT,
                RelativeOrAbsoluteUrl = relativeOrAbsoluteUrl,
                Request = request,
                Stream = fileToUpload,
                FileName = fileName,
                Options = options ?? DefaultRequestOptions
            };
            return Execute<TResponse>(Client, rr, Logger);
        }

        public Task<TResponse> PostFileWithRequestAsync<TResponse>(string relativeOrAbsoluteUrl, Stream fileToUpload,
                                                       object request, string fileName,
                                                       IRequestOptions options = null) where TResponse : class
        {
            var rr = new StreamingRestRequest()
            {
                Method = HttpMethods.PUT,
                RelativeOrAbsoluteUrl = relativeOrAbsoluteUrl,
                Request = request,
                Stream = fileToUpload,
                FileName = fileName,
                Options = options ?? DefaultRequestOptions
            };
            return ExecuteTask<TResponse>(Client, rr, Logger);
        }
    }
}
