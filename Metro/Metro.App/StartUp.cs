using System;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using AutoMapper;
using Metro.Data;
using Metro.DataProcessor;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Metro.App
{
    class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new MetroDbContext();

            //ResetDatabase(context);

            //Console.WriteLine("Database Reset.");

            Mapper.Initialize(cfg => cfg.AddProfile<MetroProfile>());

            //ImportEntities(context);

            //ExportEntities(context);

            //BonusTask(context);

            string wellcome = File.ReadAllText("Menu.txt");

            while (true)
            {
                Console.WriteLine(wellcome);
                Console.Write("Make your choice: ");

                string command = Console.ReadLine();
                Console.WriteLine(command != "0" ? "Loading..." : string.Empty);
                string output = string.Empty;

                try
                {
                    switch (command)
                    {
                        case "0":
                            Environment.Exit(0);
                            break;
                        case "1":
                            output = DbInitializer.ResetDatabase();
                            break;
                        case "2":
                            output = DbInitializer.ImportJSONData();
                            break;
                        case "3":
                            output = DbInitializer.ImportXMLData();
                            break;
                        case "4":
                            output = DbInitializer.ExportJsonData();
                            break;
                        case "5":
                            output = DbInitializer.ExportXmlData();
                            break;
                        default:
                            throw new InvalidOperationException("Invalid command!");
                    }

                    Console.Clear();
                    Console.WriteLine(output);
                    Console.WriteLine();
                    Console.WriteLine(new string('=', 15));
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.GetBaseException().Message);
                }
            }

        }
        public class DbInitializer
        {
            internal static string ResetDatabase()
            {
                using (var db = new MetroDbContext())
                {
                    db.Database.EnsureDeleted();

                    //db.Database.Migrate();

                    db.Database.EnsureCreated();
                }

                return "Database Reset Successfull";
            }


            internal static string ImportJSONData()
            {
                using (var db = new MetroDbContext())
                {
                    ImportEntitiesJson(db);
                }
                return "JSON Data Imported";
            }

            internal static string ImportXMLData()
            {
                using (var db = new MetroDbContext())
                {
                    ImportEntitiesXml(db);
                }
                return "XML Data Imported";
            }

            internal static string ExportJsonData()
            {
                using (var db = new MetroDbContext())
                {
                    ExportEntitiesJson(db);
                }
                return "XML Data Exported";
            }

            internal static string ExportXmlData()
            {
                using (var db = new MetroDbContext())
                {
                    ExportEntitiesXml(db);
                }
                return "XML Data Exported";
            }
        }

        public static void ImportEntitiesJson(MetroDbContext context, string baseDir = @"..\Datasets\")
        {
            const string exportDir = "./ImportResults/";

            string json;
            using (var client = new WebClient())
            {
                json = client.DownloadString("http://drone.sumc.bg/api/v1/metro/all");
            }
            
            var stations = DataProcessor.Deserializer.ImportJsonStations(context, json);
            PrintAndExportEntityToFile(stations, exportDir + "Stations_JSON_Import_Log.txt");

            //var employees = DataProcessor.Deserializer.ImportEmployees(context, File.ReadAllText(baseDir + "employees.json"));
            //PrintAndExportEntityToFile(employees, exportDir + "Employees.txt");


            //var orders = DataProcessor.Deserializer.ImportOrders(context, File.ReadAllText(baseDir + "orders.xml"));
            //PrintAndExportEntityToFile(orders, exportDir + "Orders.txt");
        }

        public static void ImportEntitiesXml(MetroDbContext context, string baseDir = @"..\Datasets\")
        {
            const string exportDir = "./ImportResults/";

            var station = DataProcessor.Deserializer.ImportXmlStantions(context, File.ReadAllText(baseDir + "stations.xml"));
            PrintAndExportEntityToFile(station, exportDir + "Stations_XML_Import_Log.txt");
        }

        private static void ExportEntitiesJson(MetroDbContext context)
        {
            const string exportDir = "./ExportResults/";

            var jsonOutput = DataProcessor.Serializer.ExportStantionJson(context);
            Console.WriteLine(jsonOutput);
            File.WriteAllText(exportDir + "Stations.json", jsonOutput);

            //    var jsonOutput = DataProcessor.Serializer.ExportOrdersByEmployee(context, "Avery Rush", "ToGo");
            //    Console.WriteLine(jsonOutput);
            //    File.WriteAllText(exportDir + "OrdersByEmployee.json", jsonOutput);

            //    var xmlOutput = DataProcessor.Serializer.ExportCategoryStatistics(context, "Chicken,Drinks,Toys");
            //    Console.WriteLine(xmlOutput);
            //    File.WriteAllText(exportDir + "CategoryStatistics.xml", xmlOutput);
            //}
        }

        private static void ExportEntitiesXml(MetroDbContext context)
        {
            const string exportDir = "./ExportResults/";

            var xmlOutput = DataProcessor.Serializer.ExportStantionXml(context);
            Console.WriteLine(xmlOutput);
            File.WriteAllText(exportDir + "Stations.xml", xmlOutput);
        }

        //private static void BonusTask(MetroDbContext context)
        //{
        //    var bonusOutput = DataProcessor.Bonus.UpdatePrice(context, "Cheeseburger", 6.50m);
        //    Console.WriteLine(bonusOutput);

        private static void PrintAndExportEntityToFile(string entityOutput, string outputPath)
        {
            Console.WriteLine(entityOutput);
            File.WriteAllText(outputPath, entityOutput.TrimEnd());
        }

        private static void ResetDatabase(MetroDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

    }

    
}

