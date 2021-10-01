using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazingTrails.Api.Persistence.Entities;

public class RouteInstruction
{
    public int Id { get; set; }
    public int TrailId { get; set; }
    public int Stage { get; set; }
    public string Description { get; set; } = default!;

    public Trail Trail { get; set; } = default!;
}

public class RouteInstructionConfig : IEntityTypeConfiguration<RouteInstruction>
{
    public void Configure(EntityTypeBuilder<RouteInstruction> builder)
    {
        builder.Property(x => x.TrailId).IsRequired();
        builder.Property(x => x.Description).IsRequired();
    }
}
