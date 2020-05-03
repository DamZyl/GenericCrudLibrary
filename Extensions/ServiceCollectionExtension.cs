using GenericCrud.Databases;
using GenericCrud.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GenericCrud.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
            => services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        public static IServiceCollection AddDbContextInterface<TImpl>(this IServiceCollection services)
            => services.AddScoped(typeof(IApplicationDbContext), typeof(TImpl));
    }
}