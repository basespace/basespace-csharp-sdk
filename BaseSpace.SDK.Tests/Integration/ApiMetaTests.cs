using System.Text.RegularExpressions;
using Illumina.BaseSpace.SDK.ServiceModels;
using Xunit;

namespace Illumina.BaseSpace.SDK.Tests.Integration
{
    public class ApiMetaTests : BaseIntegrationTest
    {
        [Fact]
        public void CanGetApiMeta()
        {
            var response = Client.GetApiMeta(new GetApiMetaRequest());
            Assert.NotNull(response);
            var meta = response.Response;

            const string buildNumberFormat = @"[0-9]+\.[0-9]+\.[0-9]+\.[0-9]";
            var match = Regex.Match(meta.Build, buildNumberFormat, RegexOptions.IgnoreCase);
            Assert.True(match.Success);
        }
    }
}
