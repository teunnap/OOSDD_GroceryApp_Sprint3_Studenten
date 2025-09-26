using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Grocery.App.ViewModels
{
    public partial class GroceryListViewModel : BaseViewModel
    {
        public ObservableCollection<GroceryList> GroceryLists { get; set; }
        private readonly IGroceryListService _groceryListService;

        public GroceryListViewModel(IGroceryListService groceryListService) 
        {
            Title = "Boodschappenlijst";
            _groceryListService = groceryListService;
            GroceryLists = new();
            RefreshLists();
        }

        [RelayCommand]
        public async Task SelectGroceryList(GroceryList groceryList)
        {
            Dictionary<string, object> paramater = new() { { nameof(GroceryList), groceryList } };
            await Shell.Current.GoToAsync($"{nameof(Views.GroceryListItemsView)}?Titel={groceryList.Name}", true, paramater);
        }
        public override void OnAppearing()
        {
            base.OnAppearing();
            RefreshLists();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            GroceryLists.Clear();
        }

        private void RefreshLists()
        {
            var all = _groceryListService.GetAll();
            GroceryLists.Clear();
            foreach (var g in all) GroceryLists.Add(g);
        }

        [RelayCommand]
        public async Task NewList()
        {
            string name = await Shell.Current.DisplayPromptAsync("Nieuwe lijst", "Voer een naam in:", "OK", "Annuleren", "Naam", maxLength: 100, keyboard: Keyboard.Text);
            if (name == null) return; // 3a: geannuleerd
            name = name.Trim();
            if (name.Length == 0) return; // leeg, negeren

            try
            {
                GroceryList created = _groceryListService.Add(new GroceryList(0, name, DateOnly.FromDateTime(DateTime.Now), "#626262", 1));
                // Update overzicht
                GroceryLists.Add(created);
            }
            catch (InvalidOperationException ex)
            {
                await Shell.Current.DisplayAlert("Melding", ex.Message, "OK");
            }
        }
    }
}
