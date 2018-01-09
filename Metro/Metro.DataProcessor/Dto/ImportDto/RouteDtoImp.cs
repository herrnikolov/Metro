using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;

namespace Metro.DataProcessor.Dto.ImportDto
{
    public class rootDictionary
    {
        public Dictionary<string, SecondLevelList> root { get; set; }
    }

    public class SecondLevelList
    {
        public List<RouteDtoImp> data { get; set; }
    }

    public class RouteDtoImp
    {
        [Key]
        public int Id { get; set; }

        [JsonProperty("routes_id")]
        public int RouteId { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("route_name")]
        public string RouteName { get; set; }

        [JsonProperty("line_id")]
        public int LineId { get; set; }

        [JsonProperty("id")]
        public int ExtId { get; set; }

        [JsonProperty("line_name")]
        public int LineName { get; set; }
    }
}
