﻿using GreenClean.ViewModel;
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
        public event EventHandler<TappedEventArgs> OpenBookingPage;
		public AppointmentsDashboard ()
		{
			InitializeComponent ();
            isInitial = true;
            ImageBanner.Source = ImageSource.FromResource("GreenClean.Assets.plantspray.jpg");
		}

        public async void OpenBooking(object sender, TappedEventArgs args)
        {
            if(AppointmentDashboardViewmodel.Pending.Count < 3)
            {
                OpenBookingPage(sender, args);
            }
            else
            {
                await DisplayAlert("Cannot add new booking", "You've reached the maximum allowed number of appointments", "OK");
            }
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