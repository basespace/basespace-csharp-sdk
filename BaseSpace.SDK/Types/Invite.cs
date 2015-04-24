using System;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{
	[DataContract(Name = "Invite")]
	public class InviteCompact : AbstractResource
	{
		[DataMember(IsRequired = true)]
		public override string Id { get; set; }

		[DataMember]
		public override Uri Href { get; set; }

		[DataMember]
		public UserCompact UserInvited { get; set; }

		[DataMember]
		public string EmailInvited { get; set; }
		
		
		public string Type
		{
			get { return PropertyTypes.INVITE; }
		}
		public override string ToString()
		{
			return string.Format("Href: {0}; Id: {1}", Href, Id);
		}
	}

	[DataContract]
	public class Invite : InviteCompact
	{
		[DataMember]
		public Uri HrefInvite { get; set; }

		[DataMember]
		public string Status { get; set; }

		[DataMember]
		public DateTime DateCreated { get; set; }
		[DataMember]
		public DateTime DateModified { get; set; }

		[DataMember]
		public string Permission { get; set; }

		[DataMember]
		public bool HasCollaborators { get; set; }

		[DataMember]
		public IContentReference<IAbstractResource>[] References { get; set; }

		[DataMember]
		public long TotalSize { get; set; }
	}

}
