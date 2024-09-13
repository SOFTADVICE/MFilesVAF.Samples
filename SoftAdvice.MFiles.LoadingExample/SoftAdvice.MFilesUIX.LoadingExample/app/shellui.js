"use strict";
// Entry point of the shell ui application.
async function OnNewShellUI(shellUI) {

	// Wait for shell frames to be created.
	await shellUI.Events.Register(MFiles.Event.NewNormalShellFrame, OnNewNormalShellFrame);

	var myTab;
	var myDashboard;

	// Handler for shell frame created event.
	async function OnNewNormalShellFrame(shellFrame) {
		// Add tab to right pane, when the shell frame is started.
		await shellFrame.Events.Register(MFiles.Event.Started, OnStarted);
		async function OnStarted() {
			myTab = await shellFrame.RightPane.AddTab('load-tab', 'Loading', "_last", null);
			await myTab.SetVisible(false);

			shellFrame.Events.Register(MFiles.Event.NewShellListing, async (listing) => {
				listing.Events.Register(MFiles.Event.SelectionChanged, SelectionChanged)
			});
		}

		async function SelectionChanged(items) {
			await HideDashboard()
			if (items.Count == 1)
				await ShowDashboard()
		}

		async function ShowDashboard() {
			await myTab.SetVisible(true);

			var guid = self.crypto.randomUUID();

			console.log('opening dashboard ' + guid)

			var input = { id: guid }

			if (myDashboard)
				await myDashboard.UpdateCustomData(input)
			else
				myDashboard = await myTab.ShowDashboard('loading-dashboard', input)

			await myTab.Select();
		}

		async function HideDashboard() {
			await myTab.SetVisible(false)
		}
	}

}