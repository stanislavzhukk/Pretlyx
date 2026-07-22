namespace Application.Features.Todos.Delete;

using Application.Abstractions.Messaging;
using Domain.Common;

public sealed record DeleteTodoCommand(Guid Id) : ICommand;
