using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistence;
using BlazingTrails.Api.Persistence.Entities;
using BlazingTrails.Shared.Features.ManageTrails.AddTrail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazingTrails.Api.Features.ManageTrails.AddTrail;

public class AddTrailEndpoint : BaseAsyncEndpoint.WithRequest<AddTrailRequest>.WithResponse<int>
{
    private readonly BlazingTrailsContext _database;

    public AddTrailEndpoint(BlazingTrailsContext database)
    {
        _database = database;
    }

    [Authorize]
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
            Owner = HttpContext.User.Identity!.Name!,
            Waypoints = request.Trail.Waypoints.Select(wp => new Waypoint
            {
                Latitude = wp.Latitude,
                Longitude = wp.Longitude
            }).ToList()
        };

        await _database.Trails.AddAsync(trail, cancellationToken);
        await _database.SaveChangesAsync(cancellationToken);

        return Ok(trail.Id);
    }
}
