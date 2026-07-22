using System.Numerics;
using Api.Extensions;
using Application.Abstractions.Messaging;
using Application.Features.Profiles.WalkerProfiles.Create;
using Application.Features.Todos.Create;
using Domain.Common;
using Microsoft.AspNetCore.Http.HttpResults;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Api.Endpoints;

public static class WalkerProfileEndpoints
{
    public static void MapWalkerProfileEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/Walker-profiles")
            .WithTags("WalkerProfiles")
            .RequireAuthorization();

        group.MapPost("/", Create)
            .AddEndpointFilter<ValidationFilter<CreateWalkerProfileCommand>>()
            .WithName("CreateWalkerProfile")
            .WithSummary("Create a new Walker profile");
    }

    private static async Task<IResult> Create(
        CreateWalkerProfileCommand command,
        ICommandHandler<CreateWalkerProfileCommand, Result<CreateWalkerProfileResponse>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(command, cancellationToken);
        return result.IsSuccess
            ? TypedResults.CreatedAtRoute(result.Value, "GetWalkerProfileById", new { id = result.Value!.Id })
            : result.ToProblemDetails();
    }
}
