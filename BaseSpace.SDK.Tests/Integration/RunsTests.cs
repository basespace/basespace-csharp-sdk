using System.Text.RegularExpressions;
using Illumina.BaseSpace.SDK.Tests.Helpers;
using Xunit;
using System;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.Tests.Integration
{
    public class RunsTests : BaseIntegrationTest
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
        public void CanListRunsByDateModified()
        {
            var runs = Client.ListRuns(new ServiceModels.ListRunsRequest
                {
                    SortBy = RunSortByParameters.DateModified,
                    SortDir = SortDirection.Desc
                });
            Assert.NotNull(runs);
            Assert.NotNull(runs.Response);
            Assert.NotNull(runs.Response.Items);
            foreach (var run in runs.Response.Items)
            {
                Assert.True(run.DateCreated > DateTime.MinValue);
                Assert.True(run.DateModified > DateTime.MinValue);
                Assert.NotNull(run.Id);
                Assert.NotNull(run.Href);
                Assert.NotNull(run.ExperimentName);
                Assert.NotNull(run.Status);
                Assert.NotNull(run.UserOwnedBy);
            }

        }


    }
}
