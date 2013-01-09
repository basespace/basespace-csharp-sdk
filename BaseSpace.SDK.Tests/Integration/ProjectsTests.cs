using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Types;
using Xunit;

namespace Illumina.BaseSpace.SDK.Tests.Integration
{
    public class ProjectsTests : BaseIntegrationTest
    {
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

        private const string TestProjectNameFormatString = "SDKUnitTest-{0}";
        private string CreateRandomTestProjectName() { return string.Format(TestProjectNameFormatString, StringHelpers.RandomAlphanumericString(5)); }
        private Project CreateRandomTestProject()
        {
            var projectName = CreateRandomTestProjectName();
            var response = Client.CreateProject(new PostProjectRequest(projectName));
            Assert.NotNull(response);
            Assert.NotNull(response.Response);
            var project = response.Response;
            Assert.NotNull(project);
            Assert.True(project.Name.Contains(projectName));
            return project;
        }

        [Fact]
        public void CanCreateProject()
        {
            var project = CreateRandomTestProject();

            Assert.False(string.IsNullOrEmpty(project.Id));
            Assert.True(int.Parse(project.Id) > -1);
            Assert.False(string.IsNullOrEmpty(project.Name));

            string TestProjectRegexString = @"[A-Za-z0-9]*\/projects\/{0}\/";
            string TestProjectRegexStringForAppResults = TestProjectRegexString + "appresults";
            string regexString1 = string.Format(TestProjectRegexStringForAppResults, project.Id);
            Match match1 = Regex.Match(project.HrefAppResults.OriginalString, regexString1, RegexOptions.IgnoreCase);
            Assert.True(match1.Success);

            string TestProjectRegexStringForUI = @"https:\/\/[A-Za-z0-9-]*\.illumina\.com\/project\/{0}\/{1}";
            string regexString2 = string.Format(TestProjectRegexStringForUI, project.Id, project.Name);
            Match match2 = Regex.Match(project.HrefBaseSpaceUI.OriginalString, regexString2, RegexOptions.IgnoreCase);
            Assert.True(match2.Success);

            string TestProjectRegexStringForSamples = TestProjectRegexString + "samples";
            string regexString3 = string.Format(TestProjectRegexStringForSamples, project.Id);
            Match match3 = Regex.Match(project.HrefSamples.OriginalString, regexString3, RegexOptions.IgnoreCase);
            Assert.True(match3.Success);
        }

        [Fact]
        public void CanGetProject()
        {
            var project = CreateRandomTestProject();

            ListProjectsResponse listProjectResponse = Client.ListProjects(new ListProjectsRequest() { Limit = 1, Offset = 0, Name = project.Name });
            Assert.True(listProjectResponse.Response.Items.Length == 1);
            var compactProject = listProjectResponse.Response.Items[0];
            Assert.True(project.Id == compactProject.Id);
            Assert.True(project.Name == compactProject.Name);

            var getProjectResponse = Client.GetProject(new GetProjectRequest(compactProject.Id));
            var retrievedProject = getProjectResponse.Response;
            Assert.True(project.Id == retrievedProject.Id);
            Assert.True(project.Name == retrievedProject.Name);
            Assert.True(project.HrefAppResults == retrievedProject.HrefAppResults);
            Assert.True(project.HrefBaseSpaceUI == retrievedProject.HrefBaseSpaceUI);
            Assert.True(project.HrefSamples == retrievedProject.HrefSamples);
        }

        [Fact]
        public void CannotGetProjects()
        {
            FailProject(string.Empty);
            FailProject("-1");
            FailProject("0");
            FailProject(long.MaxValue.ToString());
            FailProject("error");
            FailProject("&*%$");
        }

        private void FailProject(string id)
        {
            try
            {
                var getProjectResponse = Client.GetProject(new GetProjectRequest(id));
                //var retrievedProject = getProjectResponse.Response;
                //Assert.Null(retrievedProject);
                Assert.True(false, "BaseSpace returned a malformed Project.");
            }
            catch (BaseSpace.SDK.BaseSpaceException baseSpaceException)
            {
                Assert.True((baseSpaceException.StatusCode == System.Net.HttpStatusCode.NotFound) || 
                            (baseSpaceException.StatusCode == System.Net.HttpStatusCode.BadRequest));
            }
            catch (Exception ex)
            {
                Assert.True(ex.InnerException.Message == "Internal Server Error");
            }
        }

        // "update" and "delete" are not supported by the sdk at this time
        //public void CanDeleteProject()
        //public void CanUpdateProject()
    }
}
