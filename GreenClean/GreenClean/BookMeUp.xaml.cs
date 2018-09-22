using GreenClean.DependencyServices;
using GreenClean.Model;
using GreenClean.ViewModel;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenClean
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BookMeUp : ContentPage
	{
        AppointmentRequest request;
        CancellationTokenSource token;
        public BookMeUp (AppointmentRequest obj)
		{
			InitializeComponent ();
            token = new CancellationTokenSource();
            request = obj;
		}



        protected override async void OnAppearing()
        {
            base.OnAppearing();



            for(int x = 3; x >= 1 && !token.IsCancellationRequested; x--)
            {
                CancelBtn.Text = $"Cancel request {x.ToString()}";
                await Task.Delay(1000);
            }
            if (token.IsCancellationRequested)
            {
                DependencyService.Get<IMessage>().ShortAlert("Search cancelled");
                await Navigation.PopAsync();
                
            }
            else
            {
                var appointment = await Appointment.BookAsync(request);
                
                if (appointment == null)
                {
                    await DisplayAlert("Booking failed", "All employees are completely booked, please select another time or date", "Try again");
                    Navigation.InsertPageBefore(new PreBooking(request), this);
                    await Navigation.PopAsync();
                }
                else
                {
                    Navigation.InsertPageBefore(new BookingDetailPage(appointment, true), this);
                    await Navigation.PopAsync();
                }
            }
            
        }

        public void CancelBooking()
        {
            CancelBtn.IsEnabled = false;
            token.Cancel();
            
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            token.Cancel();
        }

    }
}