using Application.Abstractions.Data;
using Application.Abstractions.Identity;
using Application.Abstractions.Messaging;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Profiles.WalkerProfiles.Get;

public sealed class GetWalkerProfileQueryHandler(IAppDbContext dbContext, ICurrentUser user) : IQueryHandler<GetWalkerProfileQuery, Result<WalkerProfileResponse>>
{
    public async Task<Result<WalkerProfileResponse>> HandleAsync(GetWalkerProfileQuery query, CancellationToken cancellationToken = default)
    {
        //var walkerProfile = await dbContext.WalkerProfiles.FindAsync([query.Id], cancellationToken);
        var walkerProfile = await dbContext.WalkerProfiles.FirstOrDefaultAsync(x => x.UserId == user.UserId, cancellationToken);
        if (walkerProfile is null)
            return Result.Failure<WalkerProfileResponse>(Error.NotFound("WalkerProfile.NotFound", $"User with '{user.UserId}' don't have a walker profile."));

        var response = new WalkerProfileResponse(walkerProfile.Id, walkerProfile.CreatedAt, walkerProfile.Email, walkerProfile.PhoneNumber, walkerProfile.ExperienceYears, walkerProfile.Rating, walkerProfile.Location);
        return Result.Success(response);
    }
}
