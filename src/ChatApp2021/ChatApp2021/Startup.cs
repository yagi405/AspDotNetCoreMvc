using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data;
using ChatApp2021.Infrastructure.Repositories;
using ChatApp2021.Infrastructure.Repositories.Imp;
using ChatApp2021.Models.Services;
using ChatApp2021.Models.Services.Imp;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ChatApp2021
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services
                .AddScoped<IDbConnection>(
                    _ => new SqlConnection(Configuration.GetConnectionString("DefaultConnection"))
                    )
                .AddScoped<IChatLogRepository, ChatLogRepository>()
                .AddScoped<IChatService, ChatService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Chat}/{action=Index}/{id?}"
                );
            });
        }
    }
}
