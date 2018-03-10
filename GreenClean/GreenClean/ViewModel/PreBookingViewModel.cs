using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GreenClean.ViewModel
{
    class PreBookingViewModel :ViewBaseModel
    {

        #region fields
        string optionValue;
        #endregion

        public PreBookingViewModel(string optionname, string optionvalue, int optiontype, bool isenabled = true)
        {
            OptionName = optionname;
            OptionValue = optionvalue;
            OptionType = optiontype;
            IsEnabled = isenabled;
        }

        public string OptionName { get; set; }
        public string OptionValue {
            get
            {
                return optionValue;
            }
            set
            {
                optionValue = value;
                OnPropertyChanged("OptionValue");
            }
        }
        public int OptionType { get; set; }
        public bool IsEnabled { get; set; }


        
    }
}
