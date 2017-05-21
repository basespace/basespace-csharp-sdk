using System;
using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Types;
using Xunit;

namespace Illumina.BaseSpace.SDK.Tests.Integration
{
    public class UsersTests : BaseIntegrationTest
    {
        [Fact]
        public void CanGetCurrentUser()
        {
            GetUserResponse response = Client.GetUser(new GetUserRequest());
            Assert.NotNull(response);
            User user = response.Response;
            Assert.NotNull(user);
            Assert.True(user.Email.Contains("@"));
            Assert.True(user.DateCreated > new DateTime(2009,1,1));
            Assert.NotNull(user.Id);
        }

        [Fact]
        public void CanGetAccessTokenDetails()
        {            
            var request = new GetAccessTokenDetailsRequest();
            GetAccessTokenDetailsResponse response = Client.GetUserPermissions(request);

            Assert.NotNull(response);

            AccessTokenDetails accessTokenDetails = response.Response;
            Assert.NotNull(accessTokenDetails);
            Assert.NotNull(accessTokenDetails.AccessToken);
            Assert.NotNull(accessTokenDetails.UserResourceOwner);

        }
    }
}
