using GreenClean.Model;
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
	public partial class BookMeUp : ContentPage
	{
		public BookMeUp ()
		{
			InitializeComponent ();
            FindServiceAsync();

            
		}

        async void FindServiceAsync()
        {
            await Task.Delay(3000);
            

            Navigation.InsertPageBefore(new BookingDetailPage(), this);
            await Navigation.PopAsync();
        }

        
	}
}