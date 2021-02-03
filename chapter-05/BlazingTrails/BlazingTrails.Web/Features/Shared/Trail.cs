using FluentValidation;
using System;
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
        public int TimeInMinutes { get; set; }
        public string TimeFormatted => $"{TimeInMinutes / 60}h {TimeInMinutes % 60}m";
        public int Length { get; set; }
        public List<RouteInstruction> Route { get; set; }
    }

    public class TrailValidator : AbstractValidator<Trail>
    {
        public TrailValidator()
        {
            RuleFor(_ => _.Name).NotEmpty().WithMessage("Please enter a name");
            RuleFor(_ => _.Description).NotEmpty().WithMessage("Please enter a description");
            RuleFor(_ => _.Location).NotEmpty().WithMessage("Please enter a location");
            RuleFor(_ => _.TimeInMinutes).GreaterThan(0).WithMessage("Please enter a time");
            RuleFor(_ => _.Length).GreaterThan(0).WithMessage("Please enter a length");
            RuleFor(_ => _.Route).NotEmpty().WithMessage("Please add a route instruction");
            RuleForEach(_ => _.Route).SetValidator(new RouteInstructionValidator());
        }
    }


    public class RouteInstruction
    {
        public int Stage { get; set; }
        public string Description { get; set; }
    }

    public class RouteInstructionValidator : AbstractValidator<RouteInstruction>
    {
        public RouteInstructionValidator()
        {
            RuleFor(_ => _.Stage).NotEmpty().WithMessage("Please enter a stage");
            RuleFor(_ => _.Description).NotEmpty().WithMessage("Please enter a description");
        }
    }
}
