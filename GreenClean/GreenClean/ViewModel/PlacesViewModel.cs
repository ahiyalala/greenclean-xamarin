﻿using GreenClean.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreenClean.ViewModel
{
    public class PlacesViewModel : ViewBaseModel
    {
        #region fields
        PlacesModel places;
        #endregion

        public PlacesModel Places {
            get
            {
                return places;
            }
            set
            {
                places = value;
                OnPropertyChanged("Places");
            }
        }

        public PlacesViewModel(int id, string name, string street, string barangay, string city)
        {
            Places = new PlacesModel(id, name, street, barangay, city);
        }

        public PlacesViewModel(PlacesModel model)
        {
            Places = model;
        }
    }
}
