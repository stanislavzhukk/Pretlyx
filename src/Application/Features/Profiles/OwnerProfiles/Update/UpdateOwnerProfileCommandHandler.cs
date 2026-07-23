using Application.Abstractions.Data;
using Application.Abstractions.Identity;
using Application.Abstractions.Messaging;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Features.Profiles.OwnerProfiles.Update;
public sealed class UpdateOwnerProfileCommandHandler(IAppDbContext dbContext, ICurrentUser user) : ICommandHandler<UpdateOwnerProfileCommand, Result<UpdateOwnerProfileResponse>>
{
    public async Task<Result<UpdateOwnerProfileResponse>> HandleAsync(UpdateOwnerProfileCommand command, CancellationToken cancellationToken = default)
    {
        if(command.Name is null && command.PhoneNumber is null)
        {
            return Result.Failure<UpdateOwnerProfileResponse>(
                Error.Validation(
                    "OwnerProfile.Update.InvalidCommand",
                    "At least one property must be provided to update the owner profile."));
        }
        var affectedRows = await dbContext.OwnerProfiles
           .Where(x => x.UserId == user.UserId)
           .ExecuteUpdateAsync(s =>
           {
               if (command.Name is not null)
                   s.SetProperty(x => x.Name, command.Name);

               if (command.PhoneNumber is not null)
                   s.SetProperty(x => x.PhoneNumber, command.PhoneNumber);
           }, cancellationToken);

        if (affectedRows == 0)
        {
            return Result.Failure<UpdateOwnerProfileResponse>(
                Error.NotFound(
                    "OwnerProfile.NotFound",
                    $"User with '{user.UserId}' doesn't have an owner profile."));
        }

        var ownerProfile = await dbContext.OwnerProfiles
            .AsNoTracking()
            .Where(x => x.UserId == user.UserId)
            .Select(x => new UpdateOwnerProfileResponse(
                x.Id,
                x.Name,
                x.PhoneNumber))
            .FirstAsync(cancellationToken);

        return Result.Success(ownerProfile);
    }
}
