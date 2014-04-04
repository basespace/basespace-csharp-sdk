using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.ServiceClient.Web;

namespace Illumina.BaseSpace.SDK
{
    public static class JsonRestClientAsyncExtensions
    {
        public static void SendAsync<TResult>(this JsonRestClientAsync client, HttpMethods method, string relativeOrAbsoluteUrl, Action<TResult> onSuccess, Action<TResult, Exception> onError, object request = null)
        {
            switch (method)
            {
                case HttpMethods.GET:
                    client.GetAsync(relativeOrAbsoluteUrl, onSuccess, onError);
                    break;
                case HttpMethods.PUT:
                    client.PutAsync(relativeOrAbsoluteUrl, request, onSuccess, onError);
                    break;
                case HttpMethods.DELETE:
                    client.DeleteAsync(relativeOrAbsoluteUrl,  onSuccess, onError);
                    break;
                case HttpMethods.POST:
                    client.PostAsync(relativeOrAbsoluteUrl, request, onSuccess, onError);
                    break;
              
                default:
                    throw new ArgumentOutOfRangeException("HttpMethod not in use");
            }
        }
    }
}
