using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumina.BaseSpace.SDK.ServiceModels;
using Xunit;

namespace Illumina.BaseSpace.SDK.Tests
{
    public class UsersTests : IntegrationTests
    {
        private readonly IBaseSpaceClient client;

        public UsersTests()
        {
            client = CreateRealClient();
        }
        [Fact]
        public void CanGetCurrentUser()
        {
            GetUserResponse response = client.GetUser(new GetUserRequest());
            Assert.NotNull(response);
        }
    }
}
