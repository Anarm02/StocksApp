using EntityLayer.DTOs;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using ServiceLayer.ServiceContracts;
using StocksApp.Models;
using StocksApp.ServiceContracts;
using StocksApp.Services;

namespace StocksApp.Controllers
{
	public class TradeController : Controller
	{
		private readonly IFinnhubService _finnhubService;
		private readonly IStocksService _stocksService;
		private readonly IOptions<TradingOptions> _tradingOptions;
		private readonly ILogger<TradeController> _logger;
		public TradeController(IFinnhubService finnhubService, IOptions<TradingOptions> tradingOptions, IStocksService stocksService, ILogger<TradeController> logger)
		{
			_finnhubService = finnhubService;
			_tradingOptions = tradingOptions;
			_stocksService = stocksService;
			_logger = logger;
		}

		[Route("/")]
		[Route("trade/Index")]
		[Route("trade/[action]/{stockSymbole}")]
		public async Task<IActionResult> Index(string stocksymbole)
		{
			_logger.LogInformation("In {Controller}.{Action}",nameof(TradeController),nameof(Index));
			_logger.LogDebug("stocksymbole:{stocksymbole}",stocksymbole);
			if (stocksymbole == null)
				stocksymbole = "MSFT";
			Dictionary<string, object> company = await _finnhubService.GetCompanyProfile(stocksymbole);
			Dictionary<string, object> data = await _finnhubService.GetStockPriceQuote(stocksymbole);
			StockTrade stock = new StockTrade
			{
				StockSymbol = stocksymbole,
			};
			if (company != null && data != null)
			{
				stock = new StockTrade { StockSymbol = company["ticker"].ToString(), StockName = company["name"].ToString(), Price = Convert.ToDouble(data["c"].ToString()) };
			}

			return View(stock);
		}
		[Route("trade/BuyOrder")]
		[HttpPost]
		public async Task<IActionResult> BuyOrder(BuyOrderRequest buyOrder)
		{
			buyOrder.DateAndTimeOfOrder = DateTime.Now;
			ModelState.Clear();
			TryValidateModel(buyOrder);
			if (!ModelState.IsValid)
			{
				ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
				StockTrade stockTrade = new StockTrade() { StockName = buyOrder.StockName, Quantity = buyOrder.Quantity, StockSymbol = buyOrder.StockSymbol };
				return View("Index", stockTrade);
			}

			var result = await _stocksService.CreateBuyOrder(buyOrder);
			return RedirectToAction("AllOrders");
		}
		[Route("trade/SellOrder")]
		[HttpPost]
		public async Task<IActionResult> SellOrder(SellOrderRequest sellOrder)
		{
			sellOrder.DateAndTimeOfOrder = DateTime.Now;
			ModelState.Clear();
			TryValidateModel(sellOrder);
			if (!ModelState.IsValid)
			{
				ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
				StockTrade stockTrade = new StockTrade() { StockName = sellOrder.StockName, Quantity = sellOrder.Quantity, StockSymbol = sellOrder.StockSymbol };
				return View("index", stockTrade);
			}

			var result = await _stocksService.CreateSellOrder(sellOrder);
			return RedirectToAction("AllOrders");
		}
		[Route("trade/AllOrders")]
		public async Task<IActionResult> AllOrders()
		{
			var buyOrders = await _stocksService.ListBuyOrders();
			var sellOrders = await _stocksService.ListSellOrders();
			Orders orders = new Orders()
			{
				BuyOrders = buyOrders,
				SellOrders = sellOrders
			};
			return View(orders);
		}
		[Route("trade/GetPdf")]
		public async Task<IActionResult> GetPdf()
		{
			var buyorders = await _stocksService.ListBuyOrders();
			var sellOrders = await _stocksService.ListSellOrders();
			Orders orders = new Orders()
			{
				BuyOrders = buyorders,
				SellOrders = sellOrders
			};
			return new ViewAsPdf("GetPdf", orders, ViewData)
			{
				PageMargins = new Margins() { Bottom = 20, Left = 20, Right = 20, Top = 20 },
				PageOrientation = Orientation.Landscape,
			};
			}
	}
}
