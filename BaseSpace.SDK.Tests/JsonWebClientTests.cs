using System;
using System.Diagnostics;
using Illumina.BaseSpace.SDK.Deserialization;
using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Types;
using Moq;
using Xunit;

namespace Illumina.BaseSpace.SDK.Tests
{

    public class JsonWebClientTests
    {
        [Fact]
        public void RequestOptionsAreProperlyUsed()
        {
            var client = new BaseSpaceClient(new BaseSpaceClientSettings()
            {
                BaseSpaceApiUrl = "https://api.basespace.illumina.com",
                Authentication = new OAuth2Authentication("xxxxx"),
                TimeoutMin = .00001
            });

            var sw = Stopwatch.StartNew();
            try
            {
                var ass = client.GetAppSession(new GetAppSessionRequest("xxxx"), new RequestOptions(6,.1));
            }
            catch (Exception)
            {
                
            }
            sw.Stop();

            Assert.InRange(sw.ElapsedMilliseconds,0,10*1000);

        } 
        

        [Fact]
        public void CanRespawnClientAtEachRetry()
        {
            var setting = new BaseSpaceClientSettings()
            {
                BaseSpaceApiUrl = "https://api.basespace.illumina.com",
                Authentication = new OAuth2Authentication("xxxxx"),
                TimeoutMin = .0000000001
            };

            var client = new Mock<JsonWebClient>(setting, null);
            int retry = 0;
            client.Setup(c => c.RespawnClient()).Callback(() =>
            {
                // this should occur only once and because of the ridiculous timeout
                Assert.Equal(1, ++retry);

                // respawn the client with a normal timeout
                // should be proof that the newly created client
                // is used on second retry
                client.Object._clientFactoryMethod();
                client.Object.client.Timeout = TimeSpan.FromMinutes(1);
            });

            var ass = client.Object.Send(new GetAppSessionRequest("xxxxx"), new RequestOptions(6,.1));
        }


        
        [Fact]
        public void CanDeserializeARunCompactReference()
        {
            var result = ReferenceDeserializer.JsonToReference(TestData.AppResultReference) as ContentReference<AppResult>;
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
            var result = ReferenceDeserializer.JsonToReference(TestData.SampleReference) as ContentReference<Sample>;
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
