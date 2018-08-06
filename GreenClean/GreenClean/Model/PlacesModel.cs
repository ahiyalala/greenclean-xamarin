using GreenClean.Utilities;
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
        static string placesUri = Constants.BaseUri + "/api/places";

        [JsonProperty("location_id")]
        public int PlaceId { get; set; }
        [JsonProperty("customer_id")]
        public int CustomerId { get; set; }
        [JsonProperty("location_type")]
        public string PlaceName { get; set; }
        [JsonProperty("location_street")]
        public string StreetName { get; set; }
        [JsonProperty("location_barangay")]
        public string Barangay { get; set; }
        [JsonProperty("location_city")]
        public string City { get; set; }
        [JsonIgnore]
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

        public PlacesModel(string name, string street, string barangay, string city)
        {
            PlaceName = name;
            StreetName = street;
            Barangay = barangay;
            City = city;
        }

        public PlacesModel()
        {

        }

        

        public static List<PlacesModel> PlacesList = new List<PlacesModel>();

        public async static Task GetList()
        {
            HttpClient client = new HttpClient();

            var properties = Application.Current.Properties;

            client.DefaultRequestHeaders.Add("Authentication", string.Format("{0} {1}", properties["email"], properties["token"]));

            var request = await client.GetAsync(placesUri).ConfigureAwait(false);
            var content = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (request.IsSuccessStatusCode && PlacesList.Count == 0)
            {
                var model = JsonConvert.DeserializeObject<List<PlacesModel>>(content);

                PlacesList = model;
            }
        }

        public async static Task<bool> AddToList(PlacesModel places)
        { 
            HttpClient client = new HttpClient();
            var properties = Application.Current.Properties;
            client.DefaultRequestHeaders.Add("Authentication", string.Format("{0} {1}", properties["email"], properties["token"]));

            places.CustomerId = Customer.Current.CustomerId;
            var placeJson = JsonConvert.SerializeObject(places);
            var postRequest = new StringContent(placeJson, Encoding.UTF8, "application/json");

            var request = await client.PostAsync(placesUri,postRequest);
            
            var content = await request.Content.ReadAsStringAsync();
            if (request.IsSuccessStatusCode)
            {
                var place = JsonConvert.DeserializeObject<PlacesModel>(content);
                PlacesList.Add(place);

                return true;
            }

            return false;
        }

    }
}
