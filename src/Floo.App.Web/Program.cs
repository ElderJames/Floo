using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Floo.App.Shared;

namespace Floo.App.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient("Floo.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Floo.ServerAPI"));

            builder.Services.AddApiAuthorization();

            builder.Services.AddProxyClient(options =>
            {
                options.RequestHost = builder.HostEnvironment.BaseAddress;
                options.AssemblyString = new[] { typeof(IWeatherForecastService).Assembly.FullName };
            });

            await builder.Build().RunAsync();
        }
    }
}