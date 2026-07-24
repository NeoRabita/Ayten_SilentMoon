using MediatR;

namespace Application.Abstractions.Messaging;

public interface IBaseCommand { }
public interface IBaseNonTransactionalCommand : IBaseCommand { }

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{
}

public interface ICommand : IRequest<Result>, IBaseCommand
{
}

public interface INonTransactionalCommand<TResponse> : IRequest<Result<TResponse>>, IBaseNonTransactionalCommand
{
}

public interface INonTransactionalCommand : IRequest<Result>, IBaseNonTransactionalCommand
{
}