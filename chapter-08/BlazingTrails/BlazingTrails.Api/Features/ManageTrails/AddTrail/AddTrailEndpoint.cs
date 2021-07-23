using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistance;
using BlazingTrails.Api.Persistance.Entities;
using BlazingTrails.Shared.Features.ManageTrails.AddTrail;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlazingTrails.Api.Features.ManageTrails.AddTrail
{
    public class AddTrailEndpoint : BaseAsyncEndpoint<AddTrailRequest, int>
    {
        private readonly BlazingTrailsContext _database;

        public AddTrailEndpoint(BlazingTrailsContext database)
        {
            _database = database;
        }

        [HttpPost(AddTrailRequest.RouteTemplate)]
        public override async Task<ActionResult<int>> HandleAsync(AddTrailRequest request, CancellationToken cancellationToken = default)
        {
            var trail = new Trail
            {
                Name = request.Trail.Name,
                Description = request.Trail.Description,
                Image = "",
                Location = request.Trail.Location,
                TimeInMinutes = request.Trail.TimeInMinutes,
                Length = request.Trail.Length,
                IsFavourite = false,
                Waypoints = request.Trail.Waypoints.Select(_ => new Waypoint
                {
                    Latitude = _.Latitude,
                    Longitude = _.Longitude
                }).ToList()
            };

            await _database.Trails.AddAsync(trail, cancellationToken);
            await _database.SaveChangesAsync(cancellationToken);

            return Ok(trail.Id);
        }
    }
}
