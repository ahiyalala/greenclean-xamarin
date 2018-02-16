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

            StartActivity();

		}

        async void StartActivity()
        {
            await Task.Delay(3000);
            await Navigation.PushAsync(new MainPage());
        }
	}
}