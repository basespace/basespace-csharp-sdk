using System;
using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Tests.Integration;
using Illumina.BaseSpace.SDK.Types;
using Xunit;
using System.Collections.Generic;
using System.Linq;


namespace Illumina.BaseSpace.SDK.Tests.Helpers
{
    public class TestHelpers : BaseIntegrationTest
    {
        #region Project
        private const string TestProjectNameFormatString = "SDK_Test_Project-{0}";
        private static string CreateRandomTestProjectName() { return string.Format(TestProjectNameFormatString, StringHelpers.RandomAlphanumericString(5)); }
        public static Project CreateRandomTestProject(IBaseSpaceClient client, string projectName = null)
        {
            projectName = projectName ?? CreateRandomTestProjectName();
            var response = client.CreateProject(new CreateProjectRequest(projectName));
            Assert.NotNull(response);

            Assert.NotNull(response.Response);
            var project = response.Response;
            Assert.NotNull(project);
            Assert.True(project.Name.Contains(projectName));
            return project;
        }
        #endregion Project

        #region AppResult
        private const string TestAppResultNameFormatString = "SDK_Test_AppResult-{0}";
        private static string CreateRandomTestAppResultName() { return string.Format(TestAppResultNameFormatString, StringHelpers.RandomAlphanumericString(5)); }
        public static AppResult CreateRandomTestAppResult(IBaseSpaceClient client, Project project)
        {
            var appResultName = CreateRandomTestAppResultName();
            var response = client.CreateAppResult(new CreateAppResultRequest(project.Id, appResultName));
            Assert.NotNull(response);

            Assert.NotNull(response.Response);
            var appResult = response.Response;
            Assert.NotNull(appResult);
            Assert.True(appResult.Name.Contains(appResultName));
            return appResult;
        }
        #endregion AppResult

        #region AppSession
        internal static AppSession CreateTestAppSession(IBaseSpaceClient client)
        {
            var project = TestHelpers.CreateRandomTestProject(client);
            var appResult = TestHelpers.CreateRandomTestAppResult(client, project);
            var appSessionCompact = appResult.AppSession;
            Assert.NotNull(appSessionCompact);
            var appSessionResponse = client.GetAppSession(new GetAppSessionRequest(appSessionCompact.Id));
            Assert.NotNull(appSessionResponse);
            var appSession = appSessionResponse.Response;
            Assert.NotNull(appSession);

            return appSession;
        }
        #endregion AppSession

        #region Prep
        internal static BiologicalSample CreateBiologicalSample(IBaseSpaceClient client, string sampleId=null, string sampleName=null, string nucleicAcid=null)
        {
            var sample = string.Format("sample_SDK_Test_{0}", StringHelpers.RandomAlphanumericString(5));
            var biologicalSample = client.CreateBiologicalSample(new CreateBiologicalSampleRequest(sampleId ?? sample, sampleName ?? sample, nucleicAcid ?? "DNA"));
            Assert.NotNull(biologicalSample.Response);
            return biologicalSample.Response;
        }

        internal static LibraryContainer CreateLibraryContainer(IBaseSpaceClient client, string libraryPrepId=null, string userContainerId=null, string containerType=null, bool indexByWell=true)
        {
            libraryPrepId = libraryPrepId ?? "1";
            userContainerId = userContainerId ?? string.Format("container-sdk-{0}", StringHelpers.RandomAlphanumericString(5));
            containerType = containerType ?? "Plate96";
            indexByWell = indexByWell ? indexByWell : false;

            var libraryContainer = client.CreateLibraryContainer(new CreateLibraryContainerRequest(libraryPrepId, userContainerId, containerType, indexByWell));
            var response = libraryContainer.Response ;
            Assert.NotNull(response);
            Assert.Equal(response.UserContainerId, userContainerId);
            return response;
        }

        internal static LibraryPrepKit GetLibraryPrepKit(IBaseSpaceClient client, string libraryPrepKitId)
        {
            var libraryPrepKit = client.GetLibraryPrepKit(new GetLibraryPrepKitIdRequest(libraryPrepKitId));
            Assert.NotNull(libraryPrepKit.Response);
            return libraryPrepKit.Response;
        }

        internal static LibraryContainer CreateContainerLibraries(IBaseSpaceClient client, int libraryCount)
        {
            var prepKitIndex = "1";
            var originalLibraryCount = libraryCount;
            var container = CreateLibraryContainer(client);
            var libraryRequests = new List<SampleLibraryRequest>();
            var containerPositions = CreateContainerPositions();
            int containerPositionIndex = 0;
            var project = CreateRandomTestProject(client);
            var libraryPrep = GetLibraryPrepKit(client, prepKitIndex);

            while (libraryCount > 0)
            {
                var libraryRequest = new SampleLibraryRequest()
                {
                    BiologicalSampleId = CreateBiologicalSample(client).Id,
                    Index1SequenceId = libraryPrep.Index1Sequences.ElementAt(containerPositionIndex).Id,
                    Position = containerPositions[containerPositionIndex],
                    ProjectId = project.Id
                };

                libraryRequests.Add(libraryRequest);
                libraryCount--;
                containerPositionIndex++;
            }
            
            client.CreateOrUpdateContainerLibraries(new CreateOrUpdateContainerLibrariesRequest(libraryRequests, container.Id));
            var sampleLibraries = GetSampleLibrariesInContainer(client, container.Id);
            Assert.Equal(sampleLibraries.Count(), originalLibraryCount);
            Assert.Equal(sampleLibraries.FirstOrDefault().BiologicalSample.Id, libraryRequests.FirstOrDefault().BiologicalSampleId);
            return container;
        }

        internal static List<string> CreateContainerPositions()
        {
            var wellPositions = new List<string>();
            string[] prefix = { "A", "B", "C", "D", "E", "F", "G", "H" };
            
            for(int i=0; i<8; i++)
            {
                for(int j=1; j<13; j++)
                    wellPositions.Add(string.Format("{0}{1}",prefix[i],(j<10)? string.Format("0{0}",j): Convert.ToString(j)));
            }
            return wellPositions;
        }

        internal static SampleLibrary[] GetSampleLibrariesInContainer(IBaseSpaceClient client, string containerId)
        {
            var sampleLibraries = client.GetContainerToLibraryMapping(new GetContainerToLibraryMappingRequest(containerId));
            var response = sampleLibraries.Response;
            Assert.NotNull(response);
            Assert.Equal(response.Items.FirstOrDefault().Container.Id, containerId);
            return response.Items;
        }

        internal static LibraryPool CreateLibraryPoolWithMapping(IBaseSpaceClient client, IEnumerable<string>libraryIds, string userPoolId = null)
        {
            userPoolId = string.Format("pool-sdk-{0}", StringHelpers.RandomAlphanumericString(5)) ?? userPoolId;
            var libraryPool = client.CreateLibraryPool(new CreateLibraryPoolRequest(userPoolId, libraryIds)).Response;
            Assert.NotNull(libraryPool);
            //client.UpdatePoolToLibraryMapping(new UpdatePoolToLibraryMappingRequest(libraryPool.Id, libraryIds));
            return libraryPool;
        }

        internal static PlannedRun CreatePlannedRun(IBaseSpaceClient client, IEnumerable<PlannedRunPoolMapping> poolMappings, int numCyclesRead1, string name=null, 
            string platformName=null, string analysisWorkFlowType=null)
        {
            var plannedRun = client.CreatePlannedRun(new CreatePlannedRunRequest(
                                poolMappings,
                                numCyclesRead1,
                                name ?? string.Format("Run-SDK-{0}", StringHelpers.RandomAlphanumericString(5)),
                                platformName ?? "NextSeq",
                                analysisWorkFlowType ?? "HiSeqFastQ",
                                "None"
                                )).Response;

            Assert.NotNull(plannedRun);
            return plannedRun;
        }

        internal static void MarkRunAsReadyToSequence(IBaseSpaceClient client, string plannedRunId)
        {
            client.PlannedRunReadyRequest(new PlannedRunReadyRequest(plannedRunId, true));
            Assert.Equal(ListReadyRuns(client).Where(r => r.Id == plannedRunId).Count(), 1);
        }

        internal static RunCompact[] ListReadyRuns(IBaseSpaceClient client)
        {
            var plannedRuns = client.ListRuns(new ListRunsRequest() {Statuses="Ready"});
            return plannedRuns.Response.Items;
        }
        #endregion
    }
}
