using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using Microsoft.Extensions.Configuration;

namespace WebUI.Pages
{
    public class IndexModel : PageModel
    {
        public Quote quote;
        public string BackgroundColor {get;set;}
        public IQuoteClient Client { get; }
        public Version Version{ get; private set;}

        [FromForm]
        public string Data { get; set; }

        [TempData]
        public string Message { get; set; }

        private IConfiguration _config;

        public IndexModel(IQuoteClient client, IConfiguration config)
        {
            Client = client;
            _config = config;

            var envVersion = Environment.GetEnvironmentVariable("VERSION");
            if (envVersion != null){
                Version=new Version(envVersion.ToString());
            } else{
                Version=new Version("0.0.0");
            }

            BackgroundColor = Environment.GetEnvironmentVariable("BACKGROUND_COLOR");
        }

        public async Task OnGet()
        {
            quote = await Client.GetRandomQuote();
            Data = Words.GetWord();
        }

        #region Post Data

        public async Task<IActionResult> OnPost()
        {
            if(!Words.IsWordValid(Data))
            {
                Message = "Not saved. Nice try.";
                return RedirectToPage();
            }

            var storageAccount = CloudStorageAccount.Parse(_config["StorageConnectionString"]);

            // Create the queue client.
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a container.
            CloudQueue queue = queueClient.GetQueueReference(_config["QueueName"]);

            await queue.AddMessageAsync(new CloudQueueMessage(Data));

            Message = "Got it!, want to give us more data?";

            return RedirectToPage();
        }
        #endregion
    }
}
