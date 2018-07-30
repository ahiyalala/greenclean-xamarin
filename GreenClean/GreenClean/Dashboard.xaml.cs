using GreenClean.Model;
using GreenClean.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenClean
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : MasterDetailPage
    {
        public Dashboard()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as DashboardMenuItem;
            if (item == null)
                return;

            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;

            if(item.Id == 2)
            {
                Application.Current.Properties.Clear();
                AppointmentDashboardViewmodel.Finished = new ObservableCollection<AppointmentDashboardViewmodel>();
                AppointmentDashboardViewmodel.Pending = new ObservableCollection<AppointmentDashboardViewmodel>();
                DashboardViewModel.All = new ObservableCollection<DashboardViewModel>();
                PlacesPage.HasAppeared = false;
                PlacesModel.PlacesList = new List<PlacesModel>();
                Navigation.InsertPageBefore(new MainPage(), this);
                await Navigation.PopAsync();
            }
            else
            {
                await Application.Current.MainPage.Navigation.PushAsync(item.TargetType);
            }
            
        }
    }
}