using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GreenClean.Model
{
    public class AppointmentRequest
    {
        [JsonProperty("service")]
        public Services Service { get; set; }
        [JsonProperty("location")]
        public PlacesModel Place { get; set; }
        [JsonProperty("customer")]
        public Customer Customer { get; set; }
        [JsonProperty("payment")]
        public PaymentModel Payment { get; set; }
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("start_time")]
        public string Time { get; set; }
    }
}
