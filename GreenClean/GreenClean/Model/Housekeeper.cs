using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreenClean.Model
{
    [JsonObject("housekeeper")]
    public class Housekeeper
    {
        [JsonProperty("housekeeper_id")]
        public string HousekeeperId { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("middle_name")]
        public string MiddleName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonIgnore]
        public string FullName => GetFullName();
        [JsonProperty("birth_date")]
        public DateTime BirthDate { get; set; }
        [JsonProperty("email_address")]
        public string EmailAddress { get; set; }
        [JsonProperty("contact_number")]
        public string ContactNumber { get; set; }
        [JsonProperty("gender")]
        public string Gender { get; set; }
        [JsonProperty("rating")]
        public string Rating { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonIgnore]
        public string RatingText => GetRatingText();

        private string GetRatingText()
        {
            int _rating = 0;
            if(int.TryParse(Rating, out _rating))
            {
                return (_rating == 0) ? "N/A" : _rating.ToString();
            }

            return "N/A";
        }
        
        private string GetFullName()
        {
            return string.Format("{0} {1} {2}", FirstName, MiddleName, LastName);
        }
    }
}
