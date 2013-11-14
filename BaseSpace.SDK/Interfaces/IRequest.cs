namespace Illumina.BaseSpace.SDK
{
    public interface IRequest<TRequest>
    {
        TRequest Request { get; }
    }
}
