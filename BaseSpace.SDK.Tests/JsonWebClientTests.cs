using System;
using Illumina.BaseSpace.SDK.Deserialization;
using Illumina.BaseSpace.SDK.Types;
using Xunit;

namespace Illumina.BaseSpace.SDK.Tests
{

    public class JsonWebClientTests
    {
        [Fact]
        public void CanDeserializeARunCompactReference()
        {

            var AppResultReference = @"{
                ""Rel"": ""UsedBy"",
                ""Type"": ""AppResult"",
                ""Href"": ""v1pre3/appresults/291294"",
                ""HrefContent"": ""v1pre3/appresults/291294"",
                ""Content"": {
                    ""Id"": ""291294"",
                    ""Href"": ""v1pre3/appresults/291294"",
                    ""UserOwnedBy"": {
                        ""Id"": ""1001"",
                        ""Href"": ""v1pre3/users/1001"",
                        ""Name"": ""Illumina Inc""
                    },
                    ""Name"": ""BWA GATK - HiSeq 2500 NA12878 demo 2x150"",
                    ""Status"": ""Complete"",
                    ""StatusSummary"": """",
                    ""DateCreated"": ""2012-11-15T19:24:21.0000000""
                }";


            var result = ReferenceDeserializer.JsonToReference(AppResultReference) as ContentReference<AppResult>;
            Assert.NotNull(result);
            Assert.True(result.Href.ToString() == @"v1pre3/appresults/291294");
            Assert.True(result.HrefContent.ToString() == @"v1pre3/appresults/291294");
            Assert.True(result.Rel == @"UsedBy");
            Assert.True(result.Type == @"AppResult");
            Assert.True(result.Content.Id == "291294");
            Assert.True(result.Content.Href.ToString() == @"v1pre3/appresults/291294");
           
            Assert.True(result.Content.Name == "BWA GATK - HiSeq 2500 NA12878 demo 2x150");
            Assert.True(result.Content.Status == "Complete");
            Assert.True(result.Content.DateCreated == new DateTime(2012,11, 15, 19,24,21, DateTimeKind.Utc) );

            Assert.True(result.Content.UserOwnedBy.Id == "1001");
            Assert.True(result.Content.UserOwnedBy.Href.ToString() == "v1pre3/users/1001");
            Assert.True(result.Content.UserOwnedBy.Name == "Illumina Inc");

        }

        [Fact]
        public void CanDeserializeASampleCompactReference()
        {
            var SampleReference = @"{
                ""Rel"": ""Using"",
                ""Type"": ""Sample"",
                ""Href"": ""v1pre3/samples/262264"",
                ""HrefContent"": ""v1pre3/samples/262264"",
                ""Content"": {
                    ""Id"": ""262264"",
                    ""Href"": ""v1pre3/samples/262264"",
                    ""UserOwnedBy"": {
                        ""Id"": ""1001"",
                        ""Href"": ""v1pre3/users/1001"",
                        ""Name"": ""Illumina Inc""
                    },
                    ""Name"": ""HiSeq 2500 NA12878 demo 2x150"",
                    ""SampleId"": ""HiSeq 2500 NA12878 demo 2x150"",
                    ""Status"": ""Complete"",
                    ""StatusSummary"": """",
                    ""DateCreated"": ""2012-11-15T19:22:54.0000000""
                }
            }";


            var result = ReferenceDeserializer.JsonToReference(SampleReference) as ContentReference<Sample>;
            Assert.NotNull(result);
            Assert.True(result.Href.ToString() == @"v1pre3/samples/262264");
            Assert.True(result.HrefContent.ToString() == @"v1pre3/samples/262264");
            Assert.True(result.Rel == @"Using");
            Assert.True(result.Type == @"Sample");
            Assert.True(result.Content.Id == "262264");
            Assert.True(result.Content.Href.ToString() == @"v1pre3/samples/262264");

            Assert.True(result.Content.Name == "HiSeq 2500 NA12878 demo 2x150");
            Assert.True(result.Content.SampleId == "HiSeq 2500 NA12878 demo 2x150");
            Assert.True(result.Content.Status == "Complete");
            Assert.True(result.Content.DateCreated == new DateTime(2012, 11, 15, 19, 22, 54, DateTimeKind.Utc));

            Assert.True(result.Content.UserOwnedBy.Id == "1001");
            Assert.True(result.Content.UserOwnedBy.Href.ToString() == "v1pre3/users/1001");
            Assert.True(result.Content.UserOwnedBy.Name == "Illumina Inc");

        }
    }
}
