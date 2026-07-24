using System;
using System.Linq;
using System.Reflection;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SlientMoon.Application.Interfaces.Caching;
using SlientMoon.Application.Interfaces.Logging;
using SlientMoon.Application.Interfaces.Repositories;
using SlientMoon.Application.Interfaces.Services;
using SlientMoon.Infrastructure.Persistence.Caching;
using SlientMoon.Infrastructure.Persistence.Contexts;
using SlientMoon.Infrastructure.Persistence.Dapper;
using SlientMoon.Infrastructure.Persistence.Logging;
using SlientMoon.Infrastructure.Persistence.Repositories;
using SlientMoon.Infrastructure.Persistence.Services;
using SlientMoon.Infrastructure.Persistence.Settings;

namespace SlientMoon.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITopicRepository, TopicRepository>();

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
            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IOtpSender, OtpSender>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IDateTimeService, DateTimeService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IDapper, DapperClass>();
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
            var domainAssembly = Assembly.Load("SlientMoon.Domain");

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
