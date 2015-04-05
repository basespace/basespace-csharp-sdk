using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
            public string DirectUrl { get; set; }
            public string RenewUrl { get; set; }
            public DateTime Expires { get; set; }
        }
        public class BsfsMount
        {
            public BsfsMount()
            {
                MountFileList = new List<RemoteFile>();
            }
            public List<RemoteFile> MountFileList { get; set; }
        }
        static public DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }
        private static int totalFiles;
        static void Main(string[] args)
        {
            totalFiles = 0;
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
                var  request  = (HttpWebRequest) WebRequest.Create("https://api.cloud-test.illumina.com/v1pre3/files/gen1_33309276/content?access_token=a48fb726ab1b49319b873c7d1e18104d");
                request.AllowAutoRedirect = false;
                var response = (HttpWebResponse)request.GetResponse();
                var redirectedURl = response.Headers["Location"];
                DateTime expires = DateTime.MaxValue;
                
                if (!string.IsNullOrWhiteSpace(redirectedURl))
                {
                    var expiresEpoch = HttpUtility.ParseQueryString(redirectedURl).Get("expires");
                    long epoch = 0;
                    if (!string.IsNullOrWhiteSpace(redirectedURl)&&long.TryParse(expiresEpoch, out epoch))
                    {
                        expires = FromUnixTime(epoch);
                    }
                }
                var remainingTime = (expires - DateTime.UtcNow).TotalDays;
                Console.WriteLine("Expires:{0}", expires);
                
                //https://api.cloud-test.illumina.com/v1pre3/files/gen1_33309276/content?access_token=a48fb726ab1b49319b873c7d1e18104d
                
                
                GetGenomeFiles(client);
            }
          //  catch (Exception ex)
            {
                int i = 0;
            }
        }
       
        public static List<FileCompact> GetAllFilesUsingPaging(BaseSpaceClient client, ListFileSetFilesRequest listFileSetFilesRequest)
        {
            var retVal = new List<FileCompact>();
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
            var bsfsMount = new BsfsMount();
            var genomes = client.ListGenomes(new ListGenomeRequest()).Response;            
            foreach (var genome in genomes.Items)
            {
                if (genome.HrefFileSets == null)
                {
                    continue;
                }
                var genomeFileSets = client.ListFileSets(new ListGenomeFileSetsRequest(genome)).Response;
                GetAllFilesFromFileSets(client, genomeFileSets.Items, bsfsMount);
                var annotations = client.ListGenomeAnnotations(new ListGenomeAnnotationsRequest(genome)).Response;
                foreach (var annotation in annotations.Items)
                {
                    if (annotation.HrefFileSets == null)
                    {
                        continue;
                    }
                    var annotationFileSets = client.ListFileSets(new ListGenomeAnnotationFileSetsRequest(annotation)).Response;
                    GetAllFilesFromFileSets(client, annotationFileSets.Items, bsfsMount);                   
                }
            }
            using (var stream = System.IO.File.OpenWrite("out.json"))
            {
                JsonSerializer.SerializeToStream(bsfsMount, stream);
            }
            Console.WriteLine("\r\nTotal:" + totalFiles);
            Console.ReadLine();

        }

        private static void GetAllFilesFromFileSets(BaseSpaceClient client, IEnumerable<FileSet> fileSets, BsfsMount bsfsMount)
        {
            foreach (FileSet fileSet in fileSets)
            {
                var files = GetAllFilesUsingPaging(client, new ListFileSetFilesRequest(fileSet));
                foreach (var file in files)
                {
                    Console.WriteLine("{0}:{1}", file.Path, file.Href);
                    var fileMeta = client.GetFileContentUrl(new FileContentRedirectMetaRequest(file.Id)).Response;                    
                    bsfsMount.MountFileList.Add(new RemoteFile()
                    {
                        Path = file.Path,
                        Url = file.Href.ToString(),
                        FileId = file.Id,
                        DirectUrl = fileMeta.HrefContent,
                        Expires = fileMeta.Expires,
                        RenewUrl = String.Format("{0}/v1pre3/files/{1}/content?access_token={2}", "https://api.cloud-test.illumina.com", file.Id, "a48fb726ab1b49319b873c7d1e18104d")
                    });
                    totalFiles++;
                   break;
                }
            }
        }
    }
}
