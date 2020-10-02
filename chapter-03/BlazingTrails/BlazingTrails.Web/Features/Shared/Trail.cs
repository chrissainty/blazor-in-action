using System.Collections.Generic;

namespace BlazingTrails.Web.Features.Shared
{
    public class Trail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Location { get; set; }
        public string Time { get; set; }
        public int Length { get; set; }
        public bool IsFavourite { get; set; }
        public IEnumerable<RouteInstruction> Route { get; set; }
    }

    public class RouteInstruction
    {
        public int Stage { get; set; }
        public string Description { get; set; }
    }
}
