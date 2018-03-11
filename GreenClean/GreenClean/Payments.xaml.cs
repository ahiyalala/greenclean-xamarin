using GreenClean.Model;
using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenClean
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Payments : ContentPage
	{

        public event EventHandler<SelectedItemChangedEventArgs> DataSender;

        ObservableCollection<PaymentModel> payments = new ObservableCollection<PaymentModel>();

		public Payments ()
		{
			InitializeComponent ();
            ListOptions();
            Options.ItemsSource = payments;
		}

        private void ListOptions()
        {
            payments.Add(new PaymentModel());
            payments.Add(new PaymentModel("Personal", "1234-1234-1234-1234","11/11","644"));
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