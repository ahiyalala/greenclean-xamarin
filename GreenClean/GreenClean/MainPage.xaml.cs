using GreenClean.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GreenClean
{
	public partial class MainPage : ContentPage
	{
        const string UriUsers = "http://greenclean-cb.southeastasia.cloudapp.azure.com/api/users/login";
        HttpClient client;
        public MainPage()
		{
			InitializeComponent();
            client = new HttpClient();

		}
        
        async void OnLogin(object sender, EventArgs e)
        {
            LoginButton.IsEnabled = false;
            SignUpButton.IsEnabled = false;
            
            var customer = new Customer
            {
                EmailAddress = Username.Text,
                Password = Password.Text
            };

            var item = JsonConvert.SerializeObject(customer);
            var content = new StringContent(item, Encoding.UTF8, "application/json");


            var response = await client.PostAsync(UriUsers,content);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                Application.Current.Properties["token"] = result;
                Navigation.InsertPageBefore(new Dashboard(), this);
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Login failed", result, "Try again");
                LoginButton.IsEnabled = true;
                SignUpButton.IsEnabled = true;
            }
            
        }

        async void ToSignUp(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignupPageSecurity(),true);
        }
	}
}
