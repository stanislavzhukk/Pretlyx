using System.Numerics;
using Api.Extensions;
using Application.Abstractions.Messaging;
using Application.Features.Profiles.OwnerProfiles.Create;
using Application.Features.Profiles.OwnerProfiles.Get;
using Application.Features.Todos.Create;
using Domain.Common;
using Microsoft.AspNetCore.Http.HttpResults;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Api.Endpoints;

public static class OwnerProfileEndpoints
{
    public static void MapOwnerProfileEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/owner-profiles")
            .WithTags("OwnerProfiles")
            .RequireAuthorization();

        group.MapPost("/", Create)
            .AddEndpointFilter<ValidationFilter<CreateOwnerProfileCommand>>()
            .WithName("CreateOwnerProfile")
            .WithSummary("Create a new owner profile");

        group.MapGet("/", Get)
            .WithName("GetOwnerProfile")
            .WithSummary("Get the current user's Owner profile");
    }

    private static async Task<IResult> Create(
        CreateOwnerProfileCommand command,
        ICommandHandler<CreateOwnerProfileCommand, Result<CreateOwnerProfileResponse>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(command, cancellationToken);
        return result.IsSuccess
            ? TypedResults.CreatedAtRoute(result.Value, "GetOwnerProfile")
            : result.ToProblemDetails();
    }

    private static async Task<IResult> Get(
        IQueryHandler<GetOwnerProfileQuery, Result<OwnerProfileResponse>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(new GetOwnerProfileQuery(), cancellationToken);
        return result.IsSuccess ? TypedResults.Ok(result.Value) : result.ToProblemDetails();
    }
}
