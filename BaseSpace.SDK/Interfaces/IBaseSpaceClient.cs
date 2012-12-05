using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Illumina.BaseSpace.SDK.Models;

namespace Illumina.BaseSpace.SDK
{
    public interface IBaseSpaceClient
    {
        Task<GetUserResponse> GetUserAsync(GetUserRequest request, IRequestOptions options);

        GetUserResponse GetUser(GetUserRequest request, IRequestOptions options);

        void SetDefaultRequestOptions(IRequestOptions options);



    }
}
