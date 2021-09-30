using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistence;
using BlazingTrails.Api.Persistence.Entities;
using BlazingTrails.Shared.Features.ManageTrails;
using Microsoft.AspNetCore.Mvc;

namespace BlazingTrails.Api.Features.ManageTrails;

public class AddTrailEndpoint : BaseAsyncEndpoint.WithRequest<AddTrailRequest>.WithResponse<int>
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
            Location = request.Trail.Location,
            TimeInMinutes = 0,
            Length = request.Trail.Length
        };

        await _database.Trails.AddAsync(trail, cancellationToken);

        var routeInstructions = request.Trail.Route.Select(x => new RouteInstruction
        {
            Stage = x.Stage,
            Description = x.Description,
            Trail = trail
        });

        await _database.RouteInstructions.AddRangeAsync(routeInstructions, cancellationToken);
        await _database.SaveChangesAsync(cancellationToken);

        return Ok(trail.Id);
    }
}
