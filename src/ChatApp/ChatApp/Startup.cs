using System.Data;
using System.Data.SqlClient;
using ChatApp.Infrastructure.Logging;
using ChatApp.Infrastructure.Persistence.Repositories;
using ChatApp.Infrastructure.Persistence.Repositories.Imp;
using ChatApp.Models;
using ChatApp.Models.Services;
using ChatApp.Models.Services.Imp;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            services.AddControllersWithViews(options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    options.Filters.Add(new LoggingAttribute());
                })
            .AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                    factory.Create(typeof(SharedResource));
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Login");
                    options.AccessDeniedPath = new PathString("/App/AccessDenied");
                    options.SlidingExpiration = true;
                });

            services
                .AddScoped<IDbConnection>(
                    _ => new SqlConnection(Configuration.GetConnectionString("DefaultConnection"))
                )
                .AddScoped<IChatLogRepository, ChatLogRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IAccountService, AccountService>()
                .AddScoped<IAppService, AppService>()
                .AddScoped<IChatService, ChatService>()
                .AddScoped<ILoginService, LoginService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IAuthenticator, Authenticator>()
                .AddScoped<IPasswordManager, PasswordManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/App/Error");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/App/Error/{0}");

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
                    "default",
                    "{controller=Login}/{action=Index}/{id?}"
                );
            });
        }
    }
}
