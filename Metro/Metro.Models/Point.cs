using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Metro.Models
{
    public class Point
    {
        [Key]
        public int Id { get; set; }

        public int stop { get; set; }

        public int StopCode { get; set; }

        public string StopName { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public int VehicleType { get; set; }
    }
}
