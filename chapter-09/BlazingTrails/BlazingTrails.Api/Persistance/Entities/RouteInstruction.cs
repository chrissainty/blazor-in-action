using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazingTrails.Api.Persistance.Entities
{
    public class RouteInstruction
    {
        public int Id { get; set; }
        public int TrailId { get; set; }
        public int Stage { get; set; }
        public string Description { get; set; }

        public Trail Trail { get; set; }
    }

    public class RouteInstructionConfig : IEntityTypeConfiguration<RouteInstruction>
    {
        public void Configure(EntityTypeBuilder<RouteInstruction> builder)
        {
            builder.Property(_ => _.TrailId).IsRequired();
            builder.Property(_ => _.Description).IsRequired();
        }
    }
}
