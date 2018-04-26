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

        public IndexModel(QuoteClient client)
        {
            Client = client;
        }

        public async Task OnGet()
        {
            quote = await Client.GetRandomQuote();
        }
    }
}
