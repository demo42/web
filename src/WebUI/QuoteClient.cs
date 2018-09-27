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

    public QuoteClient(ILogger<QuoteClient> logger, IConfiguration config)
    {
      _client = new HttpClient();
      _client.Timeout = TimeSpan.FromSeconds(5);
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
                throw;
            }
        }
    }
}