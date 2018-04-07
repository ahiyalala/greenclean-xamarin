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
    public partial class DashboardDetail : CarouselPage
    {
        private static bool HasAppeared;
        public DashboardDetail()
        {
            InitializeComponent();
            HasAppeared = false;
            ItemsSource = DashboardViewModel.All;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (!HasAppeared)
            {
                await DashboardViewModel.GetList();
                await PlacesModel.GetList();
                HasAppeared = true;
            }
        }
    }
}