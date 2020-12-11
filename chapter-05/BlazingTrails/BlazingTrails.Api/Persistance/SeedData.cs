using BlazingTrails.Api.Persistance.Entities;
using System.Collections.Generic;

namespace BlazingTrails.Api.Persistance
{
    public static class SeedData
    {
        public static List<Trail> Trails()
        {
            var trails = new List<Trail>
            {
                new Trail
                { 
                    Id = 1,
                    Name="Countryside Ramble", 
                    Location="Durbach, Germany", 
                    TimeInMinutes=195, 
                    Length=11, 
                    Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla volutpat orci at augue ultricies fermentum. Ut massa lectus, dignissim sed molestie ut, viverra vel diam. Fusce at iaculis magna. Suspendisse vel est et est luctus ornare venenatis ut neque. Pellentesque varius lacus sed arcu pellentesque, a porttitor velit gravida. Nunc nunc lectus, rhoncus consectetur metus eget, fermentum laoreet mauris. Cras ac gravida ante. Ut in ante ex. Proin tristique a ligula vel pharetra. Vestibulum blandit nisl dui, in pulvinar metus mollis pharetra. Duis cursus porttitor libero, quis lacinia velit. Curabitur tincidunt laoreet mi, eu maximus nibh vestibulum sed. Phasellus orci.", 
                },
            };

            return trails;
        }

        public static List<RouteInstruction> RouteInstructions()
        {
            var routeInstructions = new List<RouteInstruction>
            {
                new RouteInstruction { Id = 1, TrailId = 1, Stage = 1, Description= "Lorem ipsum dolor sit amet, consectetur adipiscing elit." },
                new RouteInstruction { Id = 2, TrailId = 1, Stage = 2, Description = "Curabitur interdum molestie tempus." },
                new RouteInstruction { Id = 3, TrailId = 1, Stage = 3, Description = "Suspendisse convallis nunc ut lorem cursus, ac venenatis neque maximus." },
                new RouteInstruction { Id = 4, TrailId = 1, Stage = 4, Description = "Aenean nisi dolor, sollicitudin quis pellentesque vel, pharetra et ex." },
                new RouteInstruction { Id = 5, TrailId = 1, Stage = 5, Description = "Vestibulum vulputate velit quis mi suscipit, eget pharetra enim tristique." }
            };

            return routeInstructions;
        }
    }
}
