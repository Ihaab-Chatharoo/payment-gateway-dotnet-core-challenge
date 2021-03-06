using System;
using App.Metrics;
using App.Metrics.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Checkout.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildHost(args).Run();
        }

        public static IHost BuildHost(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureMetricsWithDefaults(
                    builder =>
                    {
                        builder.Report.ToConsole(TimeSpan.FromSeconds(2));
                        builder.Report.ToTextFile(@"C:\Users\ihaab\Desktop\metrics.txt", TimeSpan.FromSeconds(20));
                    })
                .UseMetrics()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .Build();
        }
    }
}
