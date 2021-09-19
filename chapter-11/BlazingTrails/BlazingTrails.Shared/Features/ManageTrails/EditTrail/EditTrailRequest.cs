using BlazingTrails.Shared.Features.ManageTrails.Shared;
using FluentValidation;
using MediatR;

namespace BlazingTrails.Shared.Features.ManageTrails.EditTrail;

public record EditTrailRequest(TrailDto Trail) : IRequest<EditTrailRequest.Response>
{
    public const string RouteTemplate = "/api/trails";

    public record Response(bool IsSuccess);
}

public class EditTrailRequestValidator : AbstractValidator<EditTrailRequest>
{
    public EditTrailRequestValidator()
    {
        RuleFor(x => x.Trail).SetValidator(new TrailValidator());
    }
}
