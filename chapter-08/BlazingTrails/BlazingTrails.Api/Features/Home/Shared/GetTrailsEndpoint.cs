using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistance;
using BlazingTrails.Shared.Features.Home.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlazingTrails.Api.Features.Home.Shared
{
    public class GetTrailsEndpoint : BaseAsyncEndpoint<int, GetTrailsRequest.Response>
    {
        private readonly BlazingTrailsContext _context;

        public GetTrailsEndpoint(BlazingTrailsContext context)
        {
            _context = context;
        }

        [HttpGet(GetTrailsRequest.RouteTemplate)]
        public override async Task<ActionResult<GetTrailsRequest.Response>> HandleAsync(int trailId, CancellationToken cancellationToken = default)
        {
            var trails = await _context.Trails.Include(_ => _.Waypoints).ToListAsync(cancellationToken);

            var response = new GetTrailsRequest.Response(trails.Select(trail => new GetTrailsRequest.Trail(
                trail.Id,
                trail.Name,
                trail.Image,
                trail.Location,
                trail.TimeInMinutes,
                trail.Length,
                trail.Description,
                trail.Waypoints.Select(_ => new GetTrailsRequest.Waypoint(_.Latitude, _.Longitude)).ToList()
            )));

            return Ok(response);
        }
    }
}
