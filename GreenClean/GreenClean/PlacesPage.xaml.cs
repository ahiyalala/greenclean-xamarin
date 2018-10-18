using GreenClean.Model;
using GreenClean.Utilities;
using GreenClean.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenClean
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlacesPage : ContentPage
    {
        public static ObservableCollection<PlacesViewModel> places = new ObservableCollection<PlacesViewModel>();
        PlacesViewModel placesViewModel;
        PlacesForm placeform;
        public static bool HasAppeared;
        public PlacesPage ()
		{
			InitializeComponent ();
            PlacesList.ItemsSource = null;
            HasAppeared = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (!HasAppeared)
            {
                var placeslist = PlacesViewModel.GetCollectionList(PlacesModel.PlacesList);
                PlacesList.ItemsSource = places;
                foreach(var place in placeslist)
                {
                    places.Add(place);
                }
                HasAppeared = true;
            }
                
        }



        public async void OnItemSelect(object sender, SelectedItemChangedEventArgs args)
        {
            (sender as ListView).SelectedItem = null;

            if (args.SelectedItem != null)
            {
                placesViewModel = args.SelectedItem as PlacesViewModel;
                placeform = new PlacesForm(placesViewModel.Places);
                placeform.DataSender += UpdateList;
                await Navigation.PushAsync(placeform);

            }
        }

        public async void UpdateList(object sender, FormEvent args)
        {
            placesViewModel.Places = args.Object as PlacesModel;
            await Navigation.PopAsync();
        }

        public async void AddList(object sender, FormEvent args)
        {
            var obj = args.Object as PlacesModel;
            var isSuccessful = await PlacesModel.AddToList(obj);
            if (isSuccessful)
            {
                places.Add(new PlacesViewModel(obj));
            }
            else
            {
                await DisplayAlert("Oops", "Something went wrong", "Try again");
            }
            await Navigation.PopAsync();
        }

        public async void AddNewPlace(object sender, EventArgs a)
        {
            var placeform = new PlacesForm();
            placeform.DataSender += AddList;
            await Navigation.PushAsync(placeform);
        }
    }
}