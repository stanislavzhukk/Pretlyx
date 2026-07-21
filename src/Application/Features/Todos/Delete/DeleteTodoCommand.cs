namespace Petlyx.Application.Features.Todos.Delete;

using Petlyx.Application.Abstractions.Messaging;
using Petlyx.Domain.Common;

public sealed record DeleteTodoCommand(Guid Id) : ICommand;
