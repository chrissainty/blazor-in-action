using BlazingTrails.Api.Persistence;
using BlazingTrails.Shared.Features.ManageTrails.EditTrail;

namespace BlazingTrails.Api.Features.ManageTrails.EditTrail;

public class GetTrailEndpoint : BaseAsyncEndpoint.WithRequest<int>.WithResponse<GetTrailRequest.Response>
{
    private readonly BlazingTrailsContext _context;

    public GetTrailEndpoint(BlazingTrailsContext context)
    {
        _context = context;
    }

    [Authorize]
    [HttpGet(GetTrailRequest.RouteTemplate)]
    public override async Task<ActionResult<GetTrailRequest.Response>> HandleAsync(int trailId, CancellationToken cancellationToken = default)
    {
        var trail = await _context.Trails.Include(x => x.Waypoints).SingleOrDefaultAsync(x => x.Id == trailId, cancellationToken: cancellationToken);

        if (trail is null)
        {
            return BadRequest("Trail could not be found.");
        }

        if (!trail.Owner.Equals(HttpContext.User.Identity!.Name, StringComparison.CurrentCultureIgnoreCase) && !HttpContext.User.IsInRole("Administrator"))
        {
            return Unauthorized();
        }

        var response = new GetTrailRequest.Response(new GetTrailRequest.Trail(
            trail.Id,
            trail.Name,
            trail.Location,
            trail.Image,
            trail.TimeInMinutes,
            trail.Length,
            trail.Description,
            trail.Waypoints.Select(wp => new GetTrailRequest.Waypoint(wp.Latitude, wp.Longitude))));

        return Ok(response);
    }
}
