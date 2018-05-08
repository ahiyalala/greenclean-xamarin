using GreenClean.ViewModel;
using System.Collections.ObjectModel;
using GreenClean.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GreenClean.Utilities;
using System;
using System.Threading.Tasks;

namespace GreenClean
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PaymentPage : ContentPage
	{
        ObservableCollection<PaymentViewModel> payments = new ObservableCollection<PaymentViewModel>();
        PaymentViewModel paymentViewModel;
        PaymentForm paymentForm;
        public static bool hasAppeared;
		public PaymentPage ()
		{
			InitializeComponent ();
            Task.Run(async () => await PaymentModel.GetList());
            PaymentList.ItemsSource = payments;
            hasAppeared = false;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (!hasAppeared)
            {
                ListPayments();
                hasAppeared = true;
            }
            

        }

        public async void OnItemSelect(object sender, SelectedItemChangedEventArgs args)
        {
            (sender as ListView).SelectedItem = null;

            if (args.SelectedItem != null)
            {
                paymentViewModel = args.SelectedItem as PaymentViewModel;
                paymentForm = new PaymentForm(paymentViewModel.Payment);
                paymentForm.DataSender += UpdateList;
                await Navigation.PushAsync(paymentForm);

            }
        }

        public void ListPayments()
        {
            foreach(var x in PaymentModel.PaymentList)
            {
                bool flag = (x.PaymentDetail != "Cash") ? true : false;
                payments.Add(new PaymentViewModel(x,flag));
            }
        }

        public async void AddList(object sender, FormEvent args)
        {
            var obj = args.Object as PaymentModel;
            payments.Add(new PaymentViewModel(obj));
            await Navigation.PopAsync();
        }

        public async void UpdateList(object sender, FormEvent args)
        {
            paymentViewModel.Payment = args.Object as PaymentModel;
            await Navigation.PopAsync();
        }

        public async void AddNewPayment(object sender, EventArgs a)
        {
            paymentForm = new PaymentForm();
            paymentForm.DataSender += AddList;
            await Navigation.PushAsync(paymentForm);
        }
    }
}