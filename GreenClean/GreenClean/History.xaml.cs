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
	public partial class History : ContentPage
	{
		public History ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = new HistoryViewModel(this.Navigation);
            await viewModel.GetFinishedAppointments();

            Indicator.IsVisible = false;
            if(viewModel.FinishedAppointments.Count > 0)
            {
                AppointmentListView.ItemsSource = viewModel.FinishedAppointments;
            }
            else
            {
                EmptyTextMessage.IsVisible = true;
            }

        }
    }
}