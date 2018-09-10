using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebUI{
    public class Program{
        public static IWebHostBuilder CreateWebHostBuilder(string[] args){

            var builder = WebHost.CreateDefaultBuilder(args);
            // Add secrets configuration via a files added through 
            // kubernetes secrets
            // just need the path in kube that weill contain the secrets
            var configPath = Environment.GetEnvironmentVariable("ConfigPath");
            if (!string.IsNullOrEmpty(configPath)){
                builder.ConfigureAppConfiguration(
                    config => config.AddKeyPerFile(configPath, true));
            }
            builder.UseStartup<Startup>();
            return builder;
        }
        public static void Main(string[] args){
            CreateWebHostBuilder(args).Build().Run();
        }
    }
}
