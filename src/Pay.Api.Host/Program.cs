using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Pay.Api.Host
{
    using Host = Microsoft.Extensions.Hosting.Host;
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseKestrel();
                    webBuilder.ConfigureKestrel((context, options) =>
                    {
                        options.ListenAnyIP(9000);
                    });
                });
    }
}
