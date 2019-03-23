using GreenClean.Model;
using GreenClean.Utilities;
using GreenClean.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenClean
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlacesForm : ContentPage
	{
        public event EventHandler<FormEvent> DataSender;
        PlacesModel places;
        public List<Entry> Fields;
        public List<PlacesOptions> itemsDirectory;
		public PlacesForm ()
		{
			InitializeComponent ();
            Send.Text = "Add";
            Fields = new List<Entry>
            {
                PlaceName,StreetAddress,BarangayText,CityText
            };
            Barangay.IsVisible = true;
            City.IsVisible = true;
            BarangayText.IsVisible = false;
            CityText.IsVisible = false;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            itemsDirectory = await PlacesOptions.GetList();
            City.ItemsSource = null;
            List<string> items = new List<string>();
            var _list = itemsDirectory.GroupBy(x => x.City).Select(x => x.First());
            foreach(var item in _list)
            {
                items.Add(item.City);
            }
            City.ItemsSource = items;
        }

        public PlacesForm(PlacesModel obj)
        {
            InitializeComponent();
            PlaceName.Text = obj.PlaceName;
            StreetAddress.Text = obj.StreetName;
            BarangayText.Text = obj.Barangay;
            CityText.Text = obj.City;
            Send.IsVisible = false;
            places = obj;
            Fields = new List<Entry>
            {
                PlaceName,StreetAddress,BarangayText,CityText
            };
            Barangay.IsVisible = false;
            City.IsVisible = false;
            BarangayText.IsVisible = true;
            CityText.IsVisible = true;
            PlaceName.IsEnabled = false;
            StreetAddress.IsEnabled = false;
        }

        public void OnFocus(object sender, FocusEventArgs args)
        {
            (sender as Entry).PlaceholderColor = Color.Default;
        }

        public void OnCitySelect(object sender, EventArgs a)
        {
            Barangay.IsEnabled = false;
            BarangayText.Text = null;
            var _city = City.Items[City.SelectedIndex];
            CityText.Text = _city;
            var _barangayList = itemsDirectory.Where(x => x.City == _city).ToList();
            Barangay.ItemsSource = null;
            List<string> _barangayItems = new List<string>();
            foreach(var items in _barangayList)
            {
                _barangayItems.Add(items.Barangay);
            }
            Barangay.ItemsSource = _barangayItems;
            Barangay.IsEnabled = true;
        }

        public void OnBarangaySelect(object sender, EventArgs a)
        {
            var field = (sender as Picker);
            BarangayText.Text = field.Items[field.SelectedIndex];
        }

        public async void OnClick(object sender, EventArgs a)
        {
            var placename = PlaceName.Text;
            var streetaddress = StreetAddress.Text;
            var barangay = BarangayText.Text;
            var city = CityText.Text;
            var isValid = true;
            foreach(var field in Fields)
            {
                if(field.Text == "" || field.Text == null)
                {
                    field.PlaceholderColor = Color.Red;
                    isValid = false;
                }
            }

            if (!isValid)
            {
                await DisplayAlert("Invalid input", "Fields cannot be empty", "Got it");
                return;
            }

            if (places != null)
            {
                places.PlaceName = placename;
                places.StreetName = streetaddress;
                places.Barangay = barangay;
                places.City = city;
            }
            else
            {
                places = new PlacesModel(placename, streetaddress, barangay, city);
            }
            var args = new FormEvent()
            {
                Object = places
            };
            DataSender(sender, args);
        }
	}
}