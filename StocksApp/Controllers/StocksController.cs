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
		private readonly ILogger<StocksController> _logger;

		public StocksController(IFinnhubService finnhubService, IOptions<TradingOptions> options, ILogger<StocksController> logger)
		{
			_finnhubService = finnhubService;
			_options = options.Value;
			_logger = logger;
		}
		[Route("stocks")]
		[Route("[action]/{stock?}")]
		[Route("~/[action]/{stock?}")]
		public async Task<IActionResult> Explore(string? stock, bool showAll = false)
		{
			_logger.LogInformation("{Controller}.{Action}",nameof(StocksController),nameof(Explore));
			_logger.LogDebug("stock:{stock},showall:{showAll}",stock,showAll);
			List<Dictionary<string, string>>? stocksDictionary = await _finnhubService.GetStocks();

			List<Stock> stocks = new List<Stock>();

			if (stocksDictionary is not null)
			{
				//filter stocks
				if (!showAll && _options.Top25PopularStocks != null)
				{
					string[]? Top25PopularStocksList = _options.Top25PopularStocks.Split(",");
					if (Top25PopularStocksList is not null)
					{
						stocksDictionary = stocksDictionary
						 .Where(temp => Top25PopularStocksList.Contains(Convert.ToString(temp["symbol"])))
						 .ToList();
					}
				}

				//convert dictionary objects into Stock objects
				stocks = stocksDictionary
				 .Select(temp => new Stock() { StockName = Convert.ToString(temp["description"]), StockSymbol = Convert.ToString(temp["symbol"]) })
				.ToList();
			}

			ViewBag.stock = stock;
			return View(stocks);
		}
	}
}
