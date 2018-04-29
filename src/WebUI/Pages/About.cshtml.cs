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

        public void OnGet()
        {
            Message = "Debugging Info.";
            RegistryIp = System.Net.Dns.GetHostAddresses("jengademos.azurecr.io")[0].ToString();
            HostName = Environment.GetEnvironmentVariable("COMPUTERNAME") ??
                                            Environment.GetEnvironmentVariable("HOSTNAME");
            OsArchitecture = RuntimeInformation.OSArchitecture.ToString();
            OsDescription = RuntimeInformation.OSDescription;
            ProcessArchitecture = RuntimeInformation.ProcessArchitecture.ToString();
            FrameworkDescription = RuntimeInformation.FrameworkDescription;
            AspNetCorePackageVersion  = Environment.GetEnvironmentVariable("ASPNETCORE_PKG_VERSION");
            AspNetCoreEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            StringBuilder envVars = new StringBuilder();
            foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
                envVars.Append(string.Format("<strong>{0}</strong>:{1}<br \\>", de.Key, de.Value));

            EnvironmentVariables = envVars.ToString();
        }
    }
}

