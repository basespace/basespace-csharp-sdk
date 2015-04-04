using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Illumina.BaseSpace.SDK;
using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Types;
using ServiceStack.Text;

namespace ConsoleApp
{
    class Program
    {
        public class RemoteFile
        {
            public string Url { get; set; }
            public string Path { get; set; }
            public string FileId { get; set; }
            public string HrefContent { get; set; }
        }
        public class BsfsMount
        {
            public BsfsMount()
            {
                MountFileList = new List<RemoteFile>();
            }
            public List<RemoteFile> MountFileList { get; set; }
        }

        static void Main(string[] args)
        {
          //  try
            {
                string token = "a48fb726ab1b49319b873c7d1e18104d";
                var client = new BaseSpaceClient(new BaseSpaceClientSettings()
                {
                    Authentication = new OAuth2Authentication(token),
                    BaseSpaceApiUrl = "https://api.cloud-test.illumina.com",
                    BaseSpaceWebsiteUrl = "https://cloud-test.illumina.com",
                    Version = "v1pre3",
                    RetryAttempts = 10,
                    FileDownloadMultipartSizeThreshold = Convert.ToUInt32(1024 * 1024)
                });
                //var output = client.ListGenomes(new ListGenomeRequest()).Response;
                //ListSampleFiles(new ListSampleFilesRequest("1055054")).Response;
                //var output = client.ListSampleFiles(new ListSampleFilesRequest("1055054")).Response;
                //var output = client.Search(new SearchRequest("*", "appresult_files"/*, "sample_files"*/)).Response;
                /*foreach (var searchResult in output.Items)
                {
                    Console.WriteLine("{0}: {1}", searchResult.Href, searchResult.SpeciesName);
                }*/
                GetGenomeFiles(client);
            }
          //  catch (Exception ex)
            {
                int i = 0;
            }
        }
       
        public static List<FileCompact> GetAllFilesUsingPaging(BaseSpaceClient client, ListFileSetFilesRequest listFileSetFilesRequest)
        {
            List<FileCompact> retVal = new List<FileCompact>();
            listFileSetFilesRequest.Limit = 1000;            
            GenericResourceList<FileCompact, FilesSortByParameters> nextPage;
            do
            {
                listFileSetFilesRequest.Offset = retVal.Count;
                nextPage = client.ListFiles(listFileSetFilesRequest).Response;
                retVal.AddRange(nextPage.Items);
            } while (nextPage.TotalCount > retVal.Count);
            return retVal;
        }

        private static void GetGenomeFiles(BaseSpaceClient client)
        {
            BsfsMount bsfsMount = new BsfsMount();
            var genomes = client.ListGenomes(new ListGenomeRequest()).Response;
            int totalFiles = 0;
            foreach (var genome in genomes.Items)
            {
                if (genome.HrefFileSets == null)
                {
                    continue;
                }
                var genomeFileSets = client.ListFileSets(new ListGenomeFileSetsRequest(genome)).Response;
                foreach (FileSet fileSet in genomeFileSets.Items)
                {

                    var fileSetFiles = GetAllFilesUsingPaging(client, new ListFileSetFilesRequest(fileSet));
                    foreach (var file in fileSetFiles)
                    {
                        Console.WriteLine("{0}:{1}",file.Path,file.Href);
                        FileContentRedirectMeta fileMeta = client.GetFileContentUrl(new FileContentRedirectMetaRequest(file.Id)).Response;
                        bsfsMount.MountFileList.Add(new RemoteFile() { Path = file.Path, Url = file.Href.ToString(), FileId = file.Id, HrefContent = fileMeta.HrefContent.ToString() });                        
                        totalFiles++;                        
                    }
                }
                var annotations = client.ListGenomeAnnotations(new ListGenomeAnnotationsRequest(genome)).Response;
                foreach (var annotation in annotations.Items)
                {
                    if (annotation.HrefFileSets == null)
                    {
                        continue;
                    }
                    var annotationFileSets = client.ListFileSets(new ListGenomeAnnotationFileSetsRequest(annotation)).Response;
                    //var fileList = GetAllFilesUsingPaging(client, new ListGenomeAnnotationFileSetsRequest(annotation));
                    //GenericResourceList<GenomeAnnotationFileSet, FileSetSortFields> fileList = client.ListFileSets(new ListGenomeAnnotationFileSetsRequest(annotation)).Response;
                    foreach (FileSet fileSet in annotationFileSets.Items)
                    {
                        var fileSetFiles = GetAllFilesUsingPaging(client, new ListFileSetFilesRequest(fileSet));
                        foreach (var file in fileSetFiles)
                        {
                            Console.WriteLine("{0}:{1}", file.Path, file.Href);
                            FileContentRedirectMeta fileMeta = client.GetFileContentUrl(new FileContentRedirectMetaRequest(file.Id)).Response;
                            bsfsMount.MountFileList.Add(new RemoteFile() { Path = file.Path, Url = file.Href.ToString(), FileId = file.Id, HrefContent = fileMeta.HrefContent.ToString() });
                            totalFiles++;
                        }
                    }
                }
            }
            using (var stream = System.IO.File.OpenWrite("out.json"))
            {
                JsonSerializer.SerializeToStream(bsfsMount, stream);
            }
            Console.WriteLine("\r\nTotal:" + totalFiles);
            Console.ReadLine();

        }
       
    }
}
