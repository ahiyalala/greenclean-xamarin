using GreenClean.Model;
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
		public BookingDetailPage (Appointment appointment)
		{
			InitializeComponent ();
            ServiceDate.Text = appointment.ScheduleDate.ToString("yyyy-MM-dd");
            ServiceType.Text = appointment.Service.ServiceName;
            PlaceName.Text = appointment.Places.PlaceDetail;
            PaymentType.Text = appointment.PaymentType;
            PaymentDue.Text = appointment.Service.Price;
            HousekeeperName.Text = appointment.Housekeeper.FullName;
        }
        
        async void GoToHome(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }


	}
}