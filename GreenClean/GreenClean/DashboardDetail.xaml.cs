using GreenClean.Model;
using GreenClean.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
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
            Task.Run(() => DashboardViewModel.GetList()).Wait();
            ItemsSource = DashboardViewModel.All;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (!HasAppeared)
            {
                await PlacesModel.GetList();
                HasAppeared = true;
            }
        }

        protected override void OnChildAdded(Element child)
        {
            base.OnChildAdded(child);
            if (DashboardViewModel.All[0].appointment != null)
            {
                CurrentPage = Children.FirstOrDefault();
            }
            
        }
    }
}