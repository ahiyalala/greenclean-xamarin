﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GreenClean.ViewModel
{
    class ViewBaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}