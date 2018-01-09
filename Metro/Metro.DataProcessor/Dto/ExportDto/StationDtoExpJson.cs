using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Metro.DataProcessor.Dto.ExportDto
{
    public class StationDtoExpJson
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("route_id")]
        public int route_id { get; set; }

        public int code { get; set; }

        [JsonProperty("point_id")]
        public int point_id { get; set; }

        public string name { get; set; }

        public decimal latitude { get; set; }

        public decimal longitude { get; set; }
    }
}
