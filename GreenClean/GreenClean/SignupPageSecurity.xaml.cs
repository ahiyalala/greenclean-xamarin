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
        
		public SignupPageSecurity ()
		{
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

            var isSuccessful = await Customer.SignUpAsync(customer);

            if (isSuccessful)
            {
                await DisplayAlert("Yay", "Successful", "Oki");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Oops", "Something went wrong with your sign up :(", "Try again");
            }

        }


	}
}