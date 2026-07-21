namespace Petlyx.Application.Features.Todos.GetAll;

using Petlyx.Application.Abstractions.Messaging;
using Petlyx.Application.Features.Todos.Get;
using Petlyx.Domain.Common;

public sealed record GetAllTodosQuery(int Page = 1, int PageSize = 10) : IQuery<Result<PagedResult<TodoDetailResponse>>>;
