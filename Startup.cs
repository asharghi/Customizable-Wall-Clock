using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CustomizableWallClock.VueCoreConnection;

namespace CustomizableWallClock
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            // connect vue app - middleware
            services.AddSpaStaticFiles(options => options.RootPath = "client-app/dist");
            services.AddSignalR().AddAzureSignalR("Endpoint=https://wallclock.service.signalr.net;AccessKey=yC4qyCK7f6WC12s7juQ/uOvcLOGrMnoh1w6kRrAEMH8=;Version=1.0;");
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAzureSignalR(routes => {
                routes.MapHub<ClockMaker>("/clockmaker");
            });

            // use middleware and launch server for Vue
            app.UseSpaStaticFiles();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "client-app";
                if (env.IsDevelopment())
                {

                    spa.UseVueDevelopmentServer();
                }
            });
        }
    }
}
