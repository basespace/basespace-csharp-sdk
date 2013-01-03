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
            User user = response.Response;
            Assert.NotNull(user);
            Assert.True(user.Email.Contains("@"));
            Assert.True(user.DateCreated > new DateTime(2009,1,1));
            Assert.NotNull(user.Id);
        }

        [Fact]
        public void CanGetCurrentUserAsync()
        {
            Task<GetUserResponse> task = client.GetUserAsync(new GetUserRequest());
            task.Wait(TimeSpan.FromMinutes(1));
            var response = task.Result;
            Assert.NotNull(response);
            User user = response.Response;
            Assert.NotNull(user);
            Assert.True(user.Email.Contains("@"));
            Assert.True(user.DateCreated > new DateTime(2009, 1, 1));
            Assert.NotNull(user.Id);
        }

        [Fact]
        public void ParallelAsyncFasterThanSync()
        {
            var synchTimer = new Stopwatch();
            var asynchTimer = new Stopwatch();
            //warm up
             client.GetUser(new GetUserRequest());

            synchTimer.Start();
            //call 5 times
            for (int i = 0; i < 5; i++)
            {
                client.GetUser(new GetUserRequest());
            }
            synchTimer.Stop();

            //we are calling 5 times asynch and adding to a counter when completed
            int counter = 0;
            for (int i = 0; i < 5; i++)
            {
                Task<GetUserResponse> task = client.GetUserAsync(new GetUserRequest());
                task.ContinueWith((t) => counter += 1);
            }
            asynchTimer.Start();
            while(counter < 5 || asynchTimer.ElapsedMilliseconds > 1000 * 60){}
            asynchTimer.Stop();

            Assert.True(synchTimer.ElapsedMilliseconds > asynchTimer.ElapsedMilliseconds);
           
        }
    }
}
