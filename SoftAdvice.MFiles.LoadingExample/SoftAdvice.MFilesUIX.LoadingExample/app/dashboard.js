async function OnNewDashboard(dashboard) {
	await dashboard.Events.Register(MFiles.Event.Started, async () => {

		var id = dashboard.CustomData.id;

		console.log("Dashboard " + id + " has started")

		for (var i = 0; i < 100; i++) {
			dashboard.ShellFrame.ShellUI.Vault.VaultExtensionMethodsOperations.RunExtensionMethod({
				method_name: "SA.LoadingExample.VEM.Loading",
				input: i.toString()
			}).then((result) => {
				console.log(id + " - " + result.output)
			});

		}

	});
}


