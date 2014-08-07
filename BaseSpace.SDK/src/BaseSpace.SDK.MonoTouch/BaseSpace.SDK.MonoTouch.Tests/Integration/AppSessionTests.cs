using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.MonoTouch.Tests.Helpers;
using Illumina.BaseSpace.SDK.Types;
using NUnit.Framework;

namespace Illumina.BaseSpace.SDK.MonoTouch.Tests.Integration
{
    [TestFixture]
    public class AppSessionTests: BaseIntegrationTest
    {
        [Test]
        public void CanCreateAppSession()
        {
            var appSession = TestHelpers.CreateTestAppSession(Client);
            Assert.True(appSession.Status.Contains(AppSessionStatus.Running.ToString()));
        }

        [Test]
        public void CanGetAppSessionById()
        {
            var appSession = TestHelpers.CreateTestAppSession(Client);

            var response = Client.GetAppSession(new GetAppSessionRequest(appSession.Id));
            Assert.NotNull(response);
            var updateAppSession = response.Response;
            Assert.NotNull(updateAppSession);
            Assert.AreEqual(appSession.Id, updateAppSession.Id);
            Assert.AreEqual(appSession.Status, updateAppSession.Status);
            Assert.AreEqual(appSession.StatusSummary, updateAppSession.StatusSummary);
            Assert.AreEqual(appSession.References.Length, updateAppSession.References.Length);
            Assert.AreEqual(appSession.OriginatingUri, updateAppSession.OriginatingUri);
        }

        [Test]
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
