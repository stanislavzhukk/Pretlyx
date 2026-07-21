namespace Petlyx.Application.Features.Todos.Create;

using Petlyx.Application.Abstractions.Messaging;
using Petlyx.Domain.Common;

public sealed record CreateTodoCommand(string Title, string? Description) : ICommand<Result<CreateTodoResponse>>;

public sealed record CreateTodoResponse(Guid Id, string Title, string? Description);
