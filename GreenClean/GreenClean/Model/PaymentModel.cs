using GreenClean.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GreenClean.Model
{
    [JsonObject("payment")]
    public class PaymentModel : INotifyPropertyChanged
    {

        static string paymentUri = Constants.BaseUri + "/api/payments";
        static string paymayaUri = "https://pg-sandbox.paymaya.com/payments/v1/payment-tokens";
        [JsonIgnore]
        public string PaymentId { get; set; }
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
            PaymentId = CardInfo.PaymentId;
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
            var properties = Application.Current.Properties;
            client.DefaultRequestHeaders.Add("Authentication", string.Format("{0} {1}", properties["email"], properties["token"]));
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
                var properties = Application.Current.Properties;
                //client.DefaultRequestHeaders.Add("Authentication", string.Format("{0} {1}", properties["email"], properties["token"]));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:", "pk-6y2WX6WhWxfQOg8ezKIUuiJxa7gC4sDvOipn9NFXlwz"))));
                var cardJson = JsonConvert.SerializeObject(new {
                    card = new {
                        number = card.Number,
                        expMonth = card.ExpiryMonth.ToString("D2"),
                        expYear = card.ExpiryYear.ToString(),
                        cvc = card.Cvc.ToString()
                }
                });
                var postRequest = new StringContent(cardJson, Encoding.UTF8, "application/json");
                var request = await client.PostAsync(paymayaUri, postRequest);
                var content = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (request.IsSuccessStatusCode)
                {
                    var cardResponse = JsonConvert.DeserializeObject<Card>(content);
                    return new PaymentModel
                    {
                        CardInfo = cardResponse,
                        isRequestSuccessful = true,
                    };
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
