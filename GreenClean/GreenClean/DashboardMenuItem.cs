using GreenClean.Model;
using GreenClean.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GreenClean
{

    public class DashboardMenuItem
    {

        public DashboardMenuItem(INavigation navigator)
        {
            OpenPage = new Command(async () =>
            {
                   await navigator.PushAsync(TargetType);
            });
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public Page TargetType { get; set; }

        public string VerticalLayout { get; set; }

        public ICommand OpenPage { get; set; }
    }
}