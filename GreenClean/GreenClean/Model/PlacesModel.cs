using System;
using System.Collections.Generic;
using System.Text;

namespace GreenClean.Model
{
    public class PlacesModel
    {
        public int PlaceId { get; set; }
        public string PlaceName { get; set; }
        public string PlaceDetail { get; set; }

        public PlacesModel(int id, string name, string detail)
        {
            PlaceId = id;
            PlaceName = name;
            PlaceDetail = detail;
        }
    }
}
