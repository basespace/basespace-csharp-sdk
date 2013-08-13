using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Tests.Helpers;
using Illumina.BaseSpace.SDK.Types;
using Xunit;

namespace Illumina.BaseSpace.SDK.Tests.Integration
{
    public class UploadLogsToAppSessionTests : BaseIntegrationTest
    {
        [Fact]
        public void CreateLogs()
        {
            var appSession = TestHelpers.CreateTestAppSession(Client);
            Assert.NotNull(appSession);

            var logsResponse = Client.CreateAppSessionLogs(new CreateAppSessionLogsRequest(appSession));
            Assert.NotNull(logsResponse);

            var file = System.IO.File.Create(string.Format("UnitTestFile_{0}.log", logsResponse.Response.Id));
            var data = Encoding.ASCII.GetBytes("Howdy!");
            file.Write(data, 0, data.Length);
            file.Close();
            var uploadResponse = Client.UploadFileToFileSet(new UploadFileToFileSetRequest(logsResponse.Response, file.Name));

            Assert.NotNull(uploadResponse);
            Assert.True(uploadResponse.Response.UploadStatus == FileUploadStatus.complete);
        }

    }
}
