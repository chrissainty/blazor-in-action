﻿using BlazingTrails.ComponentLibrary.Map;
using System.Collections.Generic;

namespace BlazingTrails.Client.Features.Shared
{
    public class Trail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Location { get; set; }
        public int TimeInMinutes { get; set; }
        public string TimeFormatted => $"{TimeInMinutes / 60}h {TimeInMinutes % 60}m";
        public int Length { get; set; }
        public string Owner { get; set; }
        public List<LatLong> Waypoints { get; set; } = new List<LatLong>();
    }
}
