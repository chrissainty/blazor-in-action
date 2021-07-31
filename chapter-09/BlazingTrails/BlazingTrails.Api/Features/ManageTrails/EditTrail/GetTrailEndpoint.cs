﻿using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistance;
using BlazingTrails.Shared.Features.ManageTrails.EditTrail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
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

        [Authorize]
        [HttpGet(GetTrailRequest.RouteTemplate)]
        public override async Task<ActionResult<GetTrailRequest.Response>> HandleAsync(int trailId, CancellationToken cancellationToken = default)
        {
            var trail = await _context.Trails.Include(_ => _.Waypoints).SingleOrDefaultAsync(_ => _.Id == trailId, cancellationToken: cancellationToken);

            if (trail is null)
            {
                return BadRequest("Trail could not be found.");
            }

            if (!trail.Owner.Equals(HttpContext.User.Identity.Name, StringComparison.CurrentCultureIgnoreCase) && !HttpContext.User.IsInRole("Administrator"))
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
                trail.Waypoints.Select(_ => new GetTrailRequest.Waypoint(_.Latitude, _.Longitude))));

            return Ok(response);
        }
    }
}
