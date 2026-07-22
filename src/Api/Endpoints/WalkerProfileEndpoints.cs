using System.Numerics;
using Api.Extensions;
using Application.Abstractions.Identity;
using Application.Abstractions.Messaging;
using Application.Features.Profiles.WalkerProfiles.Create;
using Application.Features.Profiles.WalkerProfiles.Get;
using Application.Features.Todos.Create;
using Application.Features.Todos.Get;
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

        group.MapGet("/", Get)
            .WithName("GetWalkerProfile")
            .WithSummary("Get the current user's walker profile");
    }

    private static async Task<IResult> Create(
        CreateWalkerProfileCommand command,
        ICommandHandler<CreateWalkerProfileCommand, Result<CreateWalkerProfileResponse>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(command, cancellationToken);
        return result.IsSuccess
            ? TypedResults.CreatedAtRoute(result.Value, "GetWalkerProfile", new { id = result.Value!.Id })
            : result.ToProblemDetails();
    }

    private static async Task<IResult> Get(
        IQueryHandler<GetWalkerProfileQuery, Result<WalkerProfileResponse>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(new GetWalkerProfileQuery(), cancellationToken);
        return result.IsSuccess ? TypedResults.Ok(result.Value) : result.ToProblemDetails();
    }
}
