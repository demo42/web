using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var builder = WebHost.CreateDefaultBuilder(args);
            var secretsDir = "/etc/quotes-secrets";
            if(!System.IO.Directory.Exists(secretsDir))
            {
                throw new InvalidOperationException("No secret volume mounted");
            }
            builder.ConfigureAppConfiguration(config => config.AddKeyPerFile(secretsDir, false));
            builder.UseStartup<Startup>();
            return builder;
        }
    }
}
