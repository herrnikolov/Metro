using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Metro.Models
{
    public class Station
    {
        [Key]
        public int Id { get; set; }

        public int StantionId { get; set; }

        public int RouteId { get; set; }

        public int Code { get; set; }

        public int PointId { get; set; }

        public string Name { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

    }
}

