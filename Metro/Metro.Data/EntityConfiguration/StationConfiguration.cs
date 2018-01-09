namespace Metro.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class StationConfiguration :IEntityTypeConfiguration<Station>
    {
        public void Configure(EntityTypeBuilder<Station> builder)
        {
            builder.HasKey(s => s.Id);
            
            builder.Property(s => s.RouteId)
                .IsRequired();

            builder.Property(s => s.Code)
                .IsRequired();

            builder.Property(s => s.PointId)
                .IsRequired();

            builder.Property(s => s.Name)
                .IsRequired();

            builder.Property(s => s.Latitude)
                .HasColumnType("decimal(18,6)")
                .IsRequired();

            builder.Property(s => s.Longitude)
                .HasColumnType("decimal(18,6)")
                .IsRequired();
        }
    }
}
