using System.Text.RegularExpressions;
using Illumina.BaseSpace.SDK.Tests.Helpers;
using Xunit;
using System;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.Tests.Integration
{
    public class LibraryContainersTests : BaseIntegrationTest
    {
        [Fact]
        public void CanListLibraryContainersByDateModified()
        {
            var libraryContainers = Client.ListLibraryContainers(new ServiceModels.ListLibraryContainersRequest()
                {
                    SortBy = LibraryContainerSortFields.DateModified,
                    SortDir = SortDirection.Desc
                });
            Assert.NotNull(libraryContainers);
            Assert.NotNull(libraryContainers.Response);
            Assert.NotNull(libraryContainers.Response.Items);
            foreach (var libraryContainer in libraryContainers.Response.Items)
            {
                Assert.True(libraryContainer.DateModified > DateTime.MinValue);
                Assert.NotNull(libraryContainer.Id);
            }
            
        }
    }
}
