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
        
        public Places()
        {
            InitializeComponent();
            Options.ItemsSource = PlacesModel.PlacesList;
        }

        public void OnItemSelect(object sender, SelectedItemChangedEventArgs args)
        {
            (sender as ListView).SelectedItem = null;

            if (args.SelectedItem != null)
            {
                DataSender(sender, args);
            }
        }

    }
}