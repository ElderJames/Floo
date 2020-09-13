using Floo.Core.Shared;
using Floo.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Floo.Infrastructure
{
    public static class ServiceCollectionExtensins
    {
        public static IServiceCollection AddFlooEntityStorage<TDbContext>(this IServiceCollection services)
            where TDbContext : class, IDbContext
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IIdentityContext>(sp => new IdentityContext(sp.GetService<IHttpContextAccessor>()));
            services.AddScoped<IDbContext, TDbContext>();
            services.AddScoped(typeof(IEntityStorage<>), typeof(EfCoreEntityStorage<>));

            return services;
        }
    }
}