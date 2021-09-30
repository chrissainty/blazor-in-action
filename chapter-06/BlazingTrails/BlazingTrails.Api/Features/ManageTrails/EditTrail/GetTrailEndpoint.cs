using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistence;
using BlazingTrails.Shared.Features.ManageTrails.EditTrail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingTrails.Api.Features.ManageTrails.EditTrail;

public class GetTrailEndpoint : BaseAsyncEndpoint.WithRequest<int>.WithResponse<GetTrailRequest.Response>
{
    private readonly BlazingTrailsContext _context;

    public GetTrailEndpoint(BlazingTrailsContext context)
    {
        _context = context;
    }

    [HttpGet(GetTrailRequest.RouteTemplate)]
    public override async Task<ActionResult<GetTrailRequest.Response>> HandleAsync(int trailId, CancellationToken cancellationToken = default)
    {
        var trail = await _context.Trails.Include(x => x.Route).SingleOrDefaultAsync(x => x.Id == trailId, cancellationToken: cancellationToken);

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
            trail.Route.Select(ri => new GetTrailRequest.RouteInstruction(ri.Id, ri.Stage, ri.Description))));

        return Ok(response);
    }
}
