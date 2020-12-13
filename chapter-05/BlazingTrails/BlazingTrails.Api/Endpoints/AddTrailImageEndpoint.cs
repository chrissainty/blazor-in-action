using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistance;
using BlazingTrails.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BlazingTrails.Api.Endpoints
{
    public class AddTrailImageEndpoint : BaseAsyncEndpoint<int, bool>
    {
        private readonly BlazingTrailsContext _database;

        public AddTrailImageEndpoint(BlazingTrailsContext database)
        {
            _database = database;
        }

        [HttpPost(AddTrailImageRequest.RouteTemplate)]
        public override async Task<ActionResult<bool>> HandleAsync([FromRoute] int trailId, CancellationToken cancellationToken = default)
        {
            var trail = await _database.Trails.SingleOrDefaultAsync(_ => _.Id == trailId, cancellationToken);
            if (trail is null)
            {
                return BadRequest("Trail does not exist.");
            }

            var file = Request.Form.Files[0];
            if (file.Length == 0)
            {
                return BadRequest("No image found.");
            }

            var filename = $"{Guid.NewGuid()}.jpg";
            var saveLocation = Path.Combine(Directory.GetCurrentDirectory(), "Images", filename);

            using var stream = System.IO.File.Create(saveLocation);
            await file.CopyToAsync(stream);

            trail.Image = $"/Images/{Path.GetFileName(saveLocation)}";
            await _database.SaveChangesAsync();

            return Ok(true);
        }
    }
}
