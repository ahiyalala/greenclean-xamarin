using GreenClean.Model;
using GreenClean.ViewModel;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenClean
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BookMeUp : ContentPage
	{
        AppointmentRequest request;
		public BookMeUp (AppointmentRequest obj)
		{
			InitializeComponent ();
            request = obj;
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var appointment = await Appointment.BookAsync(request);

            if(appointment == null)
            {
                await DisplayAlert("Oops", "We can't find you a housekeeper", "Try again");
                Navigation.InsertPageBefore(new PreBooking(request), this);
                await Navigation.PopAsync();
            }
            else
            {
                Navigation.InsertPageBefore(new BookingDetailPage(appointment), this);
                await Navigation.PopAsync();
            }
            
        }

        
	}
}