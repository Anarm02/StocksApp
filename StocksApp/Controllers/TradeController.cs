using EntityLayer.DTOs;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using ServiceLayer.ServiceContracts;
using StocksApp.Filters.ActionFilters;
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
		[TypeFilter(typeof(CreateBuyAndSellOrderActionFilter))]
		public async Task<IActionResult> BuyOrder(BuyOrderRequest order)
		{
			var result = await _stocksService.CreateBuyOrder(order);
			return RedirectToAction("AllOrders");
		}
		[Route("trade/SellOrder")]
		[HttpPost]
		[TypeFilter(typeof(CreateBuyAndSellOrderActionFilter))]
		public async Task<IActionResult> SellOrder(SellOrderRequest order)
		{	
			var result = await _stocksService.CreateSellOrder(order);
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
		[HttpGet]
		[Route("[controller]/[action]/{id}")]
		public async Task<IActionResult> DeleteBuyOrder(Guid id)
		{
			await _stocksService.DeleteBuyOrder(id);
			return RedirectToAction("AllOrders");
		}
		[HttpGet]
		[Route("[controller]/[action]/{id}")]
		public async Task<IActionResult> DeleteSellOrder(Guid id)
		{
			await _stocksService.DeleteSellOrder(id);
			return RedirectToAction("AllOrders");
		}


	}

	}

