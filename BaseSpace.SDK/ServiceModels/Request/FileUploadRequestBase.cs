namespace Illumina.BaseSpace.SDK.ServiceModels
{
	public abstract class FileUploadRequestBase
	{
		public string Id { get; set; }

		public string FilePath { get; set; }

        public string Directory { get; set; }

        public bool? MultiPart { get; set; }
    }
}
