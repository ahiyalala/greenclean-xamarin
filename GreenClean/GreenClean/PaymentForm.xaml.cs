using GreenClean.DependencyServices;
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
            ExpiryMonth.Text = obj.CardInfo.ExpiryMonth.ToString();
            ExpiryYear.Text = obj.CardInfo.ExpiryYear.ToString();
            SubmitButton.Text = "Update";
            payment = obj;
		}

        public async void OnExecute(object sender, EventArgs args)
        {

            if(payment != null)
            {
                payment.CardInfo.ExpiryMonth = ReturnNumeric(ExpiryMonth);
                payment.CardInfo.ExpiryYear = ReturnNumeric(ExpiryYear);
                payment.CardInfo.Cvc = ReturnNumeric(CVV);
                await payment.UpdateAsync();
            }
            else
            {
                var card = new Card
                {
                    Number = CardNumber.Text,
                    ExpiryMonth = ReturnNumeric(ExpiryMonth),
                    ExpiryYear = ReturnNumeric(ExpiryYear),
                    Cvc = ReturnNumeric(CVV)
                };
                payment = await PaymentModel.AddAsync(card).ConfigureAwait(false);
            }

            if (payment.isRequestSuccessful)
            {
                await DisplayAlert("Invalid card", "Please verify your details", "Try again");
            }
            else
            {
                DependencyService.Get<IMessage>().ShortAlert("Successful!");
                DataSender(sender, new FormEvent() { Object = payment });
            }
        }

        public int ReturnNumeric(Entry entry)
        {
            if(int.TryParse(entry.Text, out int result))
            {
                return int.Parse(entry.Text);
            }
            else
            {
                return 0;
            }
        }
        
	}
}