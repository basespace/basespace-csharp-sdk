using System;
using System.Text.RegularExpressions;
using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Tests.Helpers;
using Illumina.BaseSpace.SDK.Types;
using Xunit;

namespace Illumina.BaseSpace.SDK.Tests.Integration
{
    public class AppResultTests: BaseIntegrationTest
    {
        [Fact]
        public void CanCreateAppResult()
        {
            var project = TestHelpers.CreateRandomTestProject(Client);
            var appResult = TestHelpers.CreateRandomTestAppResult(Client, project);
            Assert.True(!string.IsNullOrEmpty(appResult.Name));

            Assert.True(string.IsNullOrEmpty(appResult.Description));
            Assert.True(appResult.Status == "Running");// AppSessionStatus.Running); // TODO: use enum value instead of magic value
            Assert.True(string.IsNullOrEmpty(appResult.StatusSummary));

            const string hrefFilesFormatString = @"[A-Za-z0-9]*\/appresults\/{0}\/files";
            string hrefFilesRegexString = string.Format(hrefFilesFormatString, appResult.Id);
            Match match = Regex.Match(appResult.HrefFiles.OriginalString, hrefFilesRegexString, RegexOptions.IgnoreCase);
            Assert.True(match.Success);

            Assert.Null(appResult.HrefGenome);
            Assert.Null(appResult.References);
        }

        [Fact]
        public void CanGetAppResults()
        {
            var project = TestHelpers.CreateRandomTestProject(Client);
            var appResult1 = TestHelpers.CreateRandomTestAppResult(Client, project);
            var appResult2 = TestHelpers.CreateRandomTestAppResult(Client, project);
            var appResult3 = TestHelpers.CreateRandomTestAppResult(Client, project);
            Assert.NotNull(appResult1);
            Assert.NotNull(appResult2);
            Assert.NotNull(appResult3);

            var getProjectRequest = new GetProjectRequest(project.Id);
            var getProjectResponse = Client.GetProject(getProjectRequest);
            Assert.NotNull(getProjectResponse);
            var project2 = getProjectResponse.Response;
            Assert.NotNull(project2);

            var hrefAppResults = project2.HrefAppResults;
            Assert.NotNull(hrefAppResults);
            Assert.Contains(project.Id, project2.Href.ToString());

        }

        [Fact]
        public void CanListAppResults()
        {
            var project = TestHelpers.CreateRandomTestProject(Client);
            var appResult1 = TestHelpers.CreateRandomTestAppResult(Client, project);
            var appResult2 = TestHelpers.CreateRandomTestAppResult(Client, project);
            var appResult3 = TestHelpers.CreateRandomTestAppResult(Client, project);

            var appResultListRequest = new ListAppResultsRequest(project.Id);
            var appResultListResponse = Client.ListAppResults(appResultListRequest, null);
            Assert.NotNull(appResultListResponse);

            var response = appResultListResponse.Response;

            Assert.True(response.Items.Length == 3);
            Assert.True(response.Items[0].Id == appResult1.Id);
            Assert.True(response.Items[1].Id == appResult2.Id);
            Assert.True(response.Items[2].Id == appResult3.Id);
            Assert.True(response.Items[2].Name == appResult3.Name);
            Assert.True(response.Items[2].Href == appResult3.Href);
            Assert.True(response.Items[2].Status == appResult3.Status);
            Assert.True(response.Items[2].StatusSummary == appResult3.StatusSummary);
            Assert.True(response.Items[2].UserOwnedBy.Name == appResult3.UserOwnedBy.Name);
        }

        [Fact]
        public void CanSortAppResults()
        {
            var project = TestHelpers.CreateRandomTestProject(Client);
            var appResult1 = TestHelpers.CreateRandomTestAppResult(Client, project);
            var appResult2 = TestHelpers.CreateRandomTestAppResult(Client, project);
            var appResult3 = TestHelpers.CreateRandomTestAppResult(Client, project);

            var appResultListRequest = new ListAppResultsRequest(project.Id) { Limit = 3, Offset = 0, SortBy = AppResultSortByParameters.DateCreated, SortDir = SortDirection.Desc };
            var appResultListResponse = Client.ListAppResults(appResultListRequest, null);
            Assert.NotNull(appResultListResponse);

            var response = appResultListResponse.Response;

            Assert.True(response.Items.Length == 3);
            Assert.True(response.Items[0].Id == appResult1.Id);
            Assert.True(response.Items[1].Id == appResult2.Id);
            Assert.True(response.Items[2].Id == appResult3.Id);
        }
    }
}
