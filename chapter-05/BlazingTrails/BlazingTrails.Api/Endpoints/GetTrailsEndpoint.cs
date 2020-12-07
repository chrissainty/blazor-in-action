using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistance;
using BlazingTrails.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlazingTrails.Api.Endpoints
{
    public class GetTrailsEndpoint : BaseAsyncEndpoint<GetTrailsRequest, GetTrailsRequest.Response>
    {
        private readonly ILogger<GetTrailsEndpoint> _logger;
        private readonly BlazingTrailsContext _context;

        public GetTrailsEndpoint(ILogger<GetTrailsEndpoint> logger, BlazingTrailsContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(GetTrailsRequest.RouteTemplate)]
        public override async Task<ActionResult<GetTrailsRequest.Response>> HandleAsync(GetTrailsRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogDebug("Hit Handler");
            var trails = await _context.Trails.Include(_ => _.Route)
                                              .ToListAsync(cancellationToken: cancellationToken);
            _logger.LogDebug("Hit Db");
            var response = new GetTrailsRequest.Response(trails.Select(_ => new GetTrailsRequest.Trail(_.Id,
                                                                                                       _.Name,
                                                                                                       _.Location,
                                                                                                       _.TimeInMinutes,
                                                                                                       _.Length,
                                                                                                       _.Description,
                                                                                                       _.Route.Select(_ => new GetTrailsRequest.RouteInstruction(_.Id,
                                                                                                                                                                 _.Stage,
                                                                                                                                                                 _.Description)))));
            _logger.LogDebug("Response created");
            return Ok(response);
        }
    }
}
