using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
