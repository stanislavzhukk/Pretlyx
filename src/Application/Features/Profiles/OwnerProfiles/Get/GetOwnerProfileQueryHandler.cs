using Application.Abstractions.Data;
using Application.Abstractions.Identity;
using Application.Abstractions.Messaging;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Profiles.OwnerProfiles.Get;

public sealed class GetOwnerProfileQueryHandler(IAppDbContext dbContext, ICurrentUser user) : IQueryHandler<GetOwnerProfileQuery, Result<OwnerProfileResponse>>
{
    public async Task<Result<OwnerProfileResponse>> HandleAsync(GetOwnerProfileQuery query, CancellationToken cancellationToken = default)
    {
        //var OwnerProfile = await dbContext.OwnerProfiles.FindAsync([query.Id], cancellationToken);
        var OwnerProfile = await dbContext.OwnerProfiles.FirstOrDefaultAsync(x => x.UserId == user.UserId, cancellationToken);
        if (OwnerProfile is null)
            return Result.Failure<OwnerProfileResponse>(Error.NotFound("OwnerProfile.NotFound", $"User with '{user.UserId}' don't have a Owner profile."));

        var response = new OwnerProfileResponse(OwnerProfile.Id, OwnerProfile.CreatedAt, OwnerProfile.Email, OwnerProfile.PhoneNumber);
        return Result.Success(response);
    }
}
