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
        ObservableCollection<PlacesViewModel> places = new ObservableCollection<PlacesViewModel>();
        PlacesViewModel placesViewModel;
        PlacesForm placeform;
        public PlacesPage ()
		{
			InitializeComponent ();
            ListPlaces();
            PlacesList.ItemsSource = places;
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
            places.Add(new PlacesViewModel(obj));
            await Navigation.PopAsync();
        }

        public async void AddNewPlace(object sender, EventArgs a)
        {
            var placeform = new PlacesForm();
            placeform.DataSender += AddList;
            await Navigation.PushAsync(placeform);
        }

        public void ListPlaces()
        {
            places.Add(new PlacesViewModel(0, "Home", "B1 L1 Imaginary Street", "Barangay Example", "Quezon City"));
            places.Add(new PlacesViewModel(2, "Work", "20th flr. 1650 Imaginary Tower Opal Rd", "Barangay Test", "Ortigas City"));
        }
    }
}