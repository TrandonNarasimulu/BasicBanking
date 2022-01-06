using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading.Tasks;

namespace BasicBanking.API
{
    public static class Program
    {
        public async static Task Main(string[] args)
        {
            string date = DateTime.Now.ToString("yyyyMMdd_hhmmss");
            string filepath = "C:\\Logs\\Service_" + date + ".txt";
            Log.Logger = new LoggerConfiguration()
                   .Enrich.FromLogContext()
                   .WriteTo.Console()
                   .WriteTo.File(filepath)
                   .MinimumLevel.Information()
                   .CreateLogger();

            try
            {
                Log.Information("Starting up");
                await CreateHostBuilder(args).Build().RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
                throw ex;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseWindowsService()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseHttpSys(options =>
                    {
                        options.Authentication.Schemes = AuthenticationSchemes.None;
                    });

                    webBuilder.ConfigureKestrel(serverOptions =>
                    {
                        serverOptions.Limits.MaxConcurrentConnections = null;
                        serverOptions.Limits.MinRequestBodyDataRate = null;
                        serverOptions.Limits.MinResponseDataRate = null;
                    }).UseStartup<Startup>();
                });
    }
}
