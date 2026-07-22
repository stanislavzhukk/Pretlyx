namespace Application.Features.Todos.Create;

using Application.Abstractions.Messaging;
using Domain.Common;

public sealed record CreateTodoCommand(string Title, string? Description) : ICommand<Result<CreateTodoResponse>>;

public sealed record CreateTodoResponse(Guid Id, string Title, string? Description);
