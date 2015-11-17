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
    public class ProjectsTests : BaseIntegrationTest
    {
        [Fact]
        public void CanGetUserProjectsFirstPage()
        {
            TestHelpers.CanGetUserProjectsFirstPage(Client);          
        }

        [Fact]
        public void CanGetUserProjectsFirstPageWithPermissions()
        {
            TestHelpers.CanGetUserProjectsFirstPage(Client, new[] { ProjectIncludes.PERMISSIONS });                        
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
            var project1a = TestHelpers.CreateRandomTestProject(Client);
            var project1b = TestHelpers.CreateRandomTestProject(Client);
            var project1c = TestHelpers.CreateRandomTestProject(Client);

            ListProjectsResponse sortedAsc = Client.ListProjects(new ListProjectsRequest() { Limit = 30, Offset = 0, SortBy = ProjectsSortByParameters.DateCreated, SortDir = SortDirection.Desc });
            Assert.True(sortedAsc.Response.Items.Length >= 3);
            var project2a = sortedAsc.Response.Items[0];
            var project2b = sortedAsc.Response.Items[1];
            var project2c = sortedAsc.Response.Items[2];

            Assert.True((project2a.Name == project1a.Name) || (project2a.Name == project1b.Name) || (project2a.Name == project1c.Name));
            Assert.True((project2b.Name == project1a.Name) || (project2b.Name == project1b.Name) || (project2b.Name == project1c.Name));
            Assert.True((project2c.Name == project1a.Name) || (project2c.Name == project1b.Name) || (project2c.Name == project1c.Name));
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
            var project = TestHelpers.CreateRandomTestProject(Client);

            Assert.False(string.IsNullOrEmpty(project.Id));
            Assert.True(int.Parse(project.Id) > -1);
            Assert.False(string.IsNullOrEmpty(project.Name));

            string TestProjectRegexString = @"[A-Za-z0-9]*\/projects\/{0}\/";
            string TestProjectRegexStringForAppResults = TestProjectRegexString + "appresults";
            string regexString1 = string.Format(TestProjectRegexStringForAppResults, project.Id);
            Match match1 = Regex.Match(project.HrefAppResults.OriginalString, regexString1, RegexOptions.IgnoreCase);
            Assert.True(match1.Success);

            string TestProjectRegexStringForUI = @"https*:\/\/[A-Za-z0-9-]*\.illumina\.com\/project\/{0}\/{1}";
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
            TestHelpers.CanGetProject(Client);            
        }

        [Fact]
        public void CanGetProjectWithPermissions()
        {
            TestHelpers.CanGetProject(Client,  new []{ProjectIncludes.PERMISSIONS});            
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
                Assert.True(baseSpaceException.InnerException.Message == "Internal Server Error" ||
                    baseSpaceException.StatusCode == System.Net.HttpStatusCode.NotFound || 
                            baseSpaceException.StatusCode == System.Net.HttpStatusCode.BadRequest);
            }
            catch (Exception)
            {
                Assert.False(true);
            }
        }

        // "update" and "delete" are not supported by the sdk at this time
        //public void CanDeleteProject()
        //public void CanUpdateProject()
    }
}
