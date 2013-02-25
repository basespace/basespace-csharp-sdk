using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Tests.Helpers;
using Illumina.BaseSpace.SDK.Types;
using Xunit;
using File = System.IO.File;
using System.IO;
using System;

namespace Illumina.BaseSpace.SDK.Tests.Integration
{
    public class FileDownloadTests : BaseIntegrationTest
    {
        [Fact]
        public void CanDownloadFile()
        {
            var project = TestHelpers.CreateRandomTestProject(Client);
            var appResult = TestHelpers.CreateRandomTestAppResult(Client, project);
            var file = File.Create(string.Format("UnitTestFile_{0}", appResult.Id));
            var data = new byte[10];
            new Random().NextBytes(data);
            file.Write(data, 0, 10);
            file.Close();
            var response = Client.UploadFileToAppResult(new UploadFileToAppResultRequest(appResult.Id, file.Name), null);
            Assert.NotNull(response);
            Assert.True(response.Response.UploadStatus == FileUploadStatus.complete);

            var fs = new FileStream("DownloadedFile-" + StringHelpers.RandomAlphanumericString(5), FileMode.OpenOrCreate);
            Client.DownloadFileById(response.Id, fs);
            Assert.Equal(fs.Length, data.Length);
        }
    }
}
