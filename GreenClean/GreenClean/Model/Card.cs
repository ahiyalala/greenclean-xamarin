using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreenClean.Model
{
    [JsonObject("card")]
    public class Card
    {
        [JsonProperty("payment_id")]
        public string PaymentId { get; set; }
        [JsonProperty("number")]
        public string Number { get; set; }
        [JsonProperty("expMonth")]
        public int ExpiryMonth { get; set; }
        [JsonProperty("expYear")]
        public int ExpiryYear { get; set; }
        [JsonProperty("cvc")]
        public int Cvc { get; set; }
        [JsonProperty("masked_number")]
        public int MaskedPan { get; set; }
        [JsonProperty("verification_url")]
        public string Url { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }


    }
}
