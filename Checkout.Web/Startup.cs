using System;
using App.Metrics;
using App.Metrics.Filtering;
using App.Metrics.Formatters.Json;
using Checkout.Ioc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Checkout.Web
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
            //services.Configure(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            services.ConfigureIOC(); //  Register IOC

            var filter = new MetricsFilter().WhereType(MetricType.Timer);
            var metrics = new MetricsBuilder()
                .Report.ToTextFile(
                    options =>
                    {
                        options.MetricsOutputFormatter = new MetricsJsonOutputFormatter();
                        options.AppendMetricsToTextFile = true;
                        options.Filter = filter;
                        options.FlushInterval = TimeSpan.FromSeconds(20);
                        options.OutputPathAndFileName = @"C:\Users\ihaab\Desktop\metrics.txt";
                    })
                .Build();

            services.AddMetrics(metrics);
            services.AddMetricsReportingHostedService();

            services.AddMvc().AddMetrics().SetCompatibilityVersion(CompatibilityVersion.Latest);
            //services.AddControllersWithViews();
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
                app.UseExceptionHandler(" / Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Users}/{action=Index}/{id?}");
            });
        }
    }
}
