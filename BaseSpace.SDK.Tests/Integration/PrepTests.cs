using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.ServiceModels.Response;
using Xunit;

namespace Illumina.BaseSpace.SDK.Tests.Integration
{
    public class PrepTests : BaseIntegrationTest
    {
        [Fact]
        public void CanGetSampleLibrariesFromRunId()
        {
            ListSampleLibrariesResponse listSampleLibrariesResponse =
                Client.ListSampleLibrariesFromRun(new ListSampleLibrariesFromRunRequest("2009"));

            Assert.True(listSampleLibrariesResponse.Response.Items.Length == 2);

            var sampleLibrary01 = listSampleLibrariesResponse.Response.Items[0];
            var sampleLibrary02 = listSampleLibrariesResponse.Response.Items[1];

            Assert.True(sampleLibrary01.ContainerPosition == "0");
            Assert.True(sampleLibrary01.BiologicalSample.Name == "DVT4");
            Assert.True(sampleLibrary01.LibraryName == "test1111");
            Assert.True(sampleLibrary01.ContainerEndPosition == null);

            Assert.True(sampleLibrary02.ContainerPosition == "1");
            Assert.True(sampleLibrary02.BiologicalSample.Name == "DVT8");
            Assert.True(sampleLibrary02.LibraryName == "test2222");
            Assert.True(sampleLibrary02.ContainerEndPosition == "1L");
        }
    }
}
