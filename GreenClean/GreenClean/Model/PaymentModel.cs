using GreenClean.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GreenClean.Model
{
    [JsonObject("payment")]
    public class PaymentModel
    {

        static string paymentUri = Constants.BaseUri + "/api/payments";

        [JsonIgnore]
        public int PaymentId { get; set; }

        [JsonIgnore]
        public string PaymentName { get; set; }
        
        [JsonIgnore]
        public string CardDigit { get; set; }
        [JsonIgnore]
        public string CardExpiry { get; set; }
        [JsonIgnore]
        public string CardCvv { get; set; }
        [JsonProperty("payment_type")]
        public string PaymentDetail {
            get
            {
                if(CardDigit != null)
                {
                    return String.Format("****-****-****-{0}", CardDigit.Substring(12));
                }

                return "Cash";
            }
            protected set {
            } }

        public static ObservableCollection<PaymentModel> PaymentList = new ObservableCollection<PaymentModel>();

        public PaymentModel(string name, string card, string expiry, string cvv)
        {
            CardDigit = card;
            CardExpiry = expiry;
            CardCvv = cvv;
            PaymentDetail = String.Format("****-****-****-{0}", card.Substring(12));
        }

        public PaymentModel()
        {
            PaymentName = String.Empty;
            PaymentDetail = "Cash";
        }

        public static PaymentModel GetFirstPaymentData()
        {
            return new PaymentModel();
        }

        public async static Task GetList()
        {
            HttpClient client = new HttpClient();

            var request = await client.GetAsync(paymentUri).ConfigureAwait(false);

            if (request.IsSuccessStatusCode)
            {
                var content = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
                var model = JsonConvert.DeserializeObject<IList<PaymentModel>>(content);
                PaymentList = new ObservableCollection<PaymentModel>();
                PaymentList.Add(GetFirstPaymentData());
                foreach (var x in model)
                {
                    PaymentList.Add(x);
                }
                
            }
        }
    }
}
