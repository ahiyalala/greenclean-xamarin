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
	public partial class BookingDetailPage : ContentPage
	{
        private bool displayAlerts = false;

        public BookingDetailPage(Appointment appointment, bool isFromBooking = false)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, true);
            ServiceDescription.Text = string.Format("{0}", appointment.Service.ServiceName);
            ServicePrice.Text = string.Format("Php {0} with {1} housekeepers", appointment.Price.ToString(), appointment.Housekeeper.Count.ToString());
            EmployeeList.ItemsSource = appointment.Housekeeper;
            displayAlerts = isFromBooking;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (displayAlerts)
            {
                DisplayAlert("Successful!", "We have successfully scheduled you an appointment", "Okay, thanks!");
            }
            displayAlerts = false;
        }

        public async void ContactHousekeeper(object sender, SelectedItemChangedEventArgs e)
        {
            if (e == null || e.SelectedItem == null) return;
            
            var housekeeper = (Housekeeper)e.SelectedItem;
            var contactNumber = housekeeper.ContactNumber;
            var call = await DisplayAlert("Contact Housekeeper", string.Format("Call {0} or send a message? {1}", housekeeper.FirstName, contactNumber), "Call", "SMS");
            if (call)
            {
                Device.OpenUri(new Uri(string.Format("tel:+63{0}", contactNumber)));
            }
            else
            {
                Device.OpenUri(new Uri(string.Format("sms:+63{0}", contactNumber)));
            }

            EmployeeList.SelectedItem = null;
        }

        async void GoToHome(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }


	}
}