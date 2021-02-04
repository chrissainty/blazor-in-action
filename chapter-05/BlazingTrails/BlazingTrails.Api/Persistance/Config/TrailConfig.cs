using BlazingTrails.Api.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazingTrails.Api.Persistance.Config
{
    public class TrailConfig : IEntityTypeConfiguration<Trail>
    {
        public void Configure(EntityTypeBuilder<Trail> builder)
        {
            builder.Property(_ => _.Name).IsRequired();
            builder.Property(_ => _.Description).IsRequired();
            builder.Property(_ => _.Location).IsRequired();
            builder.Property(_ => _.TimeInMinutes).IsRequired();
            builder.Property(_ => _.Length).IsRequired();
        }
    }
}
