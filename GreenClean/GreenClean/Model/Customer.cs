using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreenClean.Model
{
    public class Customer
    {
        [JsonProperty("firstname")]
        public string FirstName { get; set; }

        [JsonProperty("lastname")]
        public string LastName { get; set; }

        [JsonProperty("birthdate")]
        public string BirthDate { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("email")]
        public string EmailAddress { get; set; }

        [JsonProperty("mobile")]
        public string ContactNumber { get; set; }
        
    }
}
