using Illumina.BaseSpace.SDK.ServiceModels;
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
            Assert.True(!string.IsNullOrEmpty(genomeList.Items[0].SpeciesName));
            Assert.True(!string.IsNullOrEmpty(genomeList.Items[0].Id));
            Assert.True(!string.IsNullOrEmpty(genomeList.Items[0].Href.ToString()));
        }
    }
}
