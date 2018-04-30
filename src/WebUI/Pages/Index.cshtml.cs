using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages
{
    public class IndexModel : PageModel
    {
        public Quote quote;
        public QuoteClient Client { get; }
        public Version Version{ get; private set;}

        public IndexModel(QuoteClient client)
        {
            Client = client;
            var envVersion = new Version(Environment.GetEnvironmentVariable("VERSION"));
            if (envVersion != null){
                Version=new Version(envVersion.ToString());
            } else{
                Version=new Version("0.0.0");
            }
        }

        public async Task OnGet()
        {
            quote = await Client.GetRandomQuote();
        }
    }
}
