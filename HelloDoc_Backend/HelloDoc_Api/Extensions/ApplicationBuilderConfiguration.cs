using System.Reflection;
using HelloDoc_BusinessAccessLayer.IServices;
using HelloDoc_BusinessAccessLayer.Profiles;
using HelloDoc_BusinessAccessLayer.Services;
using HelloDoc_Common.Constants;
using HelloDoc_DataAccessLayer.Data;
using HelloDoc_DataAccessLayer.IRepositories;
using HelloDoc_DataAccessLayer.Repositories;
using HelloDoc_Entities.DTOs.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace HelloDoc_Api.Extensions
{
    public static class ApplicationBuilderConfiguration
    {
        public static void RegisterDatabaseConnection(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(config.GetConnectionString(SystemConstants.DEFAULT_CONNECTION));
            });
        }

        public static void RegisterUnitOfWork(this IServiceCollection services) => services.AddScoped<IUnitOfWork, UnitOfWork>();

        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
        }

        public static void SetRequestBodySize(this IServiceCollection services)
        {
            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = int.MaxValue;
            });
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpClient("customClient", client =>
            {
                // Configure HttpClient properties as needed
            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                // Set the ServerCertificateCustomValidationCallback to always return true
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            });

            IEnumerable<Type> implementationTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IBaseService<>)));

            foreach (Type implementationType in implementationTypes)
            {
                Type[] implementedInterfaces = implementationType.GetInterfaces();
                foreach (Type implementedInterface in implementedInterfaces)
                {
                    if (implementedInterface.IsGenericType)
                    {
                        Type openGenericInterface = implementedInterface.GetGenericTypeDefinition();
                        if (openGenericInterface == typeof(IBaseService<>))
                        {
                            services.AddScoped(implementedInterface, implementationType);
                        }
                    }
                }
            }

            services.Scan(scan =>
            {
                scan.FromAssembliesOf(typeof(IBaseService<>))
                    .AddClasses()
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
            });

            services.AddScoped<IMailService, MailService>();

            services.AddHttpContextAccessor();
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(SystemConstants.CORS_POLICY,
                    builder => builder.WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                );
            });
        }
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HelloDoc", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
        }

        public static void RegisterMail(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<MailSettingsDTO>(config.GetSection("MailSettings"));
            services.AddScoped<IMailService, MailService>();
        }
    }
}