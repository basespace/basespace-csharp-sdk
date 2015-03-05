using System;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
	public enum SharePermissions
	{
		Read,
		Write,
		Own
	}

	[DataContract]
	public class CreateProjectShareRequest : AbstractResourceRequest<ProjectShareResponse>
	{
		private const string ReadPermission = "READ";
		private const string WritePermission = "WRITE";
		private const string OwnerPermission = "OWN";

		public CreateProjectShareRequest(string id, string email, string permission,
			string description = "") : base(id)
		{
			if (permission != ReadPermission || permission != WritePermission || permission!=OwnerPermission)
				throw new ArgumentException("Permission must be either READ, WRITE or OWN");
			Permission = permission;
			CommonConstruct(id, email, description);
		}

		public CreateProjectShareRequest(string id, string email, SharePermissions permission,
			string description = "") : base(id)
		{
			CommonConstruct(id, email, description);
			switch (permission)
			{
				case SharePermissions.Read:
					Permission = ReadPermission;
					break;
				case SharePermissions.Write:
					Permission = WritePermission;
					break;
				case SharePermissions.Own:
					Permission = OwnerPermission;
					break;
			}
		}

		private void CommonConstruct(string id, string email, string description)
		{
			if (string.IsNullOrEmpty(id))
				throw new ArgumentException("You must provide an id");
			if (string.IsNullOrEmpty(email) || !email.Contains("@"))
				throw new ArgumentException("You must provide an id");
			if (description == null)
				description = string.Empty;
			Id = id;
			Email = email;
			Description = description;
			HttpMethod = HttpMethods.POST;
		}

		[DataMember]
		public string Email { get; set; }
		[DataMember]
		public string Permission { get; set; }
		[DataMember]
		public string Description { get; set; }

		protected override string GetUrl()
		{
			return string.Format("{0}/project/{1}/invites", Version, Id);
		}
	}
}