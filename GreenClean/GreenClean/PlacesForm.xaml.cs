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

		public PlacesForm ()
		{
			InitializeComponent ();
            Send.Text = "Add";
            Fields = new List<Entry>
            {
                PlaceName,StreetAddress,Barangay,City
            };
        }

        public PlacesForm(PlacesModel obj)
        {
            InitializeComponent();
            PlaceName.Text = obj.PlaceName;
            StreetAddress.Text = obj.StreetName;
            Barangay.Text = obj.Barangay;
            City.Text = obj.City;
            Send.Text = "Update";
            places = obj;
            Fields = new List<Entry>
            {
                PlaceName,StreetAddress,Barangay,City
            };

        }

        public void OnFocus(object sender, FocusEventArgs args)
        {
            (sender as Entry).PlaceholderColor = Color.Default;
        }

        public async void OnClick(object sender, EventArgs a)
        {
            var placename = PlaceName.Text;
            var streetaddress = StreetAddress.Text;
            var barangay = Barangay.Text;
            var city = City.Text;
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