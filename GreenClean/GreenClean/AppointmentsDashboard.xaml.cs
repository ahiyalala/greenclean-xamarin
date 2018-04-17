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

		public AppointmentsDashboard ()
		{
			InitializeComponent ();
            Appointments.ItemsSource = AppointmentDashboardViewmodel.All;
		}
	}
}