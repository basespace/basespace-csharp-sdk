using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Tests.Helpers;
using Illumina.BaseSpace.SDK.Types;
using Xunit;

namespace Illumina.BaseSpace.SDK.Tests.Integration
{
    public class PropertyTests : BaseIntegrationTest
    {
        [Fact]
        public void Foo()
        {
            var project = Client.GetProject(new GetProjectRequest("356356")).Response;
            var projectProperties = project.Properties;

            var setPropRequest = new SetPropertiesRequest(project);
            setPropRequest.AddProperty("SDK.foo1").SetSingleValueContent("Fooo1");
            setPropRequest.AddProperty("SDK.foo2").SetSingleValueContent("Fooo2");
            try
            {
                var propResponse = Client.SetProperties(setPropRequest);
            }
            catch (BaseSpaceException x)
            {
                string xToString = x.ToString();
                string msg = x.Message;
            }
        }
    }
}
