using Floo.Core.Shared;
using Floo.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Floo.Infrastructure
{
    public static class ServiceCollectionExtensins
    {
        public static IServiceCollection AddFlooEntityStorage<TDbContext>(this IServiceCollection services)
            where TDbContext : class, IDbContext
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IIdentityContext, IdentityContext>();
            services.AddScoped<IDbContext, TDbContext>();
            services.AddScoped(typeof(IEntityStorage<>), typeof(EfCoreEntityStorage<>));

            return services;
        }
    }
}