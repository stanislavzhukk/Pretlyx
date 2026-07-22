using Application.Abstractions.Data;
using Application.Abstractions.Identity;
using Application.Abstractions.Messaging;
using Application.Features.Profiles.OwnerProfiles.Create;
using Domain.Common;
using Domain.Entities;

namespace Application.Features.Profiles.OwnerProfiles.Create;
public sealed class CreateOwnerProfileCommandHandler(IAppDbContext dbContext, ICurrentUser user) : ICommandHandler<CreateOwnerProfileCommand, Result<CreateOwnerProfileResponse>>
{
    public async Task<Result<CreateOwnerProfileResponse>> HandleAsync(CreateOwnerProfileCommand command, CancellationToken cancellationToken = default)
    {
        Guid.TryParse(user.UserId, out var userId1);
        Console.WriteLine(userId1);
        if (!Guid.TryParse(user.UserId, out var userId))
        {
            return Result.Failure<CreateOwnerProfileResponse>(Error.Failure("404","UserId not found"));
        }
        var ownerProfile = new OwnerProfile
        {
            UserId = userId,
            Name = command.Name,
            PhoneNumber = command.Phone ?? string.Empty,
        };

        dbContext.OwnerProfiles.Add(ownerProfile);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = new CreateOwnerProfileResponse(ownerProfile.Id, ownerProfile.Name, ownerProfile.PhoneNumber);
        return Result.Success(response);
    }
}
