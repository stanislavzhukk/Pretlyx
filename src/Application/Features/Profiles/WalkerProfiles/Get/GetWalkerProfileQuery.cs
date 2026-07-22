using Application.Abstractions.Messaging;
using Domain.Common;

namespace Application.Features.Profiles.WalkerProfiles.Get;

public sealed record GetWalkerProfileQuery() : IQuery<Result<WalkerProfileResponse>>;
public sealed record WalkerProfileResponse(Guid Id, DateTimeOffset CreatedAt, string? Email, string? PhoneNumber, int? ExperienceYears, float? Rating, string? Location);
