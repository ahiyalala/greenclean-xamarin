using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GreenClean
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            
		}
        
        async void OnLogin(object sender, EventArgs e)
        {
            string username = this.FindByName<Entry>("Username").Text;
            string password = this.FindByName<Entry>("Password").Text;

            if ( username == "ahiyalala" && password == "thisisatest" )
            {
                await Navigation.PushAsync(new Dashboard());
            }

            this.FindByName<Label>("Message").Text = "Wrong credentials!";
        }
	}
}
