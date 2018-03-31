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
        public DashboardDetail()
        {
            InitializeComponent();
            Task.Run(async () =>
            {
                if(DashboardViewModel.All.Count == 0){ await DashboardViewModel.GetList(); };

                await PlacesModel.GetList();
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ItemsSource = DashboardViewModel.All;
        }
    }
}