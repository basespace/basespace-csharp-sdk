using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Types;
using NUnit.Framework;

namespace Illumina.BaseSpace.SDK.MonoTouch.Tests.Integration
{
	[TestFixture]
    public class GenomesTests : BaseIntegrationTest
    {
        [Test]
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

        [Test]
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

        [Test]
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

            // this set will only work if we continue to test against the contents of the test db and if that does not change.
            Assert.True(genomeList.Items.Length == genomeSpeciesList.Length);
            for (int i=0; i<genomeSpeciesList.Length; i++)
                Assert.True(genomeList.Items[i].SpeciesName == genomeSpeciesList[i]);
        }
    }
}
