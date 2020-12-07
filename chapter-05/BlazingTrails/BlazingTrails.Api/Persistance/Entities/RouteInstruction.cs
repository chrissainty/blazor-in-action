namespace BlazingTrails.Api.Persistance.Entities
{
    public class RouteInstruction
    {
        public int Id { get; set; }
        public int TrailId { get; set; }
        public int Stage { get; set; }
        public string Description { get; set; }

        public Trail Trail { get; set; }
    }
}
