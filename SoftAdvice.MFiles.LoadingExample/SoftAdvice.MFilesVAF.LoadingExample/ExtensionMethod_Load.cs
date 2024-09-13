using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using MFiles.VAF.Common;

using MFilesAPI;

namespace SoftAdvice.MFilesVAF.LoadingExample
{
	public partial class VaultApplication
	{
		public const string VEM_Loading = "SA.LoadingExample.VEM.Loading";

		/// <summary>
		/// Registers a Vault Extension Method with name "MyVaultExtensionMethod".
		/// Users must have at least MFVaultAccess.MFVaultAccessNone access to execute the method.
		/// </summary>
		/// <param name="env">The vault/object environment.</param>
		/// <returns>The any output from the vault extension method execution.</returns>
		/// <remarks>The input to the vault extension method is available in <see cref="EventHandlerEnvironment.Input"/>.</remarks>
		[VaultExtensionMethod(VEM_Loading,
			RequiredVaultAccess = MFVaultAccess.MFVaultAccessNone)]
		private string ExtensionMethod_Loading(EventHandlerEnvironment env)
		{
			Thread.Sleep(1000);

			return env.Input;
		}

	}
}
