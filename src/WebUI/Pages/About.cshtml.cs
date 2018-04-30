using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;
namespace WebUI.Pages
{
    public class AboutModel : PageModel
    {
        public string Message { get; set; }
        public string RegistryIp {get; set;}
        public string HostName {get;set;}
        public string OsArchitecture {get;set;}
        public string OsDescription {get;set;}
        public string ProcessArchitecture {get;set;}
        public string FrameworkDescription {get;set;}
        public string AspNetCorePackageVersion {get;set;}
        public string AspNetCoreEnvironment {get;set;}
        public string EnvironmentVariables {get;set;}
        public string ImageBuildDate{ get; private set;}
        public string BaseImageVersion { get; private set;}
        public string RegistryUrl {get; set;}

        public void OnGet()
        {
            try
            {
                Message = "Debugging Info.";
                var path = Environment.GetEnvironmentVariable("REGISTRY_NAME");
                RegistryUrl = path.Replace("/", "");
                RegistryIp = System.Net.Dns.GetHostAddresses(RegistryUrl)[0].ToString();
                HostName = Environment.GetEnvironmentVariable("COMPUTERNAME") ??
                                                Environment.GetEnvironmentVariable("HOSTNAME");
                OsArchitecture = RuntimeInformation.OSArchitecture.ToString();
                OsDescription = RuntimeInformation.OSDescription;
                ProcessArchitecture = RuntimeInformation.ProcessArchitecture.ToString();
                FrameworkDescription = RuntimeInformation.FrameworkDescription;
                AspNetCorePackageVersion  = Environment.GetEnvironmentVariable("ASPNETCORE_PKG_VERSION");
                AspNetCoreEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                ImageBuildDate = Environment.GetEnvironmentVariable("IMAGE_BUILD_DATE");
                BaseImageVersion = Environment.GetEnvironmentVariable("BASE_IMAGE_VERSION");
            }
            catch (System.Exception ex)
            {
                
                Message=ex.ToString();
            }
            StringBuilder envVars = new StringBuilder();
            foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
                envVars.Append(string.Format("<strong>{0}</strong>:{1}<br \\>", de.Key, de.Value));

            EnvironmentVariables = envVars.ToString();
        }
    }
}

