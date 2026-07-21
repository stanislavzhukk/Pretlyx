namespace Petlyx.Application.Abstractions.Messaging;

using Petlyx.Domain.Common;

public interface ICommand : ICommand<Result>;

public interface ICommand<TResponse>;
