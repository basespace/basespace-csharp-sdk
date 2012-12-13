namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class PostProjectRequest
    {
        public PostProjectRequest(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
