using Application.Abstractions.Data;
using Application.Abstractions.Identity;
using Application.Abstractions.Messaging;
using Application.Features.Profiles.WalkerProfiles.Create;
using Domain.Common;
using Domain.Entities;

namespace Application.Features.Profiles.WalkerProfiles.Create;

public sealed class CreateWalkerProfileCommandHandler(IAppDbContext dbContext, ICurrentUser user) : ICommandHandler<CreateWalkerProfileCommand, Result<CreateWalkerProfileResponse>>
{
    public async Task<Result<CreateWalkerProfileResponse>> HandleAsync(CreateWalkerProfileCommand command, CancellationToken cancellationToken = default)
    {
        var userId = user.UserId ?? throw new InvalidOperationException("User ID is not available");
        var WalkerProfile = new WalkerProfile
        {
            UserId = userId,
            Name = command.Name,
            PhoneNumber = command.Phone ?? string.Empty,
        };

        dbContext.WalkerProfiles.Add(WalkerProfile);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = new CreateWalkerProfileResponse(WalkerProfile.Id, WalkerProfile.Name, WalkerProfile.PhoneNumber);
        return Result.Success(response);
    }
}
