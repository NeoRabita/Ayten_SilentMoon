using Application.Abstractions.Messaging;
using System.Threading.Tasks;
using System.Threading;
namespace SlientMoon.Application.Interfaces.Messaging;
public interface IDispatcher
{
   
    Task<Result> Send(IBaseCommand command, CancellationToken ct = default);

    Task<Result<TResult>> Send<TResult>(ICommand<TResult> command, CancellationToken ct = default);
    Task<Result<TResult>> Send<TResult>(INonTransactionalCommand<TResult> command, CancellationToken ct = default);

    Task<Result<TResult>> Send<TResult>(IQuery<TResult> query, CancellationToken ct = default);
}