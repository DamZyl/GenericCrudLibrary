using System.Linq;
using System.Reflection;
using System.Text;
using GenericCrud.Auth;
using GenericCrud.Databases;
using GenericCrud.Mappers;
using GenericCrud.Options;
using GenericCrud.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace GenericCrud.Extensions
{
    public static class ConfigureServicesExtension
    {
        public static void AddSqlConfiguration(this IServiceCollection services, IConfiguration configuration, string section) 
            => services.Configure<SqlOption>(x => configuration.GetSection(section).Bind(x));
        
        public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration, string section)
        {
            services.Configure<JwtOption>(x => configuration.GetSection(section).Bind(x));
            
            var jwtOption = new JwtOption();
            var jwtSection = configuration.GetSection(section);
            jwtSection.Bind(jwtOption);
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
        
        public static void AddJwt(this IServiceCollection services)
            => services.AddScoped<IJwtHandler, JwtHandler>();
        
        public static void AddHasher(this IServiceCollection services)
            => services.AddScoped<IPasswordHasher, PasswordHasher>();

        public static void AddRepository(this IServiceCollection services)
            => services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        public static void AddUnitOfWork(this IServiceCollection services)
            => services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        public static void AddMapper(this IServiceCollection services, Assembly assembly)
        {
            foreach (var type in assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract))
            {
                foreach (var i in type.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapper<,>)))
                {

                    var interfaceType = typeof(IMapper<,>).MakeGenericType(i.GetGenericArguments());
                    services.Add(new ServiceDescriptor(interfaceType, type, ServiceLifetime.Scoped));
                }
            }
        }
        
        public static void AddMapperWithParams(this IServiceCollection services, Assembly assembly)
        {
            foreach (var type in assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract))
            {
                foreach (var i in type.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapperWithParams<,>)))
                {

                    var interfaceType = typeof(IMapperWithParams<,>).MakeGenericType(i.GetGenericArguments());
                    services.Add(new ServiceDescriptor(interfaceType, type, ServiceLifetime.Scoped));
                }
            }
        }

        public static void AddDatabase<TDb>(this IServiceCollection services) where TDb : DbContext
        {
            services.AddDbContext<TDb>();
            services.AddScoped(typeof(IApplicationDbContext), typeof(TDb));
        }
        
        public static void AddDbInitializer<TDbInit>(this IServiceCollection services)
            => services.AddScoped(typeof(TDbInit));

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(x =>
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