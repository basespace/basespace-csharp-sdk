using System;
using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Types;
using Xunit;

namespace Illumina.BaseSpace.SDK.Tests.Integration
{
    public class AppResultTests: BaseIntegrationTest
    {
        [Fact]
        public void CanCreateAppResult()
        {
            var appResultName = string.Format("SDKUnitTest-AppResult-{0}", StringHelpers.RandomAlphanumericString(10));
            var project = TestHelpers.CreateProject(Client);

            var response = Client.CreateAppResult(new PostAppResultRequest(project.Id, appResultName));
            Assert.NotNull(response);
            var appResult = response.Response;
            Assert.NotNull(appResult);
            Assert.True(appResult.Name.Contains(appResultName));
        }
    }
}
