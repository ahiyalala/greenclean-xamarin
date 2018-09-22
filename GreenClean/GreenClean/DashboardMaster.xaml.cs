using GreenClean.Model;
using GreenClean.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenClean
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardMaster : ContentPage
    {
        public ListView ListView;

        public DashboardMaster()
        {
            InitializeComponent();

            BindingContext = new DashboardMasterViewModel();
            ListView = MenuItemsListView;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var profileViewModel = new ProfileDashboardViewModel();
            await profileViewModel.GetProfile();
            ProfileLabel.BindingContext = profileViewModel;

        }

        class ProfileDashboardViewModel : ViewBaseModel
        {
           public string FullName { get; set; }

            public async Task GetProfile()
            {
                await Customer.GetProfile();
                FullName = Customer.Current.FullName;
            }
        }

        class DashboardMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<DashboardMenuItem> MenuItems { get; set; }
            
            public DashboardMasterViewModel()
            {
                MenuItems = new ObservableCollection<DashboardMenuItem>(new[]
                {
                    new DashboardMenuItem { Id = 3, Title = "Profile", TargetType = new Profile(), VerticalLayout="FillAndExpand" },
                    new DashboardMenuItem { Id = 0, Title = "Places", TargetType = new PlacesPage(), VerticalLayout="FillAndExpand" },
                    new DashboardMenuItem { Id = 1, Title = "Payments", TargetType = new PaymentPage(), VerticalLayout="FillAndExpand" },
                    new DashboardMenuItem {Id = 2, Title="Logout",TargetType = new MainPage(), VerticalLayout="End" }
                });
            }
            
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}