using System;
using System.Collections.Generic;
using System.Text;

namespace GreenClean.Model
{
    public class AppointmentRequest
    {
        public Services Service { get; set; }
        public PlacesModel Place { get; set; }
        public Customer Customer { get; set; }
        public PaymentModel Payment { get; set; }
        
        public string Date { get; set; }
        public string Time { get; set; }
    }
}
