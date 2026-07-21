namespace Petlyx.Application.Features.Todos.Complete;

using Petlyx.Application.Abstractions.Messaging;
using Petlyx.Domain.Common;

public sealed record CompleteTodoCommand(Guid Id) : ICommand;
