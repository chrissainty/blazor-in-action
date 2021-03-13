using BlazingTrails.Shared.Features.ManageTrails.Shared;
using FluentValidation;
using MediatR;

namespace BlazingTrails.Shared.Features.ManageTrails.EditTrail
{
    public record UpdateTrailRequest(TrailDto Trail) : IRequest<UpdateTrailRequest.Response>
    {
        public const string RouteTemplate = "/api/trails";

        public record Response(bool IsSuccess);
    }

    public class UpdateTrailRequestValidator : AbstractValidator<UpdateTrailRequest>
    {
        public UpdateTrailRequestValidator()
        {
            RuleFor(_ => _.Trail).SetValidator(new TrailValidator());
        }
    }
}
