using System.Collections.Generic;

namespace BlazingTrails.Shared
{
    public class GetTrailsRequest
    {
        public const string RouteTemplate = "/api/trails";

        public record Trail(int Id, string Name, string Location, int TimeInMinutes, int Length, string Description, IEnumerable<RouteInstruction> RouteInstructions);
        public record RouteInstruction(int Id, int Stage, string Description);
        public record Response(IEnumerable<Trail> Trails);
    }
}
