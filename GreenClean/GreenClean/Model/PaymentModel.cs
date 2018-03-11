using System;
using System.Collections.Generic;
using System.Text;

namespace GreenClean.Model
{
    public class PaymentModel
    {
        public int PaymentId { get; set; }
        public string PaymentName { get; set; }
        public string CardDigit { get; set; }
        public string CardExpiry { get; set; }
        public string CardCvv { get; set; }
        public string PaymentDetail { get; set; }

        public PaymentModel(string name, string card, string expiry, string cvv)
        {
            CardDigit = card;
            CardExpiry = expiry;
            CardCvv = cvv;
            PaymentDetail = String.Format("****-****-****-{0}", card.Substring(12));
        }

        public PaymentModel()
        {
            PaymentName = String.Empty;
            PaymentDetail = "Cash";
        }
    }
}
