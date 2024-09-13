using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using MFiles.VAF.Configuration;
using MFiles.VAF.Configuration.JsonAdaptor;
using MFiles.VAF.Configuration.Logging;
using MFiles.VAF.Extensions.Configuration;

namespace SoftAdvice.MFilesVAF.LoadingExample.Configuration
{
	[DataContract]
	[ConfigurationVersion("0.1")]
	public class ConfigurationRoot : VersionedConfigurationBase, IConfigurationWithLoggingConfiguration
	{
		[DataMember]
		[Security(ChangeBy = SecurityAttribute.UserLevel.VaultAdmin)]
		public NLogLoggingConfiguration Logging { get; set; }

		public ILoggingConfiguration GetLoggingConfiguration()
		{
			return Logging;
		}

		[DataMember]
		[Security(ChangeBy = SecurityAttribute.UserLevel.VaultAdmin)]
		public NotificationSettings Notifications { get; set; }
	}
}