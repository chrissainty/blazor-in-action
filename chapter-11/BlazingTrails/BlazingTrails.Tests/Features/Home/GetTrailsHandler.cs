using BlazingTrails.Shared.Features.Home.Shared;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BlazingTrails.Tests.Features.Home
{
    public class GetTrailsHandler : IRequestHandler<GetTrailsRequest, GetTrailsRequest.Response>
    {
        public async Task<GetTrailsRequest.Response> Handle(GetTrailsRequest request, CancellationToken cancellationToken)
        {
            return new GetTrailsRequest.Response(new List<GetTrailsRequest.Trail>
            {
                new GetTrailsRequest.Trail(
                    1,
                    "Test Trail 1",
                    "testtrail1.png",
                    "Test Location",
                    60,
                    5,
                    "Test Description",
                    "Test User",
                    new List<GetTrailsRequest.Waypoint>
                    {
                        new GetTrailsRequest.Waypoint(1.55m, 2.33m)
                    }),
                new GetTrailsRequest.Trail(
                    2,
                    "Test Trail 2",
                    "testtrail1.png",
                    "Test Location",
                    60,
                    5,
                    "Test Description",
                    "Test User",
                    new List<GetTrailsRequest.Waypoint>
                    {
                        new GetTrailsRequest.Waypoint(1.55m, 2.33m)
                    }),
                new GetTrailsRequest.Trail(
                    3,
                    "Test Trail 3",
                    "testtrail1.png",
                    "Test Location",
                    60,
                    5,
                    "Test Description",
                    "Test User",
                    new List<GetTrailsRequest.Waypoint>
                    {
                        new GetTrailsRequest.Waypoint(1.55m, 2.33m)
                    })
            });
        }
    }
}
