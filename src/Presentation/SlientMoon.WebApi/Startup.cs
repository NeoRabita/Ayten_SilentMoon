using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SlientMoon.Application;
using SlientMoon.Application.Interfaces.Messaging;
using SlientMoon.Infrastructure.Persistence;
using SlientMoon.Infrastructure.Persistence.Services;
using SlientMoon.Infrastructure.Persistence.Settings;
using SlientMoon.Infrastructure.RabbitMqMessaging;
using SlientMoon.Infrastructure.RabbitMqMessaging.Consumers;
using SlientMoon.Infrastructure.RabbitMqMessaging.Settings;
using SlientMoon.WebApi.Extensions;
using SlientMoon.WebApi.Middleware;
using System;
using System.Text;
using Microsoft.Extensions.Options;
using Minio;
using SlientMoon.Application.Interfaces.Services;

namespace SlientMoon.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.DisableDefaultApiValidation();
            services.AddControllers();
            services.AddHttpContextAccessor();

            services.AddApplicationLayer();
            services.AddApplicationServices();
            services.AddPersistenceRegistration(Configuration);
            services.AddPersistenceApiServices(Configuration);
            services.Configure<MinioSettings>(
                Configuration.GetSection("MinioSettings"));

            services.AddSingleton<IMinioClient>(sp =>
            {
                var settings = sp
                    .GetRequiredService<IOptions<MinioSettings>>()
                    .Value;

                return new MinioClient()
                    .WithEndpoint(settings.Endpoint)
                    .WithCredentials(
                        settings.AccessKey,
                        settings.SecretKey)
                    .WithSSL(settings.UseSSL)
                    .Build();
            });

            services.AddScoped<IFileStorageService, MinioStorageService>();


            services.AddScoped<IMessagePublisher, RabbitMqPublisher>();
            services.Configure<RabbitMqSettings>(
                Configuration.GetSection("RabbitMqSettings"));

            services.AddHostedService<RabbitMqConsumer>();

            services.AddSwaggerExtension();
            services.AddLocalization();
            services.AddServiceExtension();
            services.EnableApiVersioning();

            var jwtSettings = Configuration
                .GetSection("JWTSettings")
                .Get<JWTSettings>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Key)),

                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseLocalization();

            //app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseErrorHandling();

            app.UseAuthentication();

           app.UseMiddleware<CurrentUserMiddleware>();

            app.UseAuthorization();

            app.UseSwaggerExtension(env, provider);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
