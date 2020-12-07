using System.Collections.Generic;

namespace BlazingTrails.Api.Persistance.Entities
{
    public class Trail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Location { get; set; }
        public int TimeInMinutes { get; set; }
        public int Length { get; set; }
        public bool IsFavourite { get; set; }

        public ICollection<RouteInstruction> Route { get; set; }
    }
}
