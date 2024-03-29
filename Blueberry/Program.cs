﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Ocelot.Middleware;
using Ocelot.DependencyInjection;

namespace Blueberry
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                          .UseUrls("http://0.0.0.0:80")
                          .UseStartup<Startup>()
                          .ConfigureAppConfiguration((hostingContext, config) =>
                           {
                              config.AddJsonFile("config.json")
                              .AddEnvironmentVariables();
                           })
                           .ConfigureServices(s =>
                           {
                              s.AddOcelot();
                           })
                           .Configure(app =>
                           {
                              app.UseOcelot().Wait();
                           })
                           .ConfigureLogging((hostingContext, logging) =>
                           {
                               //add your logging
                           })
                           .UseIISIntegration()
                           .Build();
        }
    }
}
