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
	public partial class PostJobPage : ContentPage
	{
        private string _appointmentId;

		public PostJobPage (string appointmentId)
		{
            _appointmentId = appointmentId;
			InitializeComponent ();
            PostJobViewModel.Current = new PostJobViewModel()
            {
                Navigation = Navigation
            };
            BindingContext = PostJobViewModel.Current;

		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            PostJobViewModel.Current.AppointmentData = await Appointment.GetSpecificAppointmentAsync(_appointmentId);
            var appointmentData = PostJobViewModel.Current.AppointmentData;
            ServiceType.Text = appointmentData.Service.ServiceName;
            ServiceImage.Source = string.Format("{0}/img/{1}", Constants.BaseUri, appointmentData.Service.ServiceImage);
            ServiceDescription.Text = string.Format("Php {0} with {1} housekeepers", appointmentData.Price, appointmentData.Housekeeper.Count);
            Indicator.IsVisible = false;
            Info.IsVisible = true;
        }
    }
}