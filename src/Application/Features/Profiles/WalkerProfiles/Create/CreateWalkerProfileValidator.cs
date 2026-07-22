using FluentValidation;

namespace Application.Features.Profiles.WalkerProfiles.Create;

public sealed class CreateWalkerProfileValidator : AbstractValidator<CreateWalkerProfileCommand>
{
    public CreateWalkerProfileValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(90);
    }
}
