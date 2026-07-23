using FluentValidation;

namespace Application.Features.Profiles.OwnerProfiles.Update;
public sealed class UpdateOwnerProfileValidator : AbstractValidator<UpdateOwnerProfileCommand>
{
    public UpdateOwnerProfileValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(90);

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(16);
    }
}
