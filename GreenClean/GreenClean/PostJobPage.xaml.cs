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
	public partial class PostJobPage : ContentPage
	{
        private string _appointmentId;

		public PostJobPage (string appointmentId)
		{
            _appointmentId = appointmentId;
			InitializeComponent ();
            PostJobViewModel.Current = new PostJobViewModel();
            BindingContext = PostJobViewModel.Current;
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            PostJobViewModel.Current.AppointmentData = await Appointment.GetSpecificAppointmentAsync(_appointmentId);
            Indicator.IsVisible = false;
            Info.IsVisible = true;
        }
    }
}