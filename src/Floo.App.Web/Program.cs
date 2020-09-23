using Floo.App.Server;
using Floo.App.Shared;
using Floo.Core.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Floo.App.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient("Floo.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<AccessTokenAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Floo.ServerAPI"));

            builder.Services.AddApiAuthorization();
            builder.Services.AddAntDesign();
            builder.Services.AddScoped<IIdentityContext, IdentityContext>();

            builder.Services.TryAddTransient<AccessTokenAuthorizationMessageHandler>();

            builder.Services.AddProxyClient(options =>
             {
                 options.ClientName = "Floo.ServerAPI";
                 options.RequestHost = builder.HostEnvironment.BaseAddress;
                 options.AssemblyString = new[] { typeof(IWeatherForecastService).Assembly.FullName };
             });


            await builder.Build().RunAsync();
        }
    }
}