using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Features.Profiles.OwnerProfiles.Create;
using Domain.Common;
using Domain.Entities;

namespace Application.Features.Profiles.OwnerProfiles.Create;
public sealed class CreateOwnerProfileHandler(IAppDbContext dbContext) : ICommandHandler<CreateOwnerProfileCommand, Result<CreateOwnerProfileResponse>>
{
    public async Task<Result<CreateOwnerProfileResponse>> HandleAsync(CreateOwnerProfileCommand command, CancellationToken cancellationToken = default)
    {
        var ownerProfile = new OwnerProfile
        {
            UserId = command.UserId,
            Name = command.Name,
            PhoneNumber = command.Phone ?? string.Empty,
        };

        dbContext.OwnerProfiles.Add(ownerProfile);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = new CreateOwnerProfileResponse(ownerProfile.Id, ownerProfile.UserId, ownerProfile.Name, ownerProfile.PhoneNumber);
        return Result.Success(response);
    }
}
