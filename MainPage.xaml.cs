using System.Collections.ObjectModel;
using ItemInventory.DataAccess;
using ItemInventory.Models;

namespace ItemInventory
{
	/// <summary>
	/// Data entry page. Saves items to SQLite, then shows every saved item.
	/// </summary>
	public partial class MainPage : ContentPage
	{
		// Data access layer for the Items table
		private readonly ItemData itemData;

		// List shown on screen; the CollectionView updates when it changes
		public ObservableCollection<Item> Items { get; set; } = new();

		public MainPage()
		{
			InitializeComponent();
			itemData = new ItemData();

			// Lets the XAML bind to the Items list on this page
			BindingContext = this;

			// Load any items already saved in the database
			_ = UpdateItemList();
		}

		/// <summary>
		/// Runs when Save is clicked.
		/// Input: the three text fields. Output: a saved item plus a refreshed list.
		/// </summary>
		private async void OnSaveClicked(object sender, EventArgs e)
		{
			// Make sure every field has data; warn the user and stop if not
			if (string.IsNullOrWhiteSpace(txtItemId.Text) ||
				string.IsNullOrWhiteSpace(txtItemName.Text) ||
				string.IsNullOrWhiteSpace(txtItemDescription.Text))
			{
				await DisplayAlert("Missing Information", "Please fill in all fields before saving.", "OK");
				return;
			}

			// Build the item from the entered values
			var item = new Item
			{
				ItemId = txtItemId.Text.Trim(),
				ItemName = txtItemName.Text.Trim(),
				ItemDescription = txtItemDescription.Text.Trim()
			};

			// Save it to the database
			await itemData.AddItemAsync(item);

			// Clear the form for the next item
			txtItemId.Text = string.Empty;
			txtItemName.Text = string.Empty;
			txtItemDescription.Text = string.Empty;

			// Show the updated list
			await UpdateItemList();
		}

		/// <summary>
		/// Reloads the Items list from the database.
		/// </summary>
		private async Task UpdateItemList()
		{
			List<Item> items = await itemData.GetItemsAsync();
			Items.Clear();
			foreach (Item item in items)
			{
				Items.Add(item);
			}
		}
	}
}