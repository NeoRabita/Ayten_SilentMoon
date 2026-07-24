using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SlientMoon.Application.Behaviours.Transaction;
using System.Reflection;

namespace SlientMoon.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
        });

        return services;
    }
}