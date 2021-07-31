﻿using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistance;
using BlazingTrails.Api.Persistance.Entities;
using BlazingTrails.Shared.Features.ManageTrails.EditTrail;
using BlazingTrails.Shared.Features.ManageTrails.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlazingTrails.Api.Features.ManageTrails.EditTrail
{
    public class EditTrailEndpoint : BaseAsyncEndpoint<EditTrailRequest, bool>
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
            var trail = await _database.Trails.Include(_ => _.Waypoints).SingleOrDefaultAsync(_ => _.Id == request.Trail.Id, cancellationToken: cancellationToken);

            if (trail is null)
            {
                return BadRequest("Trail could not be found.");
            }

            if (!trail.Owner.Equals(HttpContext.User.Identity.Name, StringComparison.CurrentCultureIgnoreCase) && !HttpContext.User.IsInRole("Administrator"))
            {
                return Unauthorized();
            }

            trail.Name = request.Trail.Name;
            trail.Description = request.Trail.Description;
            trail.Location = request.Trail.Location;
            trail.TimeInMinutes = request.Trail.TimeInMinutes;
            trail.Length = request.Trail.Length;
            trail.Waypoints = request.Trail.Waypoints.Select(_ => new Waypoint
            {
                Latitude = _.Latitude,
                Longitude = _.Longitude
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
