using Xunit;

namespace Illumina.BaseSpace.SDK.Tests
{
    public class BaseSpaceClientSettingsTests
    {
        [Fact]
        public void CanGetDefaultUserAgent()
        {
            var settings = new BaseSpaceClientSettings();

            Assert.True(!string.IsNullOrWhiteSpace(settings.UserAgent));
        }
    }
}
