using BlazingTrails.Api.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazingTrails.Api.Persistance.Config
{
    public class RouteInstructionConfig : IEntityTypeConfiguration<RouteInstruction>
    {
        public void Configure(EntityTypeBuilder<RouteInstruction> builder)
        {
            builder.Property(_ => _.TrailId).IsRequired();
            builder.Property(_ => _.Description).IsRequired();
        }
    }
}
