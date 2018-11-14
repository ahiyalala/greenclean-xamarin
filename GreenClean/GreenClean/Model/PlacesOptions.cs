using GreenClean.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GreenClean.Model
{
    public class PlacesOptions
    {
        [JsonProperty("city_barangay")]
        public string Barangay { get; set; }

        [JsonProperty("city_name")]
        public string City { get; set; }

        public static async Task<List<PlacesOptions>> GetList()
        {
            HttpClient client = new HttpClient();

            var properties = Application.Current.Properties;
            string placesUri = Constants.BaseUri + "/api/places/locations-list";

            client.DefaultRequestHeaders.Add("Authentication", string.Format("{0} {1}", properties["email"], properties["token"]));

            var request = await client.GetAsync(placesUri).ConfigureAwait(false);
            
            if (request.IsSuccessStatusCode)
            {
                var content = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
                var model = JsonConvert.DeserializeObject<List<PlacesOptions>>(content);

                return model;
            }

            return null;
        }
    }
}
