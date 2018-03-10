using GreenClean.Model;
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
    public partial class Places : ContentPage
    {
        public event EventHandler<SelectedItemChangedEventArgs> DataSender;

        ObservableCollection<PlacesModel> places = new ObservableCollection<PlacesModel>();

        public Places(object placeObservable)
        {
            InitializeComponent();
            ListPlaces();
            Options.ItemsSource = places;
        }

        public void OnItemSelect(object sender, SelectedItemChangedEventArgs args)
        {
            (sender as ListView).SelectedItem = null;

            if (args.SelectedItem != null)
            {
                DataSender(sender, args);
            }
        }

        public void ListPlaces()
        {
            places.Add(new PlacesModel(0, "Home", "B1 L1 Imaginary Street, Quezon City"));
            places.Add(new PlacesModel(2, "Work", "20th flr. 1650 Imaginary Tower Opal Rd, Ortigas"));
        }

    }
}