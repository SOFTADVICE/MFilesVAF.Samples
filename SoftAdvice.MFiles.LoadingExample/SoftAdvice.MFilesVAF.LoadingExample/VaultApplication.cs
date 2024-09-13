using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using MFiles.VAF;
using MFiles.VAF.AppTasks;
using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFiles.VAF.Configuration.AdminConfigurations;
using MFiles.VAF.Extensions;

using MFilesAPI;

using SoftAdvice.MFilesVAF.LoadingExample.Configuration;

namespace SoftAdvice.MFilesVAF.LoadingExample
{
	/// <summary>
	/// The entry point for this Vault Application Framework application.
	/// </summary>
	/// <remarks>Examples and further information available on the developer portal: http://developer.m-files.com/. </remarks>
	public partial class VaultApplication
		: ConfigurableVaultApplicationBase<ConfigurationRoot>
	{
		public class ApplicationException : Exception
		{
			public static string FormatMessage(string msg) => $"{GetName()}\r\n{msg}";
			public ApplicationException(string message) : base(FormatMessage(message)) { }
			public ApplicationException(Exception innerException) : base(FormatMessage(innerException.Message), innerException) { }
			public ApplicationException(string message, Exception innerException) : base(FormatMessage(message), innerException) { }
		}

		public const string APPNAME = "SoftAdvice.MFilesVAF.LoadingExample";

		public static string GetName()
		{
			return $"{APPNAME} V" + ApplicationDefinition.Version;
		}

		public override string GetName(IConfigurationRequestContext context)
		{
			return GetName();
		}

		public override string GetID()
		{
			return APPNAME.Replace(" ", "_").Replace(".", "_");
		}

		/// <summary>
		/// Install the UIX application, as it will not be installed by default.
		/// </summary>
		/// <param name="vault">The vault to install the application into.</param>
		protected override void InitializeApplication(Vault vault)
		{
			try
			{
				string appPath = "UIX.mfappx";
				if (File.Exists(appPath))
				{
					vault.CustomApplicationManagementOperations.InstallCustomApplication(appPath);
				}
				else
				{
					Logger.Error($"File: {appPath} does not exist");
				}
			}
			catch (Exception ex)
			{
				if (!MFUtils.IsMFilesAlreadyExistsError(ex))
					Logger.Fatal(ex);
			}

			base.InitializeApplication(vault);
		}
	}
}