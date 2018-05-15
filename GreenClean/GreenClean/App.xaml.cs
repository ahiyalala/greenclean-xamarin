using GreenClean.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace GreenClean
{
	public partial class App : Application
	{
		public App ()
		{
            
			InitializeComponent();

            if (!Application.Current.Properties.ContainsKey("token"))
            {
                MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                MainPage = new NavigationPage(new Dashboard());
            }

            //MainPage = new NavigationPage(new SplashScreen());

        }

		protected async override void OnStart ()
		{
            if (Application.Current.Properties.ContainsKey("token"))
                await Customer.GetProfile();
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
