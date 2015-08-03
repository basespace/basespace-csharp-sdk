using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Types;
using Xunit;

namespace Illumina.BaseSpace.SDK.Tests.Integration
{
    public class SearchTest : BaseIntegrationTest
    {
        [Fact]
        public void SearchApiTest()
        {
            //Assume you have project BaseSpaceDemo in your account
            var searchRequest = new SearchRequest(query: "project.name:BaseSpaceDemo", scope: "appresult_files");
            var response = Client.Search(searchRequest);

            Assert.NotNull(response); 
            Assert.NotNull(response.Response);
            Assert.NotNull(response.Response.Items);
        }
    }
}
