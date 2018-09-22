using GreenClean.DependencyServices;
using GreenClean.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GreenClean.ViewModel
{
    class ProfileViewModel : ViewBaseModel
    {
        #region fields
        Customer __customerField;
        #endregion
        public ProfileViewModel()
        {
            Profile = Customer.Current;
            Update = new Command( async () => {
                var _customer = Customer.Current;

                var is_updated = await Customer.UpdateProfile(_customer);

                if (is_updated)
                {
                    DependencyService.Get<IMessage>().LongAlert("Profile updated!");
                    Customer.Current = Profile;
                    await Navigation.PopAsync();
                }
                else
                {
                    DependencyService.Get<IMessage>().LongAlert("Profile update failed!");
                }
                Customer.Current = _customer;
            });
        }
        
        public Customer Profile {
            get
            {
                return __customerField;
            }
            set
            {
                __customerField = value;
                OnPropertyChanged("Profile");
            }
        }
        public ICommand Update { get; set; }
        public INavigation Navigation { get; set; }

        public static async Task GetProfile()
        {
            await Customer.GetProfile();
        }
    }
}
