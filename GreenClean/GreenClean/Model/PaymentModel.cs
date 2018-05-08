using GreenClean.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GreenClean.Model
{
    [JsonObject("payment")]
    public class PaymentModel
    {

        static string paymentUri = Constants.BaseUri + "/api/payments";

        [JsonIgnore]
        public int PaymentId { get; set; }
        [JsonIgnore]
        public Card CardInfo { get; set; }
        [JsonIgnore]
        public bool isRequestSuccessful { get; set; }
        [JsonProperty("payment_type")]
        public string PaymentDetail {
            get
            {
                if (CardInfo != null)
                {
                    return String.Format("****-****-****-{0}", CardInfo.MaskedPan);
                }

                return "Cash";
            }
            protected set {
            } }

        public static ObservableCollection<PaymentModel> PaymentList = new ObservableCollection<PaymentModel>();

        public PaymentModel(Card card)
        {
            CardInfo = card;
        }

        public PaymentModel()
        {
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
                var model = JsonConvert.DeserializeObject<IList<Card>>(content);
                PaymentList = new ObservableCollection<PaymentModel>();
                PaymentList.Add(GetFirstPaymentData());
                foreach (var x in model)
                {
                    PaymentList.Add(new PaymentModel(x));
                }
                
            }
        }

        public async static Task<PaymentModel> AddAsync(Card card)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(
                ASCIIEncoding.ASCII.GetBytes(
                    string.Format("{0}:", "pk-6y2WX6WhWxfQOg8ezKIUuiJxa7gC4sDvOipn9NFXlwz"))));
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var cardJson = JsonConvert.SerializeObject(card);
                var postRequest = new StringContent(cardJson, Encoding.UTF8, "application/json");
                var request = await client.PostAsync(new Uri("https://pg-sandbox.paymaya.com/payments/v1/payment-tokens"), postRequest).ConfigureAwait(false);
                var content = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (request.IsSuccessStatusCode)
                {
                    var properties = Application.Current.Properties;
                    client.DefaultRequestHeaders.Remove("Authorization");
                    client.DefaultRequestHeaders.Add("Authentication", string.Format("{0} {1}", properties["email"], properties["token"]));
                    var gcPostRequest = new StringContent(content, Encoding.UTF8, "application/json");
                    var gcRequest = await client.PostAsync(paymentUri + "/register_card_to_customer", gcPostRequest);
                    var gcContent = await gcRequest.Content.ReadAsStringAsync();
                    if (gcRequest.IsSuccessStatusCode)
                    {
                        var cardResponse = JsonConvert.DeserializeObject<Card>(gcContent);
                        return new PaymentModel
                        {
                            CardInfo = cardResponse,
                            isRequestSuccessful = true,
                        };
                    }
                }
                return new PaymentModel
                {
                    isRequestSuccessful = false

                };
            }
            catch(Exception e)
            {
                Console.Write(e);
                return new PaymentModel
                {
                    isRequestSuccessful = false,
                };
            }
        }

        public async Task UpdateAsync()
        {
            await Task.Delay(0);
            isRequestSuccessful = false;
        }

    }
}
