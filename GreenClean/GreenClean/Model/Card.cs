using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreenClean.Model
{
    [JsonObject("card")]
    public class Card
    {
        [JsonProperty("payment_id"),JsonIgnore]
        public string PaymentId { get; set; }
        [JsonProperty("number")]
        public string Number { get; set; }
        [JsonProperty("expMonth")]
        public int ExpiryMonth { get; set; }
        [JsonProperty("expYear")]
        public int ExpiryYear { get; set; }
        [JsonProperty("cvc")]
        public int Cvc { get; set; }
        [JsonProperty("maskedPan")]
        [JsonIgnore]
        public int MaskedPan { get; set; }
        [JsonProperty("verification_url")]
        [JsonIgnore]
        public string Url { get; set; }
        [JsonProperty("status")]
        [JsonIgnore]
        public string Status { get; set; }


    }
}
