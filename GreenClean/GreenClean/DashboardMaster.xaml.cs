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
        public event EventHandler<EventArgs> Logout;
        public INavigation Navigator { get; set; }
        public DashboardMaster(INavigation navigator)
        {
            InitializeComponent();
            Navigator = navigator;
            BindingContext = new DashboardMasterViewModel(Navigator);
            ListView = MenuItemsListView;
        }

        public void LogoutEvent(object sender, EventArgs args) {
            Logout(sender, args);
        }

        public async void OpenPrivacyPolicy(object sender, EventArgs args)
        {
            await Navigator.PushAsync(new PrivacyPolicy(),false);
        }

        public async void OpenTnC(object sender, EventArgs args)
        {
            await Navigator.PushAsync(new TermsAndConditions(), false);
        }

        public async void OpenContactUs(object sender, EventArgs args)
        {
            await Navigator.PushAsync(new ContactUs(), false);
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
            
            public DashboardMasterViewModel(INavigation navigator)
            {
                MenuItems = new ObservableCollection<DashboardMenuItem>(new[]
                {
                    new DashboardMenuItem(navigator) { Id = 3, Title = "Profile", TargetType = new Profile(), VerticalLayout="FillAndExpand" },
                    new DashboardMenuItem(navigator)  { Id = 0, Title = "Places", TargetType = new PlacesPage(), VerticalLayout="FillAndExpand" },
                    new DashboardMenuItem(navigator)  { Id = 0, Title = "Payments", TargetType = new PaymentPage(), VerticalLayout="FillAndExpand" },
                    new DashboardMenuItem(navigator)  { Id = 4, Title = "History", TargetType = new History(), VerticalLayout="FillAndExpand"},
                    new DashboardMenuItem(navigator)  { Id = 5, Title = "FAQ", TargetType = new Faq(), VerticalLayout="FillAndExpand"}
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

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }
    }
}