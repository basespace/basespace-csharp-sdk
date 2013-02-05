using System.Text.RegularExpressions;
using Illumina.BaseSpace.SDK.Tests.Helpers;
using Xunit;

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

    }
}
