namespace Petlyx.Application.Features.Todos.Get;

using Petlyx.Application.Abstractions.Messaging;
using Petlyx.Domain.Common;

public sealed record GetTodoQuery(Guid Id) : IQuery<Result<TodoDetailResponse>>;

public sealed record TodoDetailResponse(Guid Id, string Title, string? Description, bool IsCompleted, DateTimeOffset? CompletedAt, DateTimeOffset CreatedAt);
