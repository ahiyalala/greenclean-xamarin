﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreenClean.Model
{
    [JsonObject("services")]
    public class Services
    {

        public int Id { get; set; }
        [JsonProperty("service_type_key")]
        public string ServiceName { get; set; }
        [JsonProperty("service_description")]
        public string Description { get; set; }
        [JsonProperty("service_price")]
        public string Price { get; set; }
    }
}
