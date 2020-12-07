using Ardalis.ApiEndpoints;
using BlazingTrails.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingTrails.Api.Endpoints
{
    public class AddTrailEndpoint : BaseEndpoint<AddTrailRequest, bool>
    {
        [HttpPost(AddTrailRequest.RouteTemplate)]
        public override ActionResult<bool> Handle(AddTrailRequest request)
        {
            // TODO: Add a store of some kind for the trails
            return Ok();
        }
    }
}
