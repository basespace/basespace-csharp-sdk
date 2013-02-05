using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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

        /// <summary>
        /// Send a HTTP request
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="httpMethod"></param>
        /// <param name="relativeOrAbsoluteUrl"></param>
        /// <param name="request"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        TResponse Send<TResponse>(HttpMethods httpMethod, string relativeOrAbsoluteUrl, object request, IRequestOptions options = null) where TResponse : class;

        Task<TResponse> SendAsync<TResponse>(HttpMethods httpMethod, string relativeOrAbsoluteUrl, object request, IRequestOptions options = null) where TResponse : class;

        /// <summary>
        /// Post a file
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="relativeOrAbsoluteUrl"></param>
        /// <param name="fileToUpload"></param>
        /// <param name="request"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        TResponse PostFileWithRequest<TResponse>(string relativeOrAbsoluteUrl, FileInfo fileToUpload, object request, IRequestOptions options = null) where TResponse : class;

        Task<TResponse> PostFileWithRequestAsync<TResponse>(string relativeOrAbsoluteUrl, FileInfo fileToUpload, object request, IRequestOptions options = null) where TResponse : class;

        /// <summary>
        /// Post a stream
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="relativeOrAbsoluteUrl"></param>
        /// <param name="fileToUpload"></param>
        /// <param name="request"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        TResponse PostFileWithRequest<TResponse>(string relativeOrAbsoluteUrl, Stream fileToUpload, object request, string fileName, IRequestOptions options = null) where TResponse : class;

        Task<TResponse> PostFileWithRequestAsync<TResponse>(string relativeOrAbsoluteUrl, Stream fileToUpload, object request, string fileName, IRequestOptions options = null) where TResponse : class;


    }
}
