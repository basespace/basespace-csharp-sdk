using System.Collections.ObjectModel;

namespace Illumina.BaseSpace.SDK
{
	public interface IRequestOptions
	{
		uint RetryAttempts { get; }
		Collection<int> RetryableCodes { get; }
	}
}

