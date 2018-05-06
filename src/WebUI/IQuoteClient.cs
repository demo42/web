using System.Threading.Tasks;

namespace WebUI
{
    public interface IQuoteClient
    {
        Task<Quote> GetRandomQuote();
    }
}