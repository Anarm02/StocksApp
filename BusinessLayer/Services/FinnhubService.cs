using Microsoft.Extensions.Configuration;
using StocksApp.ServiceContracts;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.RepositoyContracts;
namespace StocksApp.Services
{
	public class FinnhubService : IFinnhubService
	{
		private readonly IFinnhubRepository _finnhubRepository;

		public FinnhubService(IFinnhubRepository finnhubRepository)
		{
			_finnhubRepository = finnhubRepository;
		}

		public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
		{
			return await _finnhubRepository.GetCompanyProfile(stockSymbol);
		}

		public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
		{
			return await _finnhubRepository.GetStockPriceQuote(stockSymbol);
		}

		public Task<List<Dictionary<string, string>>?> GetStocks()
		{
			return _finnhubRepository.GetStocks();
		}

		public Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
		{
			return _finnhubRepository.SearchStocks(stockSymbolToSearch);
		}
	}
}
