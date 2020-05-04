using System.Text;
using GenericCrud.Auth;
using GenericCrud.Databases;
using GenericCrud.Options;
using GenericCrud.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace GenericCrud.Extensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection AddSqlConfiguration(this IServiceCollection services, IConfiguration configuration, string section) 
            => services.Configure<SqlOption>(x => configuration.GetSection(section).Bind(x));
        
        public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration, string section)
            => services.Configure<JwtOption>(x => configuration.GetSection(section).Bind(x));

        public static AuthenticationBuilder AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration, string section)
        {
            var jwtOption = new JwtOption();
            var jwtSection = configuration.GetSection(section);
            jwtSection.Bind(jwtOption);
            
            return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.SecretKey)),
                        ValidIssuer = jwtOption.Issuer,
                        ValidateAudience = false,
                        ValidateLifetime = jwtOption.ValidateLifetime
                    };
                });
        }
        
        public static IServiceCollection AddJwt(this IServiceCollection services)
            => services.AddScoped<IJwtHandler, JwtHandler>();
        
        public static IServiceCollection AddHasher(this IServiceCollection services)
            => services.AddScoped<IPasswordHasher, PasswordHasher>();

        public static IServiceCollection AddRepository(this IServiceCollection services)
            => services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        public static IServiceCollection AddDb<TImpl>(this IServiceCollection services)
            => services.AddScoped(typeof(IApplicationDbContext), typeof(TImpl));

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My API",
                    Version = "v1"
                });
            });
        }
    }
}