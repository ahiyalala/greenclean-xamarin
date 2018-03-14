using GreenClean.Model;
using GreenClean.Utilities;
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
	public partial class PaymentForm : ContentPage
	{
        public event EventHandler<FormEvent> DataSender;

        PaymentModel payment;

        public PaymentForm()
        {
            InitializeComponent();
            CardNumberText.IsVisible = false;
            SubmitButton.Text = "Add";
        }

		public PaymentForm (PaymentModel obj)
		{
			InitializeComponent ();
            CardNumber.IsVisible = false;
            CardNumberText.Text = obj.PaymentDetail;
            ExpiryDate.Text = obj.CardExpiry;
            SubmitButton.Text = "Update";
            payment = obj;
		}

        public void OnExecute(object sender, EventArgs args)
        {

            if(payment != null)
            {
                payment.CardExpiry = ExpiryDate.Text;
                payment.CardCvv = CVV.Text;

            }
            else
            {
                var cardexp = ExpiryDate.Text;
                var cvv = CVV.Text;
                var cardnumber = CardNumber.Text;
                payment = new PaymentModel(String.Empty, cardnumber, cardexp, cvv);
            }

            DataSender(sender, new FormEvent() { Object = payment });
        }
        
	}
}