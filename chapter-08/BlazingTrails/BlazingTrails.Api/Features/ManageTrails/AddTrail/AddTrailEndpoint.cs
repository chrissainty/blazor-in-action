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
                IsFavourite = false
            };

            await _database.Trails.AddAsync(trail, cancellationToken);

            //var routeInstructions = request.Trail.Route.Select(_ => new RouteInstruction
            //{
            //    Stage = _.Stage,
            //    Description = _.Description,
            //    Trail = trail
            //});

            var waypoints = request.Trail.Waypoints.Select(_ => new Waypoint
            {
                Latitude = _.Latitude,
                Longitude = _.Longitude,
                Trail = trail
            });

            await _database.Waypoints.AddRangeAsync(waypoints, cancellationToken);
            await _database.SaveChangesAsync(cancellationToken);

            return Ok(trail.Id);
        }
    }
}
