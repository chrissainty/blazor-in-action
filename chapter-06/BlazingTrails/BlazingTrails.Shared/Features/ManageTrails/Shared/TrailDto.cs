using FluentValidation;
using System.Collections.Generic;

namespace BlazingTrails.Shared.Features.ManageTrails.Shared
{
    public class TrailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Image { get; set; }
        public ImageAction ImageAction { get; set; }
        public int TimeInMinutes { get; set; }
        public int Length { get; set; }
        public List<RouteInstruction> Route { get; set; } = new List<RouteInstruction>();

        public class RouteInstruction
        {
            public int Stage { get; set; }
            public string Description { get; set; }
        }
    }

    public enum ImageAction
    {
        None,
        Add,
        Remove
    }

    public class TrailValidator : AbstractValidator<TrailDto>
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

    public class RouteInstructionValidator : AbstractValidator<TrailDto.RouteInstruction>
    {
        public RouteInstructionValidator()
        {
            RuleFor(_ => _.Stage).NotEmpty().WithMessage("Please enter a stage");
            RuleFor(_ => _.Description).NotEmpty().WithMessage("Please enter a description");
        }
    }
}
