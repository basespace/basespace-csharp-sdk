using System;
using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Tests.Integration;
using Illumina.BaseSpace.SDK.Types;
using Xunit;

namespace Illumina.BaseSpace.SDK.Tests.Helpers
{
    public class TestHelpers : BaseIntegrationTest
    {
        #region Project
        private const string TestProjectNameFormatString = "SDK_Test_Project-{0}";
        private static string CreateRandomTestProjectName() { return string.Format(TestProjectNameFormatString, StringHelpers.RandomAlphanumericString(5)); }
        public static Project CreateRandomTestProject(IBaseSpaceClient client, string projectName = null)
        {
            projectName = projectName ?? CreateRandomTestProjectName();
            var response = client.CreateProject(new CreateProjectRequest(projectName));
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseStatus);
            Assert.Null(response.ResponseStatus.ErrorCode);

            Assert.NotNull(response.Response);
            var project = response.Response;
            Assert.NotNull(project);
            Assert.True(project.Name.Contains(projectName));
            return project;
        }
        #endregion Project

        #region AppResult
        private const string TestAppResultNameFormatString = "SDK_Test_AppResult-{0}";
        private static string CreateRandomTestAppResultName() { return string.Format(TestAppResultNameFormatString, StringHelpers.RandomAlphanumericString(5)); }
        public static AppResult CreateRandomTestAppResult(IBaseSpaceClient client, Project project)
        {
            var appResultName = CreateRandomTestAppResultName();
            var response = client.CreateAppResult(new CreateAppResultRequest(project.Id, appResultName));
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseStatus);
            Assert.Null(response.ResponseStatus.ErrorCode);

            Assert.NotNull(response.Response);
            var appResult = response.Response;
            Assert.NotNull(appResult);
            Assert.True(appResult.Name.Contains(appResultName));
            return appResult;
        }
        #endregion AppResult

        #region AppSession
        internal static AppSession CreateTestAppSession(IBaseSpaceClient client)
        {
            var project = TestHelpers.CreateRandomTestProject(client);
            var appResult = TestHelpers.CreateRandomTestAppResult(client, project);
            var appSessionCompact = appResult.AppSession;
            Assert.NotNull(appSessionCompact);
            var appSessionResponse = client.GetAppSession(new GetAppSessionRequest(appSessionCompact.Id));
            Assert.NotNull(appSessionResponse);
            var appSession = appSessionResponse.Response;
            Assert.NotNull(appSession);

            return appSession;
        }
        #endregion Session
    }
}
