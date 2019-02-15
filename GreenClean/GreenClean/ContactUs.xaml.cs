using GreenClean.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenClean
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactUs : ContentPage
	{
		public ContactUs ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var client = new HttpClient();
            var response = await client.GetAsync(Constants.BaseUri + "/contact-us");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var deserialized = JsonConvert.DeserializeObject<ContactDetails>(content);
                ContactStack.BindingContext = deserialized;
            }

        }

        public class ContactDetails
        {
            public ContactDetails()
            {
                EmailCommand = new Command(() =>
                {
                    Device.OpenUri(new Uri(string.Format("mailto:{0}",Email)));
                });

                SmsCommand = new Command(() =>
                {
                    Device.OpenUri(new Uri(string.Format("tel:{0}", Mobile)));
                });
            }
            public string Mobile { get; set; }
            public string Landline { get; set; }
            public string Email { get; set; }

            public ICommand EmailCommand { get; set; }
            public ICommand SmsCommand { get; set; }
            public ICommand LandlineCommand { get; set; }
        }
    }
}