﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;
using AutoMapper;
using Metro.Data;
using Metro.DataProcessor.Dto.ImportDto;
using Metro.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace Metro.DataProcessor
{
    public class Deserializer
    {
        private const string FailureMessage = "Invalid data format.";
        private const string SuccessMessage = "Record {0} successfully imported.";

        public static string ImportJsonStations(MetroDbContext context, string jsonString)
        {
            //Import Stations from JSON with DTO and AutoMapper
            StringBuilder sb = new StringBuilder();

            var deserializedStations = JsonConvert.DeserializeObject<StationDtoImp[]>(jsonString);

            var validStations = new List<Station>();

            foreach (var stationDto in deserializedStations)
            {
                if (!IsValid(stationDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var stationAlreadyExists = validStations.Any(s => s.Name == stationDto.Name);

                if (stationAlreadyExists)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var station = Mapper.Map<Station>(stationDto);

                validStations.Add(station);

                sb.AppendLine(string.Format(SuccessMessage, stationDto.Name));
                //Console.WriteLine($"{stationDto.Name} {stationDto.Longitude} {stationDto.Latitude}");

            }

            context.Stations.AddRange(validStations);
            context.SaveChanges();

            var result = sb.ToString();
            return result;
        }


        public static string ImportJsonRoutes(MetroDbContext context, string jsonString)
        {
            //Import Stations from JSON with DTO
            StringBuilder sb = new StringBuilder();

            //var deserializedRoutes = JsonConvert.DeserializeObject<RouteDtoImp[]>(jsonString);
            var deserializedRoutes = JsonConvert.DeserializeObject<List<RouteDtoImp>>(jsonString);

            var validRoutes = new List<Route>();

            foreach (var routeDto in deserializedRoutes)
            {
                if (!IsValid(routeDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var routeAlreadyExists = validRoutes.Any(s => s.RouteName == routeDto.RouteName);

                if (routeAlreadyExists)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                //var route = Mapper.Map<Route>(routeDto);

                var impPoints = routeDto.Points;

                var validPoints = new List<Point>();

                foreach (var pointDtoImp in impPoints)
                {
                    var PointToAdd = new Point
                        {
                            stop = pointDtoImp.stop,
                            StopCode = pointDtoImp.StopCode,
                            StopName = pointDtoImp.StopName,
                            Latitude = pointDtoImp.Latitude,
                            Longitude = pointDtoImp.Longitude,
                            VehicleType = pointDtoImp.VehicleType,
                        };
                    validPoints.Add(PointToAdd);

                }


                var route = new Route
                {
                    Id = routeDto.Id,
                    RouteId = routeDto.RouteId,
                    Type = routeDto.Type,
                    RouteName = routeDto.RouteName,
                    LineId = routeDto.LineId,
                    ExtId = routeDto.ExtId,
                    LineName = routeDto.LineName,
                    Points = validPoints
                };



                validRoutes.Add(route);

                sb.AppendLine(string.Format(SuccessMessage, routeDto.RouteName));
                //Console.WriteLine($"{stationDto.Name} {stationDto.Longitude} {stationDto.Latitude}");

            }

            context.Routes.AddRange(validRoutes);
            context.SaveChanges();

            var result = sb.ToString();
            return result;
        }

        public static string ImportXmlStantions(MetroDbContext context, string xmlString)
        {
            // Import Stations from XML
            StringBuilder sb = new StringBuilder();

            var xmlDoc = XDocument.Parse(xmlString);

            var elements = xmlDoc.Root.Elements();
            
            var stantions = new List<Station>();

            foreach (var e in elements)
            {
                int stantionId = int.Parse(e.Element("id").Value);
                int route_id = int.Parse(e.Element("route_id").Value);
                int code = int.Parse(e.Element("code").Value);
                int point_id = int.Parse(e.Element("point_id").Value);
                string name = e.Element("name").Value;
                decimal latitude = decimal.Parse(e.Element("latitude").Value);
                decimal longitude = decimal.Parse(e.Element("longitude").Value);

                var stantion = new Station()
                {
                    StantionId = stantionId,
                    RouteId = route_id,
                    Code = code,
                    PointId = point_id,
                    Name = name,
                    Latitude = latitude,
                    Longitude = longitude
                };

                stantions.Add(stantion);
                sb.AppendLine(string.Format(SuccessMessage, stantion.Name));
            }

            context.Stations.AddRange(stantions);
            context.SaveChanges();

            var result = sb.ToString();
            return result;

        }
        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);
            return isValid;
        }
    }
}
