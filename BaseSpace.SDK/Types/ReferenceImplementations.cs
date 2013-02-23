using System;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{
	public class ContentReference<T> : IContentReference<T>
		where T : AbstractResource
	{
		[DataMember]
		public string Rel { get; set; }

		[DataMember]
		public string Type { get; set; }

		[DataMember]
		public Uri Href { get; set; }

		[DataMember]
		public Uri HrefContent { get; set; }

		[DataMember]
		public T Content { get; set; }
	}
}
