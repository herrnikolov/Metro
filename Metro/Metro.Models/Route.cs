using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Metro.Models
{
    public class Route
    {
        [Key]
        public int Id { get; set; }

        public int RouteId { get; set; }

        public int Type { get; set; }

        public string RouteName { get; set; }

        public int LineId { get; set; }

        public int ExtId { get; set; }

        public int LineName { get; set; }

        public ICollection<Point> Points { get; set; } = new List<Point>();

        public ICollection<RouteStation> RouteStations { get; set; } = new List<RouteStation>();

    }
}
