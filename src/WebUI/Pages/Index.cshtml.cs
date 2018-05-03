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
        public QuoteClient Client { get; }
        public Version Version{ get; private set;}

        [FromForm]
        public string Data { get; set; }

        private IConfiguration _config;

        public IndexModel(QuoteClient client, IConfiguration config)
        {
            Client = client;
            _config = config;

            var envVersion = Environment.GetEnvironmentVariable("VERSION");
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

        public async Task OnPost()
        {
            var storageAccount = CloudStorageAccount.Parse(_config["StorageConnectionString"]);

            // Create the queue client.
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a container.
            CloudQueue queue = queueClient.GetQueueReference(_config["QueueName"]);

            await queue.AddMessageAsync(new CloudQueueMessage(Data));
        }
    }
}
