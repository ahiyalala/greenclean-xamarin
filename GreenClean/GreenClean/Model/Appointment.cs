using System;
using System.Collections.Generic;
using System.Text;

namespace GreenClean.Model
{
    public class Appointment
    {

        public Customer Customer { get; set; }
        public Services Service { get; set; }
        public PlacesModel Places { get; set; }
        public Housekeeper Housekeeper { get; set; }

        public string ScheduleDate { get; set; }
        public string ScheduleTimeStart { get; set; }
        public string ScheduleTimeEnd { get; set; }

        public string PaymentType { get; set; }

        public bool IsPaid { get; set; }
        public bool IsFinished { get; set; }


    }
}
