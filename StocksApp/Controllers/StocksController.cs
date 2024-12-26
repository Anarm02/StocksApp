using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StocksApp.Models;
using StocksApp.ServiceContracts;

namespace StocksApp.Controllers
{
	[Route("[controller]")]
	public class StocksController : Controller
	{
		private readonly IFinnhubService _finnhubService;
		private readonly TradingOptions  _options;

		public StocksController(IFinnhubService finnhubService, IOptions<TradingOptions> options)
		{
			_finnhubService = finnhubService;
			_options = options.Value;
		}
		[Route("stocks")]
		[Route("[action]/{stock?}")]
		[Route("~/[action]/{stock?}")]
		public async Task<IActionResult> Explore(string? stock, bool showAll = false)
		{
			List<Dictionary<string,string>>? stocks=await _finnhubService.GetStocks();
			List<Stock> newStocks = new List<Stock>();
			if(stocks != null)
			{
				if(!showAll && _options.Top25PopularStocks != null)
				{
					string[]? Top25PopularStocksList = _options.Top25PopularStocks.Split(",");
					if (Top25PopularStocksList is not null)
					{
						stocks = stocks
						 .Where(temp => Top25PopularStocksList.Contains(Convert.ToString(temp["symbol"])))
						 .ToList();
					}
				}
				newStocks = stocks
	.Select(temp => new Stock() { StockName = Convert.ToString(temp["description"]), StockSymbol = Convert.ToString(temp["symbol"]) })
   .ToList();
			}
			ViewBag.Stock = stock;
			return View(newStocks);
		}
	}
}
