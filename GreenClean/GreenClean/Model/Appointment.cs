using System;
using System.Collections.Generic;
using System.Text;

namespace GreenClean.Model
{
    public class Appointment
    {
        public int BookingId { get; set; }
        public int ServiceTypeId { get; set; }

        public int HousekeeperId { get; set; }
        public string Housekeeper { get; set; }

        public string Place { get; set; }
        public DateTime Schedule { get; set; }

        public int PaymentTypeId { get; set; }

        public bool IsPaid { get; set; }
        public bool IsFinished { get; set; }


    }
}
