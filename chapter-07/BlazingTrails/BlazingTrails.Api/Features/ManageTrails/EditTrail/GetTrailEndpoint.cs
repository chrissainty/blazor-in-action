using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistance;
using BlazingTrails.Shared.Features.ManageTrails.EditTrail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlazingTrails.Api.Features.ManageTrails.EditTrail
{
    public class GetTrailEndpoint : BaseAsyncEndpoint<int, GetTrailRequest.Response>
    {
        private readonly BlazingTrailsContext _context;

        public GetTrailEndpoint(BlazingTrailsContext context)
        {
            _context = context;
        }

        [HttpGet(GetTrailRequest.RouteTemplate)]
        public override async Task<ActionResult<GetTrailRequest.Response>> HandleAsync(int trailId, CancellationToken cancellationToken = default)
        {
            var trail = await _context.Trails.Include(_ => _.Route).SingleOrDefaultAsync(_ => _.Id == trailId, cancellationToken: cancellationToken);

            if (trail is null)
            {
                return BadRequest("Trail could not be found.");
            }

            var response = new GetTrailRequest.Response(new GetTrailRequest.Trail(trail.Id,
                trail.Name,
                trail.Location,
                trail.Image,
                trail.TimeInMinutes,
                trail.Length,
                trail.Description,
                trail.Route.Select(_ => new GetTrailRequest.RouteInstruction(_.Id, _.Stage, _.Description))));

            return Ok(response);
        }
    }
}
