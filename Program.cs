using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FTCRegex
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

//      // Default BuildWebHost.
        // public static IWebHost BuildWebHost(string[] args) =>
        //     WebHost.CreateDefaultBuilder(args)
        //         .UseStartup<Startup>()
        //         .Build();
        public static IWebHost BuildWebHost(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", optional: false)  // add custom config file
                // .AddJsonFile("appsettings.json", optional: true)  // add custom config file
                .Build();

            return WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(config)  // include reference to config
                .UseStartup<Startup>()
                .Build();
        }
    }
}
