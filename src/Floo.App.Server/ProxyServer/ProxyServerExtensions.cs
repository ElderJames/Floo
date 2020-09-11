using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Floo.App.Server.ProxyServer;
using Floo.App.Server.ProxyServer.Internal;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ProxyServerExtensions
    {
        public static IServiceCollection AddProxyServer(this IServiceCollection service, Action<ProxyServerOptions> optionAction)
        {
            service.AddMvcCore().ConfigureProxyServer(optionAction);
            return service;
        }

        public static IMvcCoreBuilder ConfigureProxyServer(this IMvcCoreBuilder mvcBuilder, Action<ProxyServerOptions> optionAction)
        {
            var options = new ProxyServerOptions();
            optionAction(options);

            ConfigureInterfaces(mvcBuilder, Assembly.Load(options.AssemblyString).ExportedTypes.Where(options.Filter));

            return mvcBuilder;
        }

        private static void ConfigureInterfaces(this IMvcCoreBuilder mvcBuilder, IEnumerable<Type> serviceTypes)
        {
            foreach (var t in serviceTypes)
            {
                mvcBuilder.AddMvcOptions(opt =>
                {
                    opt.Conventions.Add(new ControllerModelConvention(t));
                    opt.Conventions.Add(new ActionModelConvention(t));
                    opt.Conventions.Add(new ParameterModelConvention(t));
                });
            }

            mvcBuilder.ConfigureApplicationPartManager(manager =>
            {
                var featureProvider = new ServiceControllerFeatureProvider(serviceTypes);

                manager.FeatureProviders.Remove(manager.FeatureProviders.FirstOrDefault(x => x.GetType() == typeof(ControllerFeatureProvider)));
                manager.FeatureProviders.Add(featureProvider);
            });
        }

        public static IMvcBuilder ConfigureProxyServer(this IMvcBuilder mvcBuilder, Action<ProxyServerOptions> optionAction)
        {
            var options = new ProxyServerOptions();
            optionAction(options);

            ConfigureInterfaces(mvcBuilder, Assembly.Load(options.AssemblyString).ExportedTypes.Where(options.Filter));

            return mvcBuilder;
        }

        private static void ConfigureInterfaces(this IMvcBuilder mvcBuilder, IEnumerable<Type> serviceTypes)
        {
            foreach (var t in serviceTypes)
            {
                mvcBuilder.AddMvcOptions(opt =>
                {
                    opt.Conventions.Add(new ControllerModelConvention(t));
                    opt.Conventions.Add(new ActionModelConvention(t));
                    opt.Conventions.Add(new ParameterModelConvention(t));
                });
            }

            mvcBuilder.ConfigureApplicationPartManager(manager =>
            {
                var featureProvider = new ServiceControllerFeatureProvider(serviceTypes);

                manager.FeatureProviders.Remove(manager.FeatureProviders.FirstOrDefault(x => x.GetType() == typeof(ControllerFeatureProvider)));
                manager.FeatureProviders.Add(featureProvider);
            });
        }
    }
}