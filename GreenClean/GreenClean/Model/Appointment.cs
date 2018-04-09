using GreenClean.Utilities;
using GreenClean.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GreenClean.Model
{
    public class Appointment
    {
        [JsonProperty("booking_request_id")]
        public string BookingRequestId { get; set; }
        [JsonProperty("service")]
        public Services Service { get; set; }
        [JsonProperty("location")]
        public PlacesModel Places { get; set; }
        [JsonProperty("housekeeper")]
        public Housekeeper Housekeeper { get; set; }
        [JsonProperty("date")]
        public DateTime ScheduleDate { get; set; }
        [JsonProperty("start_time")]
        public DateTime ScheduleTimeStart { get; set; }
        [JsonProperty("end_time")]
        public DateTime ScheduleTimeEnd { get; set; }
        [JsonProperty("payment_type")]
        public string PaymentType { get; set; }
        [JsonProperty("is_paid")]
        public bool IsPaid { get; set; }
        [JsonProperty("is_finished")]
        public bool IsFinished { get; set; }

        static string appointmentUri = Constants.BaseUri + "/api/appointments";

        public static async Task<Appointment> BookAsync(AppointmentRequest appointmentRequest)
        {
            HttpClient client = new HttpClient();
            var properties = Application.Current.Properties;
            client.DefaultRequestHeaders.Add("Authentication", string.Format("{0} {1}", properties["email"], properties["token"]));
            var requestJson = JsonConvert.SerializeObject(appointmentRequest);
            var content = new StringContent(requestJson, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(appointmentUri, content);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var appointmentResult = JsonConvert.DeserializeObject<Appointment>(result);
                DashboardViewModel.All.Insert(0,new DashboardViewModel(appointmentResult));
                return appointmentResult;
            }
            return null;
        }
    }
}
