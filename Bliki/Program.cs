using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;


namespace Bliki
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                CreateHostBuilder(args).Build().Run();
            }
            else
            {
                CreateKestrelHostBuilder(args).Build().Run();
            }
        }


        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseStaticWebAssets()
                    .UseUrls("http://localhost:5000");
                });//.UseWindowsService();
        }


        public static IHostBuilder CreateKestrelHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        .UseStaticWebAssets()
                        .UseKestrel(options =>
                        {
                            options.Listen(IPAddress.Loopback, 5200);
                            options.Listen(IPAddress.Loopback, 5201);
                        });
                });
        }
    }
}