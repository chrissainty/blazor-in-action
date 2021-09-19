using BlazingTrails.Shared.Features.ManageTrails.Shared;
using FluentValidation;
using MediatR;

namespace BlazingTrails.Shared.Features.ManageTrails.AddTrail;

public record AddTrailRequest(TrailDto Trail) : IRequest<AddTrailRequest.Response>
{
    public const string RouteTemplate = "/api/trails";

    public record Response(int TrailId);
}

public class AddTrailRequestValidator : AbstractValidator<AddTrailRequest>
{
    public AddTrailRequestValidator()
    {
        RuleFor(x => x.Trail).SetValidator(new TrailValidator());
    }
}
