namespace Application.Features.Todos.Get;

using Application.Abstractions.Messaging;
using Domain.Common;

public sealed record GetTodoQuery(Guid Id) : IQuery<Result<TodoDetailResponse>>;

public sealed record TodoDetailResponse(Guid Id, string Title, string? Description, bool IsCompleted, DateTimeOffset? CompletedAt, DateTimeOffset CreatedAt);
