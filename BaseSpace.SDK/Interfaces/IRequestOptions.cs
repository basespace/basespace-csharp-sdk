namespace Illumina.BaseSpace.SDK
{
	public interface IRequestOptions
	{
		uint RetryAttempts { get; }

		string BaseUrl { get; }
	}
}

