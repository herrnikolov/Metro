using System;
using System.Collections.Generic;
using System.Text;
using Metro.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Metro.Data.EntityConfiguration
{
    public class PointConfiguration :IEntityTypeConfiguration<Point>
    {
        public void Configure(EntityTypeBuilder<Point> builder)
        {
            builder.HasKey(p => p.Id);

            //builder.Property(p => p.Latitude)
            //    .HasColumnType("decimal(18,6)")
            //    .IsRequired();

            //builder.Property(p => p.Longitude)
            //    .HasColumnType("decimal(18,6)")
            //    .IsRequired();

        }
    }
}
