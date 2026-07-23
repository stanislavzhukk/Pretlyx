using FluentValidation;

namespace Application.Features.Profiles.OwnerProfiles.Create;
public sealed class CreateOwnerProfileValidator : AbstractValidator<CreateOwnerProfileCommand>
{
    public CreateOwnerProfileValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(90);

        RuleFor(x => x.Phone)
            .MaximumLength(16);
    }
}
