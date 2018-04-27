using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebUI
{
    public class QuoteClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<QuoteClient> _logger;

        public QuoteClient(HttpClient client, ILogger<QuoteClient> logger)
        {
            _client = client;
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
            catch (System.Exception ex)
            {
                _logger.LogError(ex, _client.BaseAddress.ToString());
                _logger.LogError(ex, "Unable to retrieve quote.");
                return null;
            }
        }
    }
}