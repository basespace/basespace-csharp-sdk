namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class CreateProjectRequest
    {
        public CreateProjectRequest(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
