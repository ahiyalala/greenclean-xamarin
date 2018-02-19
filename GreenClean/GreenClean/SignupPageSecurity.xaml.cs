using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenClean
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignupPageSecurity : ContentPage
	{
        HttpClient client;
        const string Uri = "http://greenclean-cb.southeastasia.cloudapp.azure.com/";
		public SignupPageSecurity ()
		{
            client = new HttpClient();
            InitializeComponent ();
        }

        async void SubmitForm(object sender, EventArgs e)
        {
            await client.PostAsync(Uri, new StringContent("stuff"));
        }
	}
}