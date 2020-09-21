﻿using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Floo.Core.Shared.HttpProxy;
using Microsoft.AspNetCore.Http;
using Polly;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IHttpClientBuilder AddProxyClient(this IServiceCollection services, Action<ProxyClientOptions> optionAction)
        {
            var options = new ProxyClientOptions();
            optionAction(options);

            var httpClientBuilder = services.AddHttpClient(options.RequestHost, client => { client.BaseAddress = new Uri(options.RequestHost); });

            httpClientBuilder.AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(options.RetrySleepDurations));

            foreach (var type in options.AssemblyString.SelectMany(x => Assembly.Load(x).ExportedTypes).Where(options.Filter))
                services.AddSingleton(type, sp =>
                {
                    var factory = sp.GetService<IHttpClientFactory>();
                    var httpContextAccessor = sp.GetService<IHttpContextAccessor>();
                    if (httpContextAccessor == null)
                        return HttpClientProxyGenerator.Create(type, factory.CreateClient(options.ClientName?? options.RequestHost));
                    else
                        return HttpClientProxyGenerator.Create(type, factory.CreateClient(options.ClientName ?? options.RequestHost), httpContextAccessor);
                });

            return httpClientBuilder;
        }
    }
}