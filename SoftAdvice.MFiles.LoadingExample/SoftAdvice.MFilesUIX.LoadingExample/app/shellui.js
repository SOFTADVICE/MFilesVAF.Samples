"use strict";
// Entry point of the shell ui application.
async function OnNewShellUI(shellUI) {

	// Wait for shell frames to be created.
	await shellUI.Events.Register(MFiles.Event.NewNormalShellFrame, OnNewNormalShellFrame);

	var myTab;

	// Handler for shell frame created event.
	async function OnNewNormalShellFrame(shellFrame) {
		// Add tab to right pane, when the shell frame is started.
		var myTab;
		var selectedItem = null;
		await shellFrame.Events.Register(MFiles.Event.Started, OnStarted);
		async function OnStarted() {
			shellFrame.Events.Register(MFiles.Event.NewShellListing, async (listing) => {
				listing.Events.Register(MFiles.Event.SelectionChanged, SelectionChanged)
			});
		}

		async function SelectionChanged(items) {
			await RemoveTab()
			if (items.Count == 1)
				await ShowTab()
		}

		async function ShowTab() {
			myTab = await shellFrame.RightPane.AddTab('load-tab', 'Loading', "_last", null);

			var guid = self.crypto.randomUUID();

			console.log('opening dashboard ' + guid)

			await myTab.ShowDashboard('loading-dashboard', { id: guid })

			await myTab.Select();
		}

		async function RemoveTab() {

			if (myTab)
				await myTab.Remove();
		}
	}

}