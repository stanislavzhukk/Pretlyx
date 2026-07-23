using System;
using System.Collections.Generic;
using System.Text;
using Application.Abstractions.Messaging;
using Domain.Common;

namespace Application.Features.Profiles.OwnerProfiles.Create;

public sealed record CreateOwnerProfileCommand(string Name, string? Phone) : ICommand<Result<CreateOwnerProfileResponse>>;

public sealed record CreateOwnerProfileResponse(Guid Id, string Name, string? PhoneNumber);
