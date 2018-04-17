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
    public partial class DashboardDetail : TabbedPage
    {
        private static bool HasAppeared;
        public DashboardDetail()
        {
            InitializeComponent();
            HasAppeared = false;
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if(AppointmentDashboardViewmodel.All.Count == 0)
            {
                CurrentPage = Children[1];
            }
            else
            {
                CurrentPage = Children[0];
            }
            
            if (!HasAppeared)
            {
                Task.Run(async () => {
                    await PlacesModel.GetList();
                    await DashboardViewModel.GetList();
                    await AppointmentDashboardViewmodel.GetList();
                });
                HasAppeared = true;
            }
        }
    }
}