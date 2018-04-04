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
            
            
            
        }

		protected override void OnStart ()
		{
            
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
