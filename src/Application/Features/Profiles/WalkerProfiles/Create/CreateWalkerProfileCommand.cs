using System;
using System.Collections.Generic;
using System.Text;
using Application.Abstractions.Messaging;
using Domain.Common;

namespace Application.Features.Profiles.WalkerProfiles.Create;

public sealed record CreateWalkerProfileCommand(string Name, string? Phone) : ICommand<Result<CreateWalkerProfileResponse>>;

public sealed record CreateWalkerProfileResponse(Guid Id, string Name, string? Phone);
