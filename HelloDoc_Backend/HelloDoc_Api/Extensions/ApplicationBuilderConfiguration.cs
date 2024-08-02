using System.Reflection;
using System.Text;
using HelloDoc_Api.ExtAuthorization;
using HelloDoc_BusinessAccessLayer.IServices;
using HelloDoc_BusinessAccessLayer.Profiles;
using HelloDoc_BusinessAccessLayer.Services;
using HelloDoc_Common.Constants;
using HelloDoc_DataAccessLayer.Data;
using HelloDoc_DataAccessLayer.IRepositories;
using HelloDoc_DataAccessLayer.Repositories;
using HelloDoc_Entities.DTOs.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
        // public static void ConfigureSwagger(this IServiceCollection services, IConfiguration config)
        // {
        //     services.AddSwaggerGen(c =>
        //     {
        //         c.SwaggerDoc("v1", new OpenApiInfo { Title = "HelloDoc", Version = "v1" });
        //         c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        //         {
        //             In = ParameterLocation.Header,
        //             Description = "Please enter token",
        //             Name = "Authorization",
        //             Type = SecuritySchemeType.Http,
        //             BearerFormat = "JWT",
        //             Scheme = "bearer"
        //         });

        //         c.AddSecurityRequirement(new OpenApiSecurityRequirement
        //         {
        //             {
        //                 new OpenApiSecurityScheme
        //                 {
        //                     Reference = new OpenApiReference
        //                     {
        //                         Type = ReferenceType.SecurityScheme,
        //                         Id = "Bearer"
        //                     }
        //                 },
        //                 new string[]{}
        //             }
        //         });

        //         services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //     .AddJwtBearer(options =>
        //     {
        //         options.TokenValidationParameters = new TokenValidationParameters
        //         {
        //             ValidateIssuer = true,
        //             ValidateAudience = true,
        //             ValidateLifetime = true,
        //             ValidateIssuerSigningKey = true,
        //             ValidIssuer = config["Jwt:Issuer"],
        //             ValidAudience = config["Jwt:Issuer"],
        //             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]))
        //         };
        //     });

        //         services.AddScoped<ExtAuthorizeFilter>();

        //         services.AddScoped<IAuthorizationHandler, ExtAuthorizeHandler>();

        //         services.AddAuthorization(config =>
        //         {
        //             config.AddPolicy(SystemConstants.ADMIN_POLICY, policy =>
        //             {
        //                 policy.Requirements.Add(new ExtAuthorizeRequirement(SystemConstants.ADMIN_POLICY));
        //             });
        //             config.AddPolicy(SystemConstants.PATIENT_POLICY, policy =>
        //             {
        //                 policy.Requirements.Add(new ExtAuthorizeRequirement(SystemConstants.PATIENT_POLICY));
        //             });
        //             config.AddPolicy(SystemConstants.PROVIDER_POLICY, policy =>
        //             {
        //                 policy.Requirements.Add(new ExtAuthorizeRequirement(SystemConstants.PROVIDER_POLICY));
        //             });
        //             config.AddPolicy(SystemConstants.ALL_USER_POLICY, policy =>
        //             {
        //                 policy.Requirements.Add(new ExtAuthorizeRequirement(SystemConstants.ALL_USER_POLICY));
        //             });
        //         });
        //     });
        // }
        public static void ConfigureSwagger(this IServiceCollection services, IConfiguration config)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "HelloDoc", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "Bearer Authentication with JWT Token",
                    Type = SecuritySchemeType.Http
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                            }
                        },
                    new List < string > ()
                    }
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]))
                };
            });

            services.AddScoped<ExtAuthorizeFilter>();

            services.AddScoped<IAuthorizationHandler, ExtAuthorizeHandler>();

            services.AddAuthorization(config =>
            {
                config.AddPolicy(SystemConstants.ADMIN_POLICY, policy =>
                {
                    policy.Requirements.Add(new ExtAuthorizeRequirement(SystemConstants.ADMIN_POLICY));
                });
                config.AddPolicy(SystemConstants.PATIENT_POLICY, policy =>
                {
                    policy.Requirements.Add(new ExtAuthorizeRequirement(SystemConstants.PATIENT_POLICY));
                });
                config.AddPolicy(SystemConstants.PROVIDER_POLICY, policy =>
                {
                    policy.Requirements.Add(new ExtAuthorizeRequirement(SystemConstants.PROVIDER_POLICY));
                });
                config.AddPolicy(SystemConstants.ALL_USER_POLICY, policy =>
                {
                    policy.Requirements.Add(new ExtAuthorizeRequirement(SystemConstants.ALL_USER_POLICY));
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