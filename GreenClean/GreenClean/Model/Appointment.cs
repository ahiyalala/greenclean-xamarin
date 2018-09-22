using GreenClean.Utilities;
using GreenClean.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GreenClean.Model
{
    public class Appointment
    {
        [JsonProperty("service_cleaning_id")]
        public string BookingRequestId { get; set; }
        [JsonProperty("service")]
        public Services Service { get; set; }
        [JsonProperty("location")]
        public PlacesModel Places { get; set; }
        [JsonProperty("housekeepers")]
        public List<Housekeeper> Housekeeper { get; set; }
        [JsonProperty("date")]
        public DateTime ScheduleDate { get; set; }
        [JsonProperty("start_time")]
        public DateTime ScheduleTimeStart { get; set; }
        [JsonProperty("end_time")]
        public DateTime ScheduleTimeEnd { get; set; }
        [JsonProperty("payment_type")]
        public string PaymentType { get; set; }
        [JsonProperty("total_price")]
        public int Price { get; set; }
        [JsonProperty("is_paid")]
        public bool IsPaid { get; set; }
        [JsonProperty("is_finished")]
        public bool IsFinished { get; set; }
        [JsonProperty("rating")]
        public float Rating { get; set; }
        [JsonProperty("comment")]
        public string Comment { get; set; }

        static string appointmentUri = Constants.BaseUri + "/api/appointments";

        public static async Task<Appointment> BookAsync(AppointmentRequest appointmentRequest)
        {
            try
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
                    AppointmentDashboardViewmodel.Pending.Add(new AppointmentDashboardViewmodel(appointmentResult));
                    return appointmentResult;
                }
                return null;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public static async Task GetAppointmentsAsync()
        {
            HttpClient client = new HttpClient();
            var properties = Application.Current.Properties;
            client.DefaultRequestHeaders.Add("Authentication", string.Format("{0} {1}", properties["email"], properties["token"]));

            AppointmentDashboardViewmodel.Pending = null;
            AppointmentDashboardViewmodel.Finished = null;

            var response = await client.GetAsync(appointmentUri);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                AppointmentDashboardViewmodel.Pending = new ObservableCollection<AppointmentDashboardViewmodel>();
                AppointmentDashboardViewmodel.Finished = new ObservableCollection<AppointmentDashboardViewmodel>();
                var appointmentsList = JsonConvert.DeserializeObject<List<Appointment>>(result);
                foreach(var appointment in appointmentsList.Where(x => x.IsFinished == false || x.Rating == 0))
                {
                    AppointmentDashboardViewmodel.Pending.Add(new AppointmentDashboardViewmodel(appointment));
                }
                foreach(var appointment in appointmentsList.Where(x=>x.IsFinished == true || x.Rating > 0))
                {
                    AppointmentDashboardViewmodel.Finished.Add(new AppointmentDashboardViewmodel(appointment));
                }
            }
        }

        public static async Task<Appointment> GetSpecificAppointmentAsync(string id)
        {
            HttpClient client = new HttpClient();
            var properties = Application.Current.Properties;
            client.DefaultRequestHeaders.Add("Authentication", string.Format("{0} {1}", properties["email"], properties["token"]));

            var response = await client.GetAsync(string.Format("{0}/{1}",appointmentUri,id));
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var appointment = JsonConvert.DeserializeObject<Appointment>(result);

                return appointment;
            }

            return null;
        }
    }
}
