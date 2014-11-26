using System.Linq;
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

        [Fact]
        public void CanListAppSessions()
        {
            var appSessionsList = Client.ListAppSessions(new ListAppSessionsRequest()).Response;
            Assert.True(appSessionsList.Items.Any());
        }
    }
}
