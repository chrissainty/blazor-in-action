using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazingTrails.Api.Persistance.Entities;

public class Trail
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Image { get; set; } = null!;
    public string Location { get; set; } = null!;
    public int TimeInMinutes { get; set; }
    public int Length { get; set; }
    public bool IsFavourite { get; set; }
    public string Owner { get; set; } = null!;

    public ICollection<Waypoint> Waypoints { get; set; } = null!;
}

public class TrailConfig : IEntityTypeConfiguration<Trail>
{
    public void Configure(EntityTypeBuilder<Trail> builder)
    {
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.Location).IsRequired();
        builder.Property(x => x.TimeInMinutes).IsRequired();
        builder.Property(x => x.Length).IsRequired();
        builder.Property(x => x.Owner).IsRequired();
    }
}
