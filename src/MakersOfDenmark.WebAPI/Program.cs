using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.ElmahIo;
using Serilog.Exceptions;
namespace MakersOfDenmark.WebAPI
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = BuildLoggerConfiguration("MakersOfDenmark.WebAPI").CreateLogger();

            try
            {
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (System.Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        public static LoggerConfiguration BuildLoggerConfiguration(string appName)
        {
            var configuration = new LoggerConfiguration().MinimumLevel.Information();

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                configuration
                    .Enrich.FromLogContext()
                    .Enrich.WithExceptionDetails()
                    .WriteTo.ElmahIo(new ElmahIoSinkOptions(Environment.GetEnvironmentVariable("ELMAHIO_API_KEY"), new Guid(Environment.GetEnvironmentVariable("ELMAHIO_LOGID"))));
            }
            else
            {
                configuration
                    .Enrich.FromLogContext()
                    .Enrich.WithExceptionDetails()
                    .WriteTo.Console()
                    .WriteTo.File($"{appName}-log.txt");
            }
            return configuration;
        }
    }
}
