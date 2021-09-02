using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistance;
using BlazingTrails.Shared.Features.ManageTrails.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BlazingTrails.Api.Features.ManageTrails.Shared
{
    public class UploadTrailImageEndpoint : BaseAsyncEndpoint<int, string>
    {
        private readonly BlazingTrailsContext _database;

        public UploadTrailImageEndpoint(BlazingTrailsContext database)
        {
            _database = database;
        }

        [Authorize]
        [HttpPost(UploadTrailImageRequest.RouteTemplate)]
        public override async Task<ActionResult<string>> HandleAsync([FromRoute] int trailId, CancellationToken cancellationToken = default)
        {
            var trail = await _database.Trails.SingleOrDefaultAsync(_ => _.Id == trailId, cancellationToken);
            if (trail is null)
            {
                return BadRequest("Trail does not exist.");
            }

            if (!trail.Owner.Equals(HttpContext.User.Identity.Name, StringComparison.CurrentCultureIgnoreCase) && !HttpContext.User.IsInRole("Administrator"))
            {
                return Unauthorized();
            }

            var file = Request.Form.Files[0];
            if (file.Length == 0)
            {
                return BadRequest("No image found.");
            }

            var filename = $"{Guid.NewGuid()}.jpg";
            var saveLocation = Path.Combine(Directory.GetCurrentDirectory(), "Images", filename);

            var resizeOptions = new ResizeOptions
            {
                Mode = ResizeMode.Pad,
                Size = new Size(640, 426)
            };

            using var image = Image.Load(file.OpenReadStream());
            image.Mutate(_ => _.Resize(resizeOptions));
            await image.SaveAsJpegAsync(saveLocation, cancellationToken: cancellationToken);

            if (!string.IsNullOrWhiteSpace(trail.Image))
            {
                System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "Images", trail.Image));
            }

            trail.Image = filename;
            await _database.SaveChangesAsync(cancellationToken);

            return Ok(trail.Image);
        }
    }
}
