using System;
using System.Collections.Generic;
using System.Text;
using Metro.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Metro.Data.EntityConfiguration
{
    class RouteStationConfiguration : IEntityTypeConfiguration<RouteStation>
    {
        public void Configure(EntityTypeBuilder<RouteStation> builder)
        {
            builder.HasKey(r => new { r.RouteId, r.StationId });

        }
    }
}
