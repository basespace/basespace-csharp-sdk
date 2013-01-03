using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumina.BaseSpace.SDK.Tests.Integration
{
    public class RunsTests : BaseIntegrationTest
    {
        private readonly IBaseSpaceClient client;

        public RunsTests()
        {
            client = CreateRealClient();
        }


    }
}
