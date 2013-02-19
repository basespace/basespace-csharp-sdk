using System.ComponentModel;

namespace Illumina.BaseSpace.SDK.Types
{
	public enum InitialRunState
	{
		[Description("Incomplete")]
		New,
		
		[Description("Ready to Sequence")]
		Ready,
		
		[Description("Running")]
		Running
	}
}
