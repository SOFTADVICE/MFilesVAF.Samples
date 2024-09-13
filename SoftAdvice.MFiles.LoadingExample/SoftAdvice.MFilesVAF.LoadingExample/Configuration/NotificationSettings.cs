using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using MFiles.VAF.Configuration;

using MFilesAPI;

namespace SoftAdvice.MFilesVAF.LoadingExample.Configuration
{
	[DataContract]
	public class NotificationSettings
	{
		[DataMember]
		[JsonConfEditor(
			Label = "Licensing Errors",
			HelpText = "In case of error in the license (expired, number of licensed users)"
			)]
		[MFUserGroup]
		[Security(ChangeBy = SecurityAttribute.UserLevel.VaultAdmin)]
		public List<MFIdentifier> LicensingErrors { get; set; } = new List<MFIdentifier>();

		internal UserOrUserGroupIDs LicensingErrorRecipients => Parse();

		private UserOrUserGroupIDs Parse()
		{
			var ids = new UserOrUserGroupIDs();

			foreach (MFIdentifier value in LicensingErrors)
			{
				var id = new UserOrUserGroupID()
				{
					UserOrGroupType = MFUserOrUserGroupType.MFUserOrUserGroupTypeUserGroup,
					UserOrGroupID = value.ID
				};
				ids.Add(-1, id);
			}

			return ids;
		}
	}
}
