using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OnionArchitecture.Application.Interfaces.Messaging;

namespace SlientMoon.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<IDispatcher, Dispatcher>();
            services.AddCqrsHandlers(Assembly.GetExecutingAssembly());
        }
    }
}