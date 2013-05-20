using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Tests.Helpers;
using Illumina.BaseSpace.SDK.Types;
using Xunit;

namespace Illumina.BaseSpace.SDK.Tests.Integration
{
    public class AppSessionTests: BaseIntegrationTest
    {
        [Fact]
        public void CanCreateAppSession()
        {
            var appSession = TestHelpers.CreateTestAppSession(Client);
            Assert.True(appSession.Status.Contains(AppSessionStatus.Running.ToString()));
        }

        [Fact]
        public void CanGetAppSessions()
        {
            var appSession = TestHelpers.CreateTestAppSession(Client);      //create app session
            appSession = TestHelpers.CreateTestAppSession(Client);          //create another app session

            ListAppSessionsRequest request = new ListAppSessionsRequest(appSession.UserCreatedBy.Id);

            ListAppSessionsResponse response = Client.ListAppSessions(request);

            //every unit test has this
            Assert.NotNull(response);

            //verify at least two app sessions are returned; the test user may already have app sessions so can't verify on just two without different criteria
            Assert.True(response.Response.Items.Length >= 2);
        }

        [Fact]
        public void CanGetAppSessionById()
        {
            var appSession = TestHelpers.CreateTestAppSession(Client);

            var response = Client.GetAppSession(new GetAppSessionRequest(appSession.Id));
            Assert.NotNull(response);
            var updateAppSession = response.Response;
            Assert.NotNull(updateAppSession);
            Assert.Equal(appSession.Id, updateAppSession.Id);
            Assert.Equal(appSession.Status, updateAppSession.Status);
            Assert.Equal(appSession.StatusSummary, updateAppSession.StatusSummary);
            Assert.Equal(appSession.References.Length, updateAppSession.References.Length);
            Assert.Equal(appSession.OriginatingUri, updateAppSession.OriginatingUri);
            Assert.Equal(appSession.Name, updateAppSession.Name);
        }

        [Fact]
        public void CanUpdateAppSessionStatus()
        {
            var appSession = TestHelpers.CreateTestAppSession(Client);
            Assert.True(appSession.Status.Contains(AppSessionStatus.Running.ToString()));

            var response = Client.UpdateAppSession(new UpdateAppSessionRequest(appSession.Id, AppSessionStatus.Complete.ToString()));
            Assert.NotNull(response);
            var updateAppSession = response.Response;
            Assert.NotNull(updateAppSession);
            Assert.True(updateAppSession.Status.Contains(AppSessionStatus.Complete.ToString()));
        }
    }
}
