using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebUI
{
    public class QuoteClient : IQuoteClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<QuoteClient> _logger;

    public QuoteClient(HttpClient client, ILogger<QuoteClient> logger, IConfiguration config)
    {
      _client = client;
      _client.BaseAddress = new Uri(config["QuotesUri"]);
      _logger = logger;
    }

    public async Task<Quote> GetRandomQuote()
        {
            try
            {
                var result = await _client.GetAsync("/quotes/rand");
                result.EnsureSuccessStatusCode();
                return await result.Content.ReadAsAsync<Quote>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format(
                     "Unable to retrieve quote. URI: {0}", _client.BaseAddress.ToString()));
               return new Quote
                {
                    id = 1,
                    text = "Everythings fine here, now. How are you?",
                    attribution = "Han Solo"
                };
            }
        }
    }
}