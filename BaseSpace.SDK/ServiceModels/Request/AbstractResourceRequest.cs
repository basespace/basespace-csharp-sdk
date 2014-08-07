namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public abstract class AbstractResourceRequest<TReturn> : AbstractRequest<TReturn>
        where TReturn : class
    {
        protected AbstractResourceRequest()
        {
        }

        protected AbstractResourceRequest(string id)
            : this()
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
