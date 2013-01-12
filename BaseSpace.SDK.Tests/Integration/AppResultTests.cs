using System;
using System.Text.RegularExpressions;
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
            string appResultName;
            var appResult = CreateAppResult(out appResultName);
            Assert.True(appResult.Name.Contains(appResultName));

            Assert.True(string.IsNullOrEmpty(appResult.Description));
            Assert.True(appResult.Status == "Running");// AppSessionStatus.Running); // TODO: use enum value instead of magic value
            Assert.True(string.IsNullOrEmpty(appResult.StatusSummary));

            string HrefFilesFormatString = @"[A-Za-z0-9]*\/appresults\/{0}\/files";
            string HrefFilesRegexString = string.Format(HrefFilesFormatString, appResult.Id);
            Match match = Regex.Match(appResult.HrefFiles.OriginalString, HrefFilesRegexString, RegexOptions.IgnoreCase);
            Assert.True(match.Success);

            Assert.Null(appResult.HrefGenome);
            Assert.Null(appResult.References);
        }

        [Fact]
        public void CanFindAppResults()
        {
            string appResultName;
            var appResult1a = CreateAppResult(out appResultName);
            var appResult1b = CreateAppResult(out appResultName);

            var appResultRequest = new GetAppResultRequest(appResult1b.Id);
            var appResultResponse = Client.GetAppResult(appResultRequest);
            Assert.NotNull(appResultResponse);
            var appResult2 = appResultResponse.Response;
            Assert.NotNull(appResult2);

            Assert.True(appResult1b.Id           == appResult2.Id);
            Assert.True(appResult1b.Name         == appResult2.Name);
            Assert.True(appResult1b.Description  == appResult2.Description);
            Assert.True(appResult1b.DateCreated  != appResult2.DateCreated); // the containers must have different creation times
            Assert.True(appResult1b.Href         == appResult2.Href);
            Assert.True(appResult1b.HrefFiles    == appResult2.HrefFiles);
            Assert.Null(appResult1b.References);
            Assert.NotNull(appResult2.References); // a default value should have been assigned
            Assert.True(appResult2.References.Length == 0);
        }

        [Fact]
        public void CanFindAppResultsByProject()
        {
            var project = TestHelpers.CreateProject(Client);

            string appResultName;
            var appResult1a = CreateAppResult(out appResultName, project);
            var appResult1b = CreateAppResult(out appResultName, project);

            var getProjectRequest = new GetProjectRequest(project.Id);
            var getProjectResponse = Client.GetProject(getProjectRequest);
            Assert.NotNull(getProjectResponse);
            project = getProjectResponse.Response;
            Assert.NotNull(project);

            // TODO: acquire the list of appresults from the project and compare them to the inpu
        }

        [Fact]
        public void CanListAppResults()
        {
            var project = TestHelpers.CreateProject(Client);

            string appResultName;
            var appResult1a = CreateAppResult(out appResultName, project);
            var appResult1b = CreateAppResult(out appResultName, project);
            var appResult1c = CreateAppResult(out appResultName, project);

            var appResultListRequest = new ListAppResultsRequest(project.Id);
            var appResultListResponse = Client.ListAppResults(appResultListRequest, null);
            Assert.NotNull(appResultListResponse);

            var response = appResultListResponse.Response;

            Assert.True(response.Items.Length == 3);
            Assert.True(response.Items[0].Id == appResult1a.Id);
            Assert.True(response.Items[1].Id == appResult1b.Id);
            Assert.True(response.Items[2].Id == appResult1c.Id);
            Assert.True(response.Items[2].Name == appResult1c.Name);
            Assert.True(response.Items[2].Href == appResult1c.Href);
            Assert.True(response.Items[2].Status == appResult1c.Status);
            Assert.True(response.Items[2].StatusSummary == appResult1c.StatusSummary);
            Assert.True(response.Items[2].UserOwnedBy.Name == appResult1c.UserOwnedBy.Name);
        }

        private AppResult CreateAppResult(out string appResultName, Project projectIn=null)
        {
            appResultName = string.Format("SDKUnitTest-AppResult-{0}", StringHelpers.RandomAlphanumericString(10));
            var project = projectIn ?? TestHelpers.CreateProject(Client);

            var response = Client.CreateAppResult(new PostAppResultRequest(project.Id, appResultName));
            Assert.NotNull(response);
            var appResult2 = response.Response;
            Assert.NotNull(appResult2);
            return appResult2;
        }
    }
}
