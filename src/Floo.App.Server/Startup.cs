using Floo.App.Shared;
using Floo.App.Shared.Cms.Articles;
using Floo.App.Shared.Cms.Comments;
using Floo.App.Shared.Cms.Contents;
using Floo.Core.Entities.Cms.Articles;
using Floo.Core.Entities.Cms.Contents;
using Floo.Core.Entities.Identity;
using Floo.Core.Entities.Identity.Users;
using Floo.Core.Shared;
using Floo.Infrastructure;
using Floo.Infrastructure.Identity;
using Floo.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Text.Json;

namespace Floo.App.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            //services.AddServerSideBlazor();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Floo.App.Server", Version = "v1" });
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    option => option.MigrationsAssembly(GetType().Assembly.FullName)))
                .AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton<IDbAdapter>(sp => new DapperDbAdapter(() => new SqlConnection(Configuration.GetConnectionString("DefaultConnection"))));

            services.AddDefaultIdentity<User>(options => Configuration.GetSection("Identity").Bind(options))
                .AddRoles<Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                   .AddApiAuthorization<User, ApplicationDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddScoped<IUserClaimsPrincipalFactory<User>, AdditionalUserClaimsPrincipalFactory>();

            services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
            services.AddScoped<SignOutSessionStateManager>();

            services.AddFlooEntityStorage<ApplicationDbContext>();

            services.AddAntDesign();

            services.AddProxyServer(options =>
            {
                options.AssemblyString = typeof(IWeatherForecastService).Assembly.FullName;
            });

            services.Configure<JsonOptions>(options => options.JsonSerializerOptions.IgnoreNullValues = true);

            services.AddFlooCore();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                app.UseWebAssemblyDebugging();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Floo Server v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                //endpoints.MapBlazorHub();
                endpoints.MapFallbackToFile("/index.html");
            });
        }
    }
}