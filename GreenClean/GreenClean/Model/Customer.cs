using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GreenClean.Model
{
    [JsonObject("customer")]
    public class Customer
    {


        [JsonProperty("customer_id")]
        public int CustomerId { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("birth_date")]
        public string BirthDate { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("email_address")]
        public string EmailAddress { get; set; }

        [JsonProperty("contact_number")]
        public string ContactNumber { get; set; }

        [JsonProperty("user_token")]
        public string UserToken { get; set; }

        public static Customer Current { get; set; }

        const string UriUsers = "http://greenclean-cb.southeastasia.cloudapp.azure.com/api/users/login";

        public async static Task GetProfile()
        {
            HttpClient client = new HttpClient();
            var properties = Application.Current.Properties;
            client.DefaultRequestHeaders.Add("Authentication", string.Format("{0} {1}", properties["email"], properties["token"]));

            var request = await client.GetAsync(UriUsers).ConfigureAwait(false) ;
            var content = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (request.IsSuccessStatusCode)
            {
                Current = JsonConvert.DeserializeObject<Customer>(content);
            }
        }

    }
}
