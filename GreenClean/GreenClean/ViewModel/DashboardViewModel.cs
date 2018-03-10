using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GreenClean.ViewModel
{
    class DashboardViewModel
    {
        public DashboardViewModel(string servicename, string description, string price)
        {
            ServiceName = servicename;
            Description = description;
            Price = price;
            SelectBooking = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PushAsync(new PreBooking(ServiceName));
            });
        }

        public string ServiceName { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }

        public ICommand SelectBooking { protected set; get; }

        public static IList<DashboardViewModel> All { private set; get; }

        static DashboardViewModel()
        {
            All = new List<DashboardViewModel>{
                new DashboardViewModel("Service Cleaning A", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum non nisl ac erat blandit faucibus eu quis lacus. Nullam et interdum sapien.", "Php 300"),
                new DashboardViewModel("Service Cleaning B", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum non nisl ac erat blandit faucibus eu quis lacus. Nullam et interdum sapien.", "Php 500"),
                new DashboardViewModel("Service Cleaning C", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum non nisl ac erat blandit faucibus eu quis lacus. Nullam et interdum sapien.", "Php 800")
            };
        }
    }
}
