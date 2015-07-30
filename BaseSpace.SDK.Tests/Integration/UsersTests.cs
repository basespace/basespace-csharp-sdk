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
        public void CanGetCUrrentUserPermissions()
        {            
            var request = new GetUserPermissionRequest();
            GetUserPermissionResponse response = Client.GetUserPermissions(request);

            Assert.NotNull(response);

            UserPermission userPermission = response.Response;
            Assert.NotNull(userPermission);
            Assert.NotNull(userPermission.AccessToken);
            Assert.NotNull(userPermission.UserResourceOwner);

        }
    }
}
