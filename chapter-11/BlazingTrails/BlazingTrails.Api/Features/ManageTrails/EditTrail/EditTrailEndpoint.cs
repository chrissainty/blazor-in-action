using BlazingTrails.Api.Persistence;
using BlazingTrails.Shared.Features.ManageTrails.EditTrail;
using BlazingTrails.Shared.Features.ManageTrails.Shared;

namespace BlazingTrails.Api.Features.ManageTrails.EditTrail;

public class EditTrailEndpoint : BaseAsyncEndpoint.WithRequest<EditTrailRequest>.WithResponse<bool>
{
    private readonly BlazingTrailsContext _database;

    public EditTrailEndpoint(BlazingTrailsContext database)
    {
        _database = database;
    }

    [Authorize]
    [HttpPut(EditTrailRequest.RouteTemplate)]
    public override async Task<ActionResult<bool>> HandleAsync(EditTrailRequest request, CancellationToken cancellationToken = default)
    {
        var trail = await _database.Trails.Include(x => x.Waypoints).SingleOrDefaultAsync(x => x.Id == request.Trail.Id, cancellationToken: cancellationToken);

        if (trail is null)
        {
            return BadRequest("Trail could not be found.");
        }

        if (!trail.Owner.Equals(HttpContext.User.Identity!.Name, StringComparison.CurrentCultureIgnoreCase) && !HttpContext.User.IsInRole("Administrator"))
        {
            return Unauthorized();
        }

        trail.Name = request.Trail.Name;
        trail.Description = request.Trail.Description;
        trail.Location = request.Trail.Location;
        trail.TimeInMinutes = request.Trail.TimeInMinutes;
        trail.Length = request.Trail.Length;
        trail.Waypoints = request.Trail.Waypoints.Select(wp => new Waypoint
        {
            Latitude = wp.Latitude,
            Longitude = wp.Longitude
        }).ToList();

        if (request.Trail.ImageAction == ImageAction.Remove)
        {
            System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "Images", trail.Image!));
            trail.Image = "";
        }

        await _database.SaveChangesAsync(cancellationToken);

        return Ok(true);
    }
}
