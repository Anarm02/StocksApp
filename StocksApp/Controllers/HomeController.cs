using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StocksApp.Models;
using StocksApp.ServiceContracts;
using StocksApp.Services;

namespace StocksApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly IFinnhubService _finnhubService;
		private readonly IOptions<TradingOptions> _tradingOptions;
		public HomeController(IFinnhubService finnhubService, IOptions<TradingOptions> tradingOptions)
		{
			_finnhubService = finnhubService;
			_tradingOptions = tradingOptions;
		}

		[Route("/")]
		public async Task<IActionResult> Index()
		{
			if (_tradingOptions.Value.DefaultSymbol == null)
				_tradingOptions.Value.DefaultSymbol = "MSFT";
			Dictionary<string, object> company = await _finnhubService.GetCompanyProfile(_tradingOptions.Value.DefaultSymbol);
			Dictionary<string, object> data = await _finnhubService.GetStockPriceQuote(_tradingOptions.Value.DefaultSymbol);
			StockTrade stock = new StockTrade
			{
				StockSymbol = _tradingOptions.Value.DefaultSymbol,
			};
			if(company != null && data!=null) {
				stock = new StockTrade { StockSymbol = company["ticker"].ToString(), StockName = company["name"].ToString(), Price = Convert.ToDouble(data["c"].ToString()) };
			}

			return View(stock);
		}
	}
}
