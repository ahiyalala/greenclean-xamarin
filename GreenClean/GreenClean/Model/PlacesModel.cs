using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GreenClean.Model
{
    [JsonObject("location")]
    public class PlacesModel
    {
        const string placesUri = "http://greenclean-cb.southeastasia.cloudapp.azure.com/api/places";

        [JsonProperty("location_id")]
        public int PlaceId { get; set; }
        [JsonProperty("customer_id")]
        public int CustomerId { get; set; }
        [JsonProperty("name")]
        public string PlaceName { get; set; }
        [JsonProperty("street_address")]
        public string StreetName { get; set; }
        [JsonProperty("barangay")]
        public string Barangay { get; set; }
        [JsonProperty("city_address")]
        public string City { get; set; }
        public string PlaceDetail
        {
            get
            {
                return PlaceDetail = String.Format("{0}{1}, {2}", StreetName, " " + Barangay, City);
            }
            protected set
            {

            }
        }

        public PlacesModel(int id, string name, string street, string barangay, string city)
        {
            PlaceId = id;
            PlaceName = name;
            StreetName = street;
            Barangay = barangay;
            City = city;
        }

        public PlacesModel()
        {

        }

        

        public static ObservableCollection<PlacesModel> PlacesList = new ObservableCollection<PlacesModel>();

        public async static Task GetList()
        {
            HttpClient client = new HttpClient();

            var properties = Application.Current.Properties;

            client.DefaultRequestHeaders.Add("Authentication", string.Format("{0} {1}", properties["email"], properties["token"]));

            var request = await client.GetAsync(placesUri).ConfigureAwait(false);
            var content = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (request.IsSuccessStatusCode)
            {
                var model = JsonConvert.DeserializeObject<IList<PlacesModel>>(content);

                PlacesList = new ObservableCollection<PlacesModel>(model);
            }
        }

        public async static Task AddToList(PlacesModel places)
        { 
            HttpClient client = new HttpClient();
            var properties = Application.Current.Properties;
            client.DefaultRequestHeaders.Add("Authentication", string.Format("{0} {1}", properties["email"], properties["token"]));

            places.CustomerId = Customer.Current.CustomerId;

            var request = await client.PostAsync(placesUri,places).ConfigureAwait(false);
            var content = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

    }
}
