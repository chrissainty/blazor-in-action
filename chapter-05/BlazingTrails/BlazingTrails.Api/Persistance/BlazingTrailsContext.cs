using BlazingTrails.Api.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingTrails.Api.Persistance
{
    public class BlazingTrailsContext : DbContext
    {
        public DbSet<Trail> Trails { get; set; }
        public DbSet<RouteInstruction> RouteInstructions { get; set; }

        public BlazingTrailsContext(DbContextOptions<BlazingTrailsContext> options) : base(options) { }
    }
}
