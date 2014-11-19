namespace Illumina.BaseSpace.SDK
{
    public interface IRetryOptions
    {
        uint RetryAttempts { get; }
        double RetryPowerBase { get; }
    }

    public interface IRequestOptions: IRetryOptions
	{
	}
}

