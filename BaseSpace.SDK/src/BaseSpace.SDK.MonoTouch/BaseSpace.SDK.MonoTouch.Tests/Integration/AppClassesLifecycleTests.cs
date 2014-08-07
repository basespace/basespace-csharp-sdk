﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Illumina.BaseSpace.SDK;
using Illumina.BaseSpace.SDK.Types;
using Illumina.BaseSpace.SDK.MonoTouch.Tests.Helpers;
using NUnit.Framework;

namespace Illumina.BaseSpace.SDK.MonoTouch.Tests.Integration
{
    [TestFixture]
    public class AppClassesLifecycleTests : BaseIntegrationTest
    {
        [Test]
        public void RunTypicalAppLifecycleTest()
        {
            // create a project
            var project = TestHelpers.CreateRandomTestProject(Client);

            // create a samples collector in the project
            var sample = TestHelpers.CreateRandomTestSample(Client, project);

            // create an appresult and associate it with a new appsession
            var appresult = TestHelpers.CreateRandomTestAppResult(Client, project);

            // upload a file to the appresult

            // verify the file is associated to both the samples and appresults collectors


        }
    }
}
