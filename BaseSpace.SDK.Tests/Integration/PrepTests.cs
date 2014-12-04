using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumina.BaseSpace.SDK.Tests.Helpers;
using Xunit;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.Tests.Integration
{
    public class PrepTests : BaseIntegrationTest
    {
        [Fact]
        public void CreatePlannedRun()
        {   
            var libraryIds = new List<string>();
            var container = TestHelpers.CreateContainerLibraries(Client, 1);
            var sampleLibraries = TestHelpers.GetSampleLibrariesInContainer(Client, container.Id);
            foreach(var sampleLibrary in sampleLibraries)
                libraryIds.Add(sampleLibrary.Id);

            var pool = TestHelpers.CreateLibraryPoolWithMapping(Client, libraryIds);
            var poolList = new List<PlannedRunPoolMapping>() { new PlannedRunPoolMapping() {PoolId = Convert.ToInt64(pool.Id), Lane = "1"}};
            var plannedRun = TestHelpers.CreatePlannedRun(Client, poolList, 36);
            TestHelpers.MarkRunAsReadyToSequence(Client, plannedRun.Id);

        }

        [Fact]
        public void CreateBiologicalSample()
        {
            var biologicalSample = TestHelpers.CreateBiologicalSample(Client);
        }
    }
}

