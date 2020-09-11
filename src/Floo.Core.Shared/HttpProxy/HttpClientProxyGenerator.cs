using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Floo.Core.Shared.DynamicProxy;
using Floo.Core.Shared.Utils;
using Microsoft.AspNetCore.Http;

namespace Floo.Core.Shared.HttpProxy
{
    public static class HttpClientProxyGenerator
    {
        private static readonly ConcurrentCache<string, HttpClient> clientCache = new ConcurrentCache<string, HttpClient>();

        public static T Create<T>(string apiUrl)
        {
            var client = clientCache.GetOrAdd(apiUrl, (url) => new HttpClient()
            {
                BaseAddress = new Uri(url)
            });
            return ProxyGenerator.Create<T>(new HttpClientProxy(client));
        }

        public static T Create<T>(HttpClient httpClient)
        {
            return ProxyGenerator.Create<T>(new HttpClientProxy(httpClient));
        }

        public static object Create(Type type, HttpClient httpClient)
        {
            return ProxyGenerator.Create(type, new HttpClientProxy(httpClient));
        }

        public static object Create(Type type, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            return ProxyGenerator.Create(type, new HttpClientProxy(httpClient, httpContextAccessor));
        }

        public static Dictionary<Type, object> CreateAll(string apiUrl, Action<HttpClientProxyOption> optionAction)
        {
            var option = new HttpClientProxyOption();
            optionAction(option);

            return option.InterfaceTypes.ToDictionary(x => x, x =>
            {
                var client = clientCache.GetOrAdd(apiUrl, (url) => new HttpClient()
                {
                    BaseAddress = new Uri(url)
                });
                return ProxyGenerator.Create(x, new HttpClientProxy(client));
            });
        }
    }

    public class HttpClientProxyOption
    {
        internal IList<Type> InterfaceTypes { get; }

        public HttpClientProxyOption()
        {
            InterfaceTypes = new List<Type>();
        }

        public void AddService<T>()
        {
            if (InterfaceTypes.Contains(typeof(T)))
                return;

            InterfaceTypes.Add(typeof(T));
        }
    }
}