using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistance;
using BlazingTrails.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlazingTrails.Api.Endpoints
{
    public class GetTrailsEndpoint : BaseAsyncEndpoint<GetTrailsRequest.Response>
    {
        private readonly BlazingTrailsContext _context;

        public GetTrailsEndpoint(BlazingTrailsContext context)
        {
            _context = context;
        }

        [HttpGet(GetTrailsRequest.RouteTemplate)]
        public override async Task<ActionResult<GetTrailsRequest.Response>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var trails = await _context.Trails.Include(_ => _.Route)
                                              .ToListAsync(cancellationToken: cancellationToken);

            var response = new GetTrailsRequest.Response(trails.Select(_ => new GetTrailsRequest.Trail(_.Id,
                                                                                                       _.Name,
                                                                                                       _.Image,
                                                                                                       _.Location,
                                                                                                       _.TimeInMinutes,
                                                                                                       _.Length,
                                                                                                       _.Description,
                                                                                                       _.Route.Select(_ => new GetTrailsRequest.RouteInstruction(_.Id,
                                                                                                                                                                 _.Stage,
                                                                                                                                                                 _.Description)))));

            return Ok(response);
        }
    }
}
