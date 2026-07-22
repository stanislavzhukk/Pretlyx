using FluentValidation;

namespace Application.Features.Profiles.OwnerProfiles.Create;
public sealed class CreateOwnerProfileValidator : AbstractValidator<CreateOwnerProfileCommand>
{
    public CreateOwnerProfileValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(90);
    }
}
