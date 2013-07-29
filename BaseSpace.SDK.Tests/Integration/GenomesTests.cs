using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Types;
using System.Linq;
using Xunit;

namespace Illumina.BaseSpace.SDK.Tests.Integration
{
    public class GenomesTests : BaseIntegrationTest
    {
        [Fact]
        public void CanGetGenome()
        {
            const string genomeId = "4";
            var response = Client.GetGenome(new GetGenomeRequest(genomeId));
            Assert.NotNull(response);
            var genome = response.Response;
            Assert.NotNull(genome);
            Assert.True(genome.DisplayName.Contains("Homo Sapiens - UCSC (hg19)"));
            Assert.True(genome.Source.Contains("UCSC"));
            Assert.True(genome.Build.Contains("hg19"));
            Assert.True(genome.Id.Contains(genomeId));
            Assert.True(genome.SpeciesName.Contains("Homo sapiens"));
            Assert.True(genome.Href.ToString().Contains(string.Format("genomes/{0}", genomeId)));
        }

        [Fact]
        public void CanListGenomes()
        {
            var response = Client.ListGenomes(new ListGenomeRequest(), null);
            Assert.NotNull(response);
            var genomeList = response.Response;
            Assert.NotNull(genomeList);
            Assert.NotNull(genomeList.Items);
            const int minimumNumberOfExpectedGenomes = 10;
            Assert.True(genomeList.Items.Length >= minimumNumberOfExpectedGenomes);
            Assert.True(!string.IsNullOrEmpty(genomeList.Items[0].SpeciesName));
            Assert.True(!string.IsNullOrEmpty(genomeList.Items[0].Id));
            Assert.True(!string.IsNullOrEmpty(genomeList.Items[0].Href.ToString()));
        }

        [Fact]
        public void CanSortGenomes()
        {
            const int someRidiculouslyHighNumberOfGenomes = 1000;
            var genomesListRequest = new ListGenomeRequest() { Limit = someRidiculouslyHighNumberOfGenomes, Offset = 0, SortBy = GenomeSortByParameters.SpeciesName, SortDir = SortDirection.Asc };
            var genomesListResponse = Client.ListGenomes(genomesListRequest, null);
            Assert.NotNull(genomesListResponse);
            var genomeList = genomesListResponse.Response;

            string[] genomeSpeciesList =
                {
                    "Arabidopsis thaliana",
                    "Bacillus Cereus",
                    "Bos Taurus",
                    "Escherichia coli",
                    "Homo sapiens",
                    "Mus musculus",
                    "Phix",
                    "Rattus norvegicus",
                    "Rhodobacter sphaeroides",
                    "Saccharomyces cerevisiae",
                    "Staphylococcus aureus"
                };

            for (int i=0; i<genomeSpeciesList.Length; i++)
                Assert.True(genomeList.Items.Any(x => x.SpeciesName == genomeSpeciesList[i]));
        }

        // These tests assume that you have imported files for at least one Genome
        [Fact]
        public void CanListGenomeFileSets()
        {
            var genomesListRequest = new ListGenomeRequest() { Limit = 1000 };
            var genomesListResponse = Client.ListGenomes(genomesListRequest, null);
            var oneWithFileSets = genomesListResponse.Response.Items.Where(x => x.HrefFileSets != null).FirstOrDefault();

            Assert.NotNull(oneWithFileSets);

            var fileSets = Client.ListFileSets(new ListGenomeFileSetsRequest(oneWithFileSets), null);
            Assert.NotNull(fileSets);
            Assert.NotEmpty(fileSets.Response.Items);
            var item = fileSets.Response.Items[0];
            Assert.NotNull(item.Href);
            Assert.NotNull(item.OriginalPath);
            Assert.NotNull(item.HrefFiles);
            Assert.NotNull(item.Type);
            Assert.NotNull(item.Category);

            // make sure we can list files
            var files = Client.ListFiles(new ListFileSetFilesRequest(item));
            Assert.NotNull(files);
            Assert.NotNull(files.Response);
            Assert.NotEmpty(files.Response.Items);
            Assert.NotEmpty(files.Response.Items[0].Name);
        }

        [Fact]
        public void CanListGenomeAnnotationFileSets()
        {
            var genomesListRequest = new ListGenomeRequest() { Limit = 1000 };
            var genomesListResponse = Client.ListGenomes(genomesListRequest, null);
            var oneWithFileSets = genomesListResponse.Response.Items.Where(x => x.HrefFileSets != null).FirstOrDefault();
            // we know these have annotqations with file sets too

            Assert.NotNull(oneWithFileSets);

            // this verifies I can list annotations too
            var annotations = Client.ListGenomeAnnotations(new ListGenomeAnnotationsRequest(oneWithFileSets));
            Assert.NotNull(annotations);
            Assert.NotEmpty(annotations.Response.Items);

            var annotationWithFileSets = annotations.Response.Items.Where(x => x.HrefFileSets != null).FirstOrDefault();
            Assert.NotNull(annotationWithFileSets);

            var fileSets = Client.ListFileSets(new ListGenomeAnnotationFileSetsRequest(annotationWithFileSets), null);
            Assert.NotNull(fileSets);
            Assert.NotEmpty(fileSets.Response.Items);
            var item = fileSets.Response.Items[0];
            Assert.NotNull(item.Href);
            Assert.NotNull(item.OriginalPath);
            Assert.NotNull(item.HrefFiles);
            Assert.NotNull(item.Source);
            Assert.NotNull(item.Version);

            // make sure we can list files
            var files = Client.ListFiles(new ListFileSetFilesRequest(item));
            Assert.NotNull(files);
            Assert.NotNull(files.Response);
            Assert.NotEmpty(files.Response.Items);
            Assert.NotEmpty(files.Response.Items[0].Name);
        }
    }
}
