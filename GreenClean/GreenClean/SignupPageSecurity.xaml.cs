using GreenClean.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Bcr = BCrypt.Net;

namespace GreenClean
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignupPageSecurity : ContentPage
	{
        Regex alphabetOnly = new Regex("/^[A-z]+$/");

        HttpClient client;
        const string UriUsers = "http://greenclean-cb.southeastasia.cloudapp.azure.com/api/users";
		public SignupPageSecurity ()
		{
            
            client = new HttpClient();
            InitializeComponent ();
            BirthDate.SetValue(DatePicker.MaximumDateProperty, DateTime.Now);
        }

        async void SubmitForm(object sender, EventArgs e)
        {
            SubmitButton.IsEnabled = false;

            Customer customer = new Customer
            {
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                BirthDate = BirthDate.Date.ToString("yyyy-MM-dd"),
                EmailAddress = Email.Text,
                Password = Bcr.BCrypt.HashPassword(Password.Text),
                ContactNumber = Mobile.Text
            };


            var item = JsonConvert.SerializeObject(customer);
            var content = new StringContent(item, Encoding.UTF8, "application/json");
            
            var response = await client.PostAsync(UriUsers, content);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                
                await DisplayAlert("Yay", "Successful", "Oki");
            }
            
            await Navigation.PopAsync();
        }


	}
}