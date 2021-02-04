using BlazingTrails.Api.Persistance.Config;
using BlazingTrails.Api.Persistance.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazingTrails.Api.Persistance
{
    public class BlazingTrailsContext : DbContext
    {
        public DbSet<Trail> Trails { get; set; }
        public DbSet<RouteInstruction> RouteInstructions { get; set; }

        public BlazingTrailsContext(DbContextOptions<BlazingTrailsContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TrailConfig());
            modelBuilder.ApplyConfiguration(new RouteInstructionConfig());
        }
    }
}
