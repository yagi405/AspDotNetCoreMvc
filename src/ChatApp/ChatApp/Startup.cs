using System.Data;
using System.Data.SqlClient;
using ChatApp.Models.Managers;
using ChatApp.Models.Managers.Imp;
using ChatApp.Models.Mappers;
using ChatApp.Models.Mappers.Imp;
using ChatApp.Models.Services;
using ChatApp.Models.Services.Imp;
using ChatApp.Resources;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChatApp
{
    public class Startup
    {
        private const string JaJpCulture = "ja-jp";

        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(SharedResource));
                });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Login");
                    options.SlidingExpiration = true;
                });

            services
                .AddScoped<IDbConnection>(
                    _ => new SqlConnection(Configuration.GetConnectionString("DefaultConnection"))
                )
                .AddScoped<IAuthService, AuthService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IChatLogService, ChatLogService>()
                .AddScoped<IChatMapper, ChatMapper>()
                .AddScoped<IUserMapper, UserMapper>()
                .AddScoped<IPasswordManager, PasswordManager>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var supportedCulture = new[] { JaJpCulture };
            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(supportedCulture[0])
                .AddSupportedCultures(supportedCulture)
                .AddSupportedUICultures(supportedCulture);

            app.UseRequestLocalization(localizationOptions);

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}"
                );
            });
        }
    }
}
