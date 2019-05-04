using GreenClean.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        List<Entry> controls;

		public SignupPageSecurity ()
		{
            InitializeComponent ();
            controls = new List<Entry>
            {
                FirstName,LastName,Email,Password,Mobile
            };

            FirstName.Completed += (sender, e) => MiddleName.Focus();
            MiddleName.Completed += (sender, e) => LastName.Focus();
            LastName.Completed += (s, e) => Gender.Focus();
            Mobile.Completed += (s, e) => Email.Focus();
            Email.Completed += (s, e) => Password.Focus();
            Password.Completed += (s, e) => RetypePassword.Focus();
            RetypePassword.Completed += (s,e) => SubmitForm(s, e);
        }

        private void ResetView(object sender, FocusEventArgs e)
        {
            (sender as Entry).PlaceholderColor = Color.Default;
        }

        async void SubmitForm(object sender, EventArgs e)
        {
            SubmitButton.IsEnabled = false;
            InfoMessage.IsVisible = false;
            int SelectedIndex = 2;
            if (Gender.SelectedIndex >= 0)
            {
                SelectedIndex = Gender.SelectedIndex;
            }


            if (CheckIfThereIsNull())
            {
                await DisplayAlert("Missing fields", "Some required fields are empty", "Ok");
            }
            else if(Password.Text != RetypePassword.Text)
            {
                Password.TextColor = Color.Red;
                RetypePassword.TextColor = Color.Red;
                await DisplayAlert("Error", "Password does not match", "Ok");
            }
            else if(!Regex.Match(Email.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
            {
                await DisplayAlert("Error", "Invalid email", "Ok");
                Email.TextColor = Color.Red;
            }
            else
            {

                Customer customer = new Customer
                {
                    FirstName = FirstName.Text,
                    MiddleName = MiddleName.Text,
                    LastName = LastName.Text,
                    EmailAddress = Email.Text,
                    BirthDate = null,
                    Gender = Gender.Items[SelectedIndex],
                    Password = Bcr.BCrypt.HashPassword(Password.Text),
                    ContactNumber = Mobile.Text
                };

                var response = await Customer.SignUpAsync(customer);

                if (response == 200)
                {
                    await DisplayAlert("Message", "Registration successful", "Ok");
                    await Navigation.PopAsync();
                }
                else
                {
                    var message = (response == 400) ? "This email is already registered" : "Something went wrong with your sign up";
                    await DisplayAlert("Error", message, "Try again");
                }
            }
            SubmitButton.IsEnabled = true;
        }

        private async void OpenTnC(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new TermsAndConditions(), false);
        }

        private async void OpenPrivacyPolicy(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new PrivacyPolicy(), false);
        }

        bool CheckIfThereIsNull()
        {
            foreach(var control in controls)
            {
                if (string.IsNullOrWhiteSpace(control.Text)) { 
                    control.PlaceholderColor = Color.Red;
                    return true;
                }
            }
            return false;
        }


	}
}