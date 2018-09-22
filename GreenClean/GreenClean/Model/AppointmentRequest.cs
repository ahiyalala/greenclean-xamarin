using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GreenClean.Model
{
    public class AppointmentRequest
    {
        [JsonIgnore]
        public Services Service { get; set; }

        [JsonProperty("service_type_key")]
        public string ServiceTypeKey => GetServiceKey();

        [JsonIgnore]
        public PlacesModel Place { get; set; }

        [JsonProperty("location_id")]
        public int PlaceId => Place.PlaceId;

        [JsonIgnore]
        public Customer Customer { get; set; }

        [JsonProperty("customer_id")]
        public int CustomerId => Customer.CustomerId;

        [JsonIgnore]
        public PaymentModel Payment { get; set; }

        [JsonProperty("payment_type")]
        public string PaymentType => GetType();

        [JsonProperty("duration")]
        public string Duration => Service.Duration;

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("start_time")]
        public string Time { get; set; }

        [JsonProperty("number_of_housekeepers")]
        public int Housekeepers { get; set; }
        [JsonProperty("location_area")]
        public int Area { get; set; }

        private string GetServiceKey()
        {
            return Service.ServiceName;
        }

        private new string GetType()
        {
            if(Payment.PaymentDetail == "Cash")
            {
                return "Cash";
            }
            else
            {
                return Payment.CardInfo.PaymentId;
            }
        }
    }
}
