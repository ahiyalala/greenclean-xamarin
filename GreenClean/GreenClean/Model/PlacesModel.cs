using System;
using System.Collections.Generic;
using System.Text;

namespace GreenClean.Model
{
    public class PlacesModel
    {
        public int PlaceId { get; set; }
        public string PlaceName { get; set; }
        public string StreetName { get; set; }
        public string Barangay { get; set; }
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
    }
}
