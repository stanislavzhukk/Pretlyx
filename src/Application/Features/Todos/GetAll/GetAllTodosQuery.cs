namespace Application.Features.Todos.GetAll;

using Application.Abstractions.Messaging;
using Application.Features.Todos.Get;
using Domain.Common;

public sealed record GetAllTodosQuery(int Page = 1, int PageSize = 10) : IQuery<Result<PagedResult<TodoDetailResponse>>>;
