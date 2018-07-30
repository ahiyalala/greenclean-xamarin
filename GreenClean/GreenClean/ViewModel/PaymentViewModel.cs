using GreenClean.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreenClean.ViewModel
{
    public class PaymentViewModel : ViewBaseModel
    {
        #region fields
        string payment;
        string status;
        #endregion
        
        public PaymentModel Payment { get; set; }

        public string Detail {
            get
            {
                return payment;
            }
            set
            {
                payment = value;
                OnPropertyChanged("PaymentDetail");
            }
        }

        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }

        public bool IsEnabled { get; set; }

        public PaymentViewModel(PaymentModel obj,bool enabled = true){
            Payment = obj;
            Detail = Payment.PaymentDetail;
            if(Payment.CardInfo != null)
            {
                Status = Payment.CardInfo.Status;
            }
            
            IsEnabled = enabled;
        }
    }
}
