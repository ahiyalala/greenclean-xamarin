using GreenClean.Model;
using GreenClean.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace GreenClean
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardDetail : TabbedPage
    {
        private static bool HasAppeared;
        public DashboardDetail()
        {
            InitializeComponent();
            HasAppeared = false;
            var _appointmentPage = new AppointmentsDashboard();
            _appointmentPage.OpenBookingPage += OpenBooking;
            var appointmentPage = new NavigationPage(_appointmentPage);
            appointmentPage.Title = "Appointments";
            var dashboardpage = new DashboardMaster(this.Navigation);
            dashboardpage.Logout += Logout;
            var optionsPage = new NavigationPage(dashboardpage);
            optionsPage.Title = "Options";
            optionsPage.Icon = "ic_person.png";
            appointmentPage.Icon =  "ic_access_alarms.png";
            BarTextColor = Color.White;
            Children.Add(appointmentPage);
            Children.Add(optionsPage);
        }


        public async void OpenBooking(object sender, TappedEventArgs args)
        {
            await Navigation.PushAsync(new ServicesDashboard(), false);
        }

        public async void Logout(object sender, EventArgs args)
        {
            var logout = await DisplayAlert("Logging out", "Are you sure?", "Yes", "No");
            if (!logout) return;

            Application.Current.Properties.Clear();
            AppointmentDashboardViewmodel.Finished = new ObservableCollection<AppointmentDashboardViewmodel>();
            AppointmentDashboardViewmodel.Pending = new ObservableCollection<AppointmentDashboardViewmodel>();
            DashboardViewModel.All = new ObservableCollection<DashboardViewModel>();
            PlacesPage.HasAppeared = false;
            PlacesModel.PlacesList = new List<PlacesModel>();
            Navigation.InsertPageBefore(new MainPage(),this);
            await Navigation.PopToRootAsync();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if(!HasAppeared)
                await AppointmentDashboardViewmodel.GetList();
            
            if (!HasAppeared)
            {
                await PlacesModel.GetList();
                await DashboardViewModel.GetList();
                HasAppeared = true;
            }
        }
    }
}