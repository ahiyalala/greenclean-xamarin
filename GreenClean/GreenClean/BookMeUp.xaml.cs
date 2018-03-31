using GreenClean.Model;
using GreenClean.ViewModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenClean
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BookMeUp : ContentPage
	{
		public BookMeUp (AppointmentRequest obj)
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