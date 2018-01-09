using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

using Metro.Data;
using Metro.DataProcessor.Dto.ExportDto;
using Metro.DataProcessor.Dto.ExportDtoXml;

namespace Metro.DataProcessor
{
    public class Serializer
    {
        //public static string ExportStantionJson(MetroDbContext context)
        //{
        //    //Export JSON
        //    var metroStation = context.Stations
        //        .Select(e => new
        //        {
        //            id = e.StantionId,
        //            route_id = e.RouteId,
        //            code = e.Code,
        //            point_id = e.PointId,
        //            name = e.Name,
        //            latitude = e.Latitude,
        //            longitude = e.Longitude,
        //        }
        //        )
        //        .OrderBy(o => o.id)
        //        .ToArray();

        //    var result = JsonConvert.SerializeObject(metroStation, Formatting.Indented);

        //    return result;

        //    //throw new NotImplementedException();
        //}

        public static string ExportStantionJson(MetroDbContext context)
        {
            // Export JSON with DTO
            // Include AutMapper?
            var metroStation = context.Stations
                .Select(e => new StationDtoExpJson()
                    {
                    id = e.StantionId,
                    route_id = e.RouteId,
                    code = e.Code,
                    point_id = e.PointId,
                    name = e.Name,
                    latitude = e.Latitude,
                    longitude = e.Longitude,
                }
                )
                .OrderBy(o => o.id)
                .ToArray();

            var result = JsonConvert.SerializeObject(metroStation, Formatting.Indented);

            return result;

            //throw new NotImplementedException();
        }


        //public static string ExportStantionXml(MetroDbContext context)
        //{
        //    // Export to XML
        //    var metroStations = context.Stations
        //        .Select(e => new
        //        {
        //            id = e.StantionId,
        //            route_id = e.RouteId,
        //            code = e.Code,
        //            point_id = e.PointId,
        //            name = e.Name,
        //            latitude = e.Latitude,
        //            longitude = e.Longitude,
        //        }
        //        )
        //        .OrderBy(o => o.id)
        //        .ToList();


        //    var xmlDoc = new XDocument(new XElement("Stantions"));

        //    foreach (var s in metroStations)
        //    {

        //        var currentStantion = new XElement("Stantion",
        //                                new XElement("id", s.id),
        //                                new XElement("route_id", s.route_id),
        //                                new XElement("code", s.code),
        //                                new XElement("point_id", s.point_id),
        //                                new XElement("name", s.name),
        //                                new XElement("latitude", s.latitude),
        //                                new XElement("longitude", s.longitude)
        //            );
        //        xmlDoc.Root.Add(currentStantion);
        //    }

        //    var result = xmlDoc.ToString();

        //    return result;

        //    //return xmlDoc.ToString();
        //    //throw new NotImplementedException();
        //}

        public static string ExportStantionXml(MetroDbContext context)
        {
            //Export XML using DTO - WIP!!!

            var metroStations = context.Stations
                .Select(e => new StationDtoExpXml()
                {
                    id = e.StantionId,
                    route_id = e.RouteId,
                    code = e.Code,
                    point_id = e.PointId,
                    name = e.Name,
                    latitude = e.Latitude,
                    longitude = e.Longitude,
                }
                )
                .OrderBy(o => o.id)
                .ToArray();

            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(StationDtoExpXml[]), new XmlRootAttribute("Stantions"));
            serializer.Serialize(new StringWriter(sb), metroStations, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));

            var result = sb.ToString();
            return result;

            //throw new NotImplementedException();
        }

    }
}