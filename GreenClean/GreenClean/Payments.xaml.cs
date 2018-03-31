using GreenClean.Model;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenClean
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Payments : ContentPage
	{

        public event EventHandler<SelectedItemChangedEventArgs> DataSender;
        
		public Payments ()
		{
			InitializeComponent ();
            Options.ItemsSource = PaymentModel.PaymentList;
        }
        
        public void OnItemSelect(object sender, SelectedItemChangedEventArgs args)
        {
            (sender as ListView).SelectedItem = null;

            if (args.SelectedItem != null)
            {
                DataSender(sender, args);
            }
        }
    }
}