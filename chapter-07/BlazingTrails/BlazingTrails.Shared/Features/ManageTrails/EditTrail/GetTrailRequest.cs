using MediatR;

namespace BlazingTrails.Shared.Features.ManageTrails.EditTrail;

public record GetTrailRequest(int TrailId) : IRequest<GetTrailRequest.Response>
    {
        public const string RouteTemplate = "/api/trails/{trailId}";

        public record Response(Trail Trail);
        public record Trail(int Id, string Name, string Location, string? Image, int TimeInMinutes, int Length, string Description, IEnumerable<RouteInstruction> RouteInstructions);
        public record RouteInstruction(int Id, int Stage, string Description);
    }
