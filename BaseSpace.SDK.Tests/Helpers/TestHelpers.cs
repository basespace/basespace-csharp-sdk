using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Types;
using Xunit;

namespace Illumina.BaseSpace.SDK.Tests.Integration
{
    public class TestHelpers : BaseIntegrationTest
    {
        public static Project CreateProject(IBaseSpaceClient client, string name = null)
        {
            name = name ?? string.Format("SDKUnitTest-Project-{0}", StringHelpers.RandomAlphanumericString(10));
            var response = client.CreateProject(new PostProjectRequest(name));
            Assert.NotNull(response);
            
            var project = response.Response;
            Assert.NotNull(project);
            
            return project;
        }

        public static AppResult CreateAppResult(IBaseSpaceClient client, string projectId = null, string appResultName = null )
        {
            appResultName = appResultName ?? string.Format("SDKUnitTest-AppResult-{0}", StringHelpers.RandomAlphanumericString(10));
            projectId = projectId ?? (CreateProject(client)).Id;

            var response = client.CreateAppResult(new PostAppResultRequest(projectId, appResultName));
            Assert.NotNull(response);
            var appResult = response.Response;
            Assert.NotNull(appResult);
            return appResult;
        }
    }
}
