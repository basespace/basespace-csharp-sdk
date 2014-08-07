using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Types;
using NUnit.Framework;

namespace Illumina.BaseSpace.SDK.MonoTouch.Tests.Integration
{
    [TestFixture]
    public class UsersTests : BaseIntegrationTest
    {
        [Test]
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

        [Test]
        public void CanGetCurrentUserAsync()
        {
            Task<GetUserResponse> task = Client.GetUserAsync(new GetUserRequest());
            task.Wait(TimeSpan.FromMinutes(1));
            var response = task.Result;
            Assert.NotNull(response);
            User user = response.Response;
            Assert.NotNull(user);
            Assert.True(user.Email.Contains("@"));
            Assert.True(user.DateCreated > new DateTime(2009, 1, 1));
            Assert.NotNull(user.Id);
        }

        [Test]
        public void ParallelAsyncFasterThanSync()
        {
            var synchTimer = new Stopwatch();
            var asynchTimer = new Stopwatch();
            //warm up
            Client.GetUser(new GetUserRequest());

            synchTimer.Start();
            //call 5 times
            for (int i = 0; i < 5; i++)
            {
                Client.GetUser(new GetUserRequest());
            }
            synchTimer.Stop();

            //we are calling 5 times asynch and adding to a counter when completed
            int counter = 0;
            for (int i = 0; i < 5; i++)
            {
                Task<GetUserResponse> task = Client.GetUserAsync(new GetUserRequest());
                task.ContinueWith((t) => counter += 1);
            }
            asynchTimer.Start();
            while(counter < 5 || asynchTimer.ElapsedMilliseconds > 1000 * 60){}
            asynchTimer.Stop();

            Assert.True(synchTimer.ElapsedMilliseconds > asynchTimer.ElapsedMilliseconds);

        }
    }
}
