using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Tests.Helpers;
using Illumina.BaseSpace.SDK.Types;
using Xunit;

namespace Illumina.BaseSpace.SDK.Tests.Integration
{
    public class AppSessionTests: BaseIntegrationTest
    {
        [Fact]
        public void CanUpdateAppSessionStatus()
        {
            var project = TestHelpers.CreateRandomTestProject(Client);
            var appResult = TestHelpers.CreateRandomTestAppResult(Client, project);
            var appSession = appResult.AppSession;
            var response = Client.UpdateAppSession(new UpdateAppSessionRequest(appSession.Id, AppSessionStatus.Complete.ToString()));
            Assert.NotNull(response);
            var updateAppSession = response.Response;
            Assert.NotNull(updateAppSession);
            Assert.True(updateAppSession.Status.Contains(AppSessionStatus.Complete.ToString()));
        }
    }
}
