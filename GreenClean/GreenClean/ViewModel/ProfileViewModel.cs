using GreenClean.DependencyServices;
using GreenClean.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GreenClean.ViewModel
{
    class ProfileViewModel
    {
        public ProfileViewModel()
        {
            Profile = Customer.Current;
            Update = new Command( async () => {
                var _customer = Customer.Current;
                Customer.Current = Profile;

                var is_updated = await Customer.UpdateProfile();

                if (is_updated)
                {
                    DependencyService.Get<IMessage>().LongAlert("Profile updated!");
                    await Navigation.PopAsync();
                }
                else
                {
                    DependencyService.Get<IMessage>().LongAlert("Profile update failed!");
                }
                Customer.Current = _customer;
            });
        }

        public Customer Profile { get; set; }
        public ICommand Update { get; set; }
        public INavigation Navigation { get; set; }
    }
}
