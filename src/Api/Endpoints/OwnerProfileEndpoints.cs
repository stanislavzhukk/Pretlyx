using System.Numerics;
using Api.Extensions;
using Application.Abstractions.Messaging;
using Application.Features.Profiles.OwnerProfiles.Create;
using Application.Features.Profiles.OwnerProfiles.Get;
using Application.Features.Profiles.OwnerProfiles.Update;
using Application.Features.Todos.Create;
using Domain.Common;
using Microsoft.AspNetCore.Http.HttpResults;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Api.Endpoints;

public static class OwnerProfileEndpoints
{
    public static void MapOwnerProfileEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/profiles/owner")
            .WithTags("OwnerProfiles")
            .RequireAuthorization();

        group.MapPost("/", Create)
            .AddEndpointFilter<ValidationFilter<CreateOwnerProfileCommand>>()
            .WithName("CreateOwnerProfile")
            .WithSummary("Create a new owner profile");

        group.MapGet("/me", Get)
            .WithName("GetOwnerProfile")
            .WithSummary("Get the current user's Owner profile");

        group.MapPost("/upload-avatar", UploadAvatar)
            .WithName("UploadAvatar")
            .WithSummary("Upload an avatar for the current user's Owner profile");

        group.MapPatch("/update-profile", UpdateProfile)
            .WithName("UpdateOwnerProfile")
            .WithSummary("Update the current user's Owner profile");
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

    private static async Task<IResult> UploadAvatar(
        IQueryHandler<GetOwnerProfileQuery, Result<OwnerProfileResponse>> handler,
        CancellationToken cancellationToken)
    {
        // Implementation for uploading avatar
        return TypedResults.Ok();
    }

    private static async Task<IResult> UpdateProfile(
        UpdateOwnerProfileCommand command,
        ICommandHandler<UpdateOwnerProfileCommand, Result<UpdateOwnerProfileResponse>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(command, cancellationToken);
        return result.IsSuccess ? TypedResults.Ok(result.Value) : result.ToProblemDetails();
    }
}
