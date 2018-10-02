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
	public partial class AppointmentsDashboard : ContentPage
	{
        public bool isInitial;
		public AppointmentsDashboard ()
		{
			InitializeComponent ();
            isInitial = true;
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            
            
            if (AppointmentDashboardViewmodel.isOrigin)
            {
                Indicator.IsVisible = true;
                Appointments.IsVisible = false;
                Appointments.ItemsSource = null;
                await AppointmentDashboardViewmodel.GetList();
                Appointments.ItemsSource = AppointmentDashboardViewmodel.Pending;
            }
            else if(isInitial)
            {
                await AppointmentDashboardViewmodel.GetList();
                Appointments.ItemsSource = AppointmentDashboardViewmodel.Pending;
            }
            if(AppointmentDashboardViewmodel.Pending.Count == 0)
            {
                NullText.IsVisible = true;
            }
            else
            {
                NullText.IsVisible = false;
            }
            Indicator.IsVisible = false;
            Appointments.IsVisible = true;

            AppointmentDashboardViewmodel.isOrigin = false;
            isInitial = false;
        }

        private async void Refresher(object sender, EventArgs args)
        {
            Appointments.IsRefreshing = true;
            await AppointmentDashboardViewmodel.GetList();
            Appointments.ItemsSource = AppointmentDashboardViewmodel.Pending;

            Appointments.IsRefreshing = false;
        }
    }
}