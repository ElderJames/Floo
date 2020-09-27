using Floo.Core;
using Floo.Core.Shared;
using Floo.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using System.Linq;

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

        public static IServiceCollection AddRepositories(this IServiceCollection services,IConfiguration configuration)
        {
            var assemblie = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(u => u.GetName().Name.Equals("Floo.Core"));
            if (assemblie == null)
                return services;
            services.AddTransient<IDbConnection>((sp) => new SqlConnection(configuration.GetConnectionString("DefaultConnection")));
            var repositoryInterfaces = assemblie.GetTypes().Where(i => i.IsInterface).Where(i => i.GetInterfaces().Any(u => u.Name.Contains("IQueryBaseRepository")));
            foreach (var @interface in repositoryInterfaces)
            {
                var implementationTypes = assemblie.GetTypes().Where(t => t.GetInterfaces().Contains(@interface)).ToList();
                if (implementationTypes == null)
                {
                    continue;
                }
                if (implementationTypes.Count != 1)
                {
                    continue;
                }
                services.Add(new ServiceDescriptor(@interface, implementationTypes.FirstOrDefault(), ServiceLifetime.Transient));
            }
            return services;
        }
    }
}