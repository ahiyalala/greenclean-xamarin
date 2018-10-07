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
	public partial class SplashScreen : ContentPage
	{
		public SplashScreen ()
		{
			InitializeComponent ();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (!Application.Current.Properties.ContainsKey("token"))
            {
                Navigation.InsertPageBefore(new MainPage(), this);
                await Navigation.PopAsync();
            }
            else
            {
                await Customer.GetProfile();
                Navigation.InsertPageBefore(new DashboardDetail(), this);
                await Navigation.PopAsync();
            }
        }
    }
}