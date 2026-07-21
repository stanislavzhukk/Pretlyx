namespace Petlyx.Application.Features.Todos.Update;

using Petlyx.Application.Abstractions.Messaging;
using Petlyx.Domain.Common;

public sealed record UpdateTodoCommand(Guid Id, string Title, string? Description) : ICommand;
