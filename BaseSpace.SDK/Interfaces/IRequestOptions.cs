namespace Illumina.BaseSpace.SDK
{
	public interface IRequestOptions
	{
		uint RetryAttempts { get; }

		IAuthentication Authentication { get; }

		string BaseUrl { get; }
	}
}
