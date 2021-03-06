﻿using GreenClean.Model;
using GreenClean.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenClean
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Profile : ContentPage
	{
		public Profile ()
		{
			InitializeComponent ();

		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ProfileViewModel.GetProfile();
            BindingContext = new ProfileViewModel()
            {
                Navigation = Navigation
            };
            Email.Text = Customer.Current.EmailAddress;
            Form.IsVisible = true;
            Indicator.IsVisible = false;
        }
    }
}