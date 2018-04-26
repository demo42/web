using System.Net.Http;
using System.Threading.Tasks;

namespace WebUI
{
    public class QuoteClient
    {
        private readonly HttpClient _client;

        public QuoteClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<Quote> GetRandomQuote()
        {
            var result = await _client.GetAsync("/quotes/rand");
            result.EnsureSuccessStatusCode();
            return await result.Content.ReadAsAsync<Quote>();
        }
    }
}