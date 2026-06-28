using System;
using System.Linq;
using System.Reflection;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnionArchitecture.Application.Interfaces.Caching;
using OnionArchitecture.Application.Interfaces.Logging;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Application.Interfaces.Services;
using OnionArchitecture.Infrastructure.Persistence.Caching;
using OnionArchitecture.Infrastructure.Persistence.Contexts;
using OnionArchitecture.Infrastructure.Persistence.Dapper;
using OnionArchitecture.Infrastructure.Persistence.Logging;
using OnionArchitecture.Infrastructure.Persistence.Repositories;
using OnionArchitecture.Infrastructure.Persistence.Services;
using OnionArchitecture.Infrastructure.Persistence.Settings;

namespace OnionArchitecture.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseOracle(configuration["APIAppSettings:ConnectionString"],
            b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            services.AddStackExchangeRedisCache(options => {
                options.Configuration = configuration["APIAppSettings:Redis"];
                options.InstanceName = Assembly.GetEntryAssembly()?.GetName().Name + "_";
            });

            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerManager<>));
            services.AddScoped<ICacheService, RedisCacheService>();
            services.Configure<APIAppSettings>(configuration.GetSection("APIAppSettings"));
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IDateTimeService, DateTimeService>();
            services.AddScoped<IDapper, DapperClass>();
            services.AddScoped<IPomodoroRepository, PomodoroRepository>();
            services.AddScoped<IUow, Uow>();
            RegisterDapperDomainMappings();
        }
        #region APIRepositories
        public static void AddPersistenceApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
        #endregion


        private static void RegisterDapperDomainMappings()
        {
            var domainAssembly = Assembly.Load("OnionArchitecture.Domain");

            var entityTypes = domainAssembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Namespace != null && t.Namespace.EndsWith("Entities"));

            foreach (var type in entityTypes)
            {
                var mapperType = typeof(ColumnAttributeTypeMapper<>).MakeGenericType(type);
                var mapper = (SqlMapper.ITypeMap)Activator.CreateInstance(mapperType);

                SqlMapper.SetTypeMap(type, mapper);
            }
        }
    }
}
