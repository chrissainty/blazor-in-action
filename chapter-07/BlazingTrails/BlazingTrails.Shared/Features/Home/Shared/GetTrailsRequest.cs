using MediatR;

namespace BlazingTrails.Shared.Features.Home.Shared;

public record GetTrailsRequest : IRequest<GetTrailsRequest.Response>
{
    public const string RouteTemplate = "/api/trails";

    public record Trail(int Id, string Name, string? Image, string Location, int TimeInMinutes, int Length, string Description);
    public record Response(IEnumerable<Trail> Trails);
}
