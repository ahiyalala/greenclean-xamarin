using FFImageLoading.Svg.Forms;
using GreenClean.DependencyServices;
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
        private Appointment _appointment;

        public BookingDetailPage(Appointment appointment, bool isFromBooking = false)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, true);
            ServiceDescription.Text = string.Format("{0}", appointment.Service.ServiceName);
            
            if(appointment.Rating > 0)
            {
                ServiceRating.Text = string.Format(" | Rated {0}", appointment.Rating);
            }
            else
            {
                StarIcon.Text = "";
            }
            ServicePrice.Text = string.Format("Php {0} with {1} housekeepers", appointment.Price.ToString(), appointment.Housekeeper.Count.ToString());
            ServiceSchedule.Text = string.Format("{0} {1}-{2}", appointment.ScheduleDate.ToString("yyyy-MM-dd"), appointment.ScheduleTimeStart.ToString("hh:mm tt"), appointment.ScheduleTimeEnd.ToString("hh:mm tt"));
            EmployeeList.ItemsSource = appointment.Housekeeper;
            displayAlerts = isFromBooking;
            _appointment = appointment;
            if (isFromBooking)
            {
                CancelationFrame.IsVisible = false;
            }
            else
            {
                if ((_appointment.ScheduleDate.Date - DateTime.Now.Date).TotalDays < 1)
                {
                    CancelText.Text = "Cancellation period over";
                    CancelText.TextColor = Color.FromHex("a5a5a5");
                    CancelButton.BorderColor = Color.FromHex("dddddd");
                    CancelButton.BackgroundColor = Color.FromHex("dddddd");
                    CancelButton.IsEnabled = false;
                    PolicyText.IsVisible = false;
                }
                else
                {
                    CancellationPolicy.Text = string.Format("You can cancel this appointment until {0}", _appointment.ScheduleDate.AddDays(-1).ToString("yyyy-MM-dd"));
                }
            }
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
            if (e == null || e.SelectedItem == null || _appointment.Rating > 0) return;
            
            var housekeeper = (Housekeeper)e.SelectedItem;
            var contactNumber = housekeeper.ContactNumber;
            var contact = await DisplayActionSheet(string.Format("Call {0} or send a message? {1}", housekeeper.FirstName, contactNumber),"Cancel", null , "Call", "SMS");
            if (contact == "Call")
            {
                Device.OpenUri(new Uri(string.Format("tel:+63{0}", contactNumber)));
            }
            else if(contact == "SMS")
            {
                Device.OpenUri(new Uri(string.Format("sms:+63{0}", contactNumber)));
            }

            EmployeeList.SelectedItem = null;
        }

        async void CancelBooking(object sender, EventArgs e)
        {
            var proceed = await DisplayAlert("Cancel appointment", "Do you want to cancel?", "Yes","No");
            if (!proceed) return;

            var cancel_status = await Appointment.CancelAppointment(_appointment.BookingRequestId);
            if (cancel_status)
            {
                AppointmentDashboardViewmodel.isOrigin = true;
                DependencyService.Get<IMessage>().ShortAlert("Appointment cancelled");
                await Navigation.PopAsync(false);
            }
            else
            {
                DependencyService.Get<IMessage>().ShortAlert("Cancellation failed");
            }
        }


	}
}