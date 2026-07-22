namespace Application.Features.Todos.Complete;

using Application.Abstractions.Messaging;
using Domain.Common;

public sealed record CompleteTodoCommand(Guid Id) : ICommand;
