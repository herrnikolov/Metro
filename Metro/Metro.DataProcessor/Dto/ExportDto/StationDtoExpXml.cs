using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Metro.DataProcessor.Dto.ExportDtoXml
{
    [XmlType("Stantion")]
    public class StationDtoExpXml
    {
        [XmlElement("id")]
        public int id { get; set; }

        [XmlElement("route_id")]
        public int route_id { get; set; }

        [XmlElement("code")]
        public int code { get; set; }

        [XmlElement("point_id")]
        public int point_id { get; set; }

        [XmlElement("name")]
        public string name { get; set; }

        [XmlElement("latitude")]
        public decimal latitude { get; set; }

        [XmlElement("longitude")]
        public decimal longitude { get; set; }
    }
}
