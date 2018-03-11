using GreenClean.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreenClean.ViewModel
{
    public class PaymentViewModel : ViewBaseModel
    {
        #region fields
        PaymentModel payment;
        #endregion

        public PaymentModel Payment
        {
            get
            {
                return payment;
            }
            set
            {
                payment = value;
                OnPropertyChanged("Places");
            }
        }

        public bool IsEnabled;

        public PaymentViewModel(PaymentModel obj,bool enabled = true){
            Payment = obj;
            IsEnabled = enabled;
        }
    }
}
