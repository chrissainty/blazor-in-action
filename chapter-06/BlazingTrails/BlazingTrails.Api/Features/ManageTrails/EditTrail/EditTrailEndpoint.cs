using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistance;
using BlazingTrails.Api.Persistance.Entities;
using BlazingTrails.Shared.Features.ManageTrails.EditTrail;
using BlazingTrails.Shared.Features.ManageTrails.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlazingTrails.Api.Features.ManageTrails.EditTrail
{
    public class EditTrailEndpoint : BaseAsyncEndpoint
        .WithRequest<EditTrailRequest>
        .WithResponse<bool>
    {
        private readonly BlazingTrailsContext _database;

        public EditTrailEndpoint(BlazingTrailsContext database)
        {
            _database = database;
        }

        [HttpPut(EditTrailRequest.RouteTemplate)]
        public override async Task<ActionResult<bool>> HandleAsync(EditTrailRequest request, CancellationToken cancellationToken = default)
        {
            var trail = await _database.Trails.Include(_ => _.Route).SingleOrDefaultAsync(_ => _.Id == request.Trail.Id, cancellationToken: cancellationToken);

            if (trail is null)
            {
                return BadRequest("Trail could not be found.");
            }

            trail.Name = request.Trail.Name;
            trail.Description = request.Trail.Description;
            trail.Location = request.Trail.Location;
            trail.TimeInMinutes = request.Trail.TimeInMinutes;
            trail.Length = request.Trail.Length;
            trail.Route = request.Trail.Route.Select(_ => new RouteInstruction
            {
                Stage = _.Stage,
                Description = _.Description,
                Trail = trail
            }).ToList();

            if (request.Trail.ImageAction == ImageAction.Remove)
            {
                System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "Images", trail.Image));
                trail.Image = "";
            }

            await _database.SaveChangesAsync(cancellationToken);

            return Ok(true);
        }
    }
}
