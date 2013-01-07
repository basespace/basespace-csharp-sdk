using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Types;
using Xunit;

namespace Illumina.BaseSpace.SDK.Tests.Integration
{
    public class ProjectsTests : BaseIntegrationTest
    {
        public ProjectsTests()
        {
        }

        [Fact]
        public void CanGetUserProjectsFirstPage()
        {
           ListProjectsResponse response = Client.ListProjects(new ListProjectsRequest());
            
           Assert.NotNull(response);
           Assert.True(response.Response.TotalCount > 0); //make sure account has at least 1 for access token
            ProjectCompact projectResult = response.Response.Items[0];

           Assert.NotNull(projectResult);
            Assert.NotEmpty(projectResult.Id);
            Assert.NotEmpty(projectResult.Name);
            Assert.NotSame(projectResult.Id, projectResult.Name);
            Assert.True(projectResult.DateCreated > new DateTime(2009,1,1));
        }

        [Fact]
        public void CanGetUserProjectsFirstPageAsync()
        {
            Task<ListProjectsResponse> asyncResponse = Client.ListProjectsAsync(new ListProjectsRequest());
           
            asyncResponse.Wait(TimeSpan.FromMinutes(1));
            var response = asyncResponse.Result;
            Assert.NotNull(response);
            Assert.True(response.Response.TotalCount > 0);
            //make sure account has at least 1 for access token
            ProjectCompact projectResult = response.Response.Items[0];

            Assert.NotNull(projectResult);
            Assert.NotEmpty(projectResult.Id);
            Assert.NotEmpty(projectResult.Name);
            Assert.NotSame(projectResult.Id, projectResult.Name);
            Assert.True(projectResult.DateCreated > new DateTime(2009, 1, 1));

        }

        [Fact]
        public void CanPageThroughProjects()
        {

            ListProjectsResponse baseResponse = Client.ListProjects(new ListProjectsRequest(){Limit = 1, Offset = 0});   //get 1 and total count
            //grab next page, assume there is another
            ListProjectsResponse pagedResponse = Client.ListProjects(new ListProjectsRequest() { Limit = 1, Offset = 1 });
            Assert.True(pagedResponse.Response.DisplayedCount == 1);
        }

        [Fact]
        public void CanSortThroughProjects()
        {

            ListProjectsResponse baseResponse = Client.ListProjects(new ListProjectsRequest() { Limit = 1, Offset = 0, SortBy = ProjectsSortByParameters.Name, SortDir = SortDirection.Desc });
            
            //grab next page, assume there is another
            ListProjectsResponse sortedAsc = Client.ListProjects(new ListProjectsRequest() { Limit = 1, Offset = 0, SortBy = ProjectsSortByParameters.Name, SortDir = SortDirection.Asc });
            Assert.True(baseResponse.Response.Items[0].Name[0] > sortedAsc.Response.Items[0].Name[0]);
        }

        [Fact]
        public void CanLookupByName()
        {

            ListProjectsResponse baseResponse = Client.ListProjects(new ListProjectsRequest() { Limit = 1, Offset = 0 });

            //grab next page, assume there is another
            ListProjectsResponse byName = Client.ListProjects(new ListProjectsRequest() { Limit = 1, Offset = 0, Name = baseResponse.Response.Items[0].Name });
            Assert.True(baseResponse.Response.Items[0].Name == byName.Response.Items[0].Name);
        }

        [Fact]
        public void CanCreateProject()
        {
            var projectName = string.Format("SDKUnitTest-{0}", StringHelpers.RandomAlphanumericString(5));
            var response = Client.CreateProject(new PostProjectRequest(projectName));
            Assert.NotNull(response);
            var project = response.Response;
            Assert.NotNull(project);
            Assert.True(project.Name.Contains(projectName));
        }
    }
}
