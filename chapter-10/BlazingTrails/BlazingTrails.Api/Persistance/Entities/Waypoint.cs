using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazingTrails.Api.Persistance.Entities
{
    public class Waypoint
    {
        public int Id { get; set; }
        public int TrailId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public Trail Trail { get; set; }
    }

    public class WaypointConfig : IEntityTypeConfiguration<Waypoint>
    {
        public void Configure(EntityTypeBuilder<Waypoint> builder)
        {
            builder.Property(_ => _.TrailId).IsRequired();
            builder.Property(_ => _.Latitude).IsRequired();
            builder.Property(_ => _.Longitude).IsRequired();
        }
    }
}
