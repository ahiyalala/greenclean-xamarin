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
            BirthDate.SetValue(DatePicker.MaximumDateProperty, DateTime.Now);
            controls = new List<Entry>
            {
                FirstName,LastName,Email,Password,Mobile
            };
        }

        async void SubmitForm(object sender, EventArgs e)
        {
            SubmitButton.IsEnabled = false;
            InfoMessage.IsVisible = false;
            Customer customer = new Customer
            {
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                BirthDate = BirthDate.Date.ToString("yyyy-MM-dd"),
                EmailAddress = Email.Text,
                Password = Bcr.BCrypt.HashPassword(Password.Text),
                ContactNumber = Mobile.Text
            };


            if (CheckIfThereIsNull())
            {
                InfoMessage.Text = "Field is required";
                InfoMessage.IsVisible = true;
            }
            else if(Password.Text != RetypePassword.Text)
            {
                Password.TextColor = Color.Red;
                RetypePassword.TextColor = Color.Red;
                InfoMessage.Text = "Password does not match";
                InfoMessage.IsVisible = true;
            }
            else if(!Regex.Match(Email.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
            {
                Email.TextColor = Color.Red;
                InfoMessage.Text = "Invalid email";
                InfoMessage.IsVisible = true;
            }
            else
            {
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
            SubmitButton.IsEnabled = true;
        }

        bool CheckIfThereIsNull()
        {
            foreach(var control in controls)
            {
                if (control.Text.Equals(string.Empty)) { 
                    control.BackgroundColor = Color.Red;
                    return true;
                }
            }
            return false;
        }


	}
}