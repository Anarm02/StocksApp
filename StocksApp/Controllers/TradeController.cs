using EntityLayer.DTOs;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
		public TradeController(IFinnhubService finnhubService, IOptions<TradingOptions> tradingOptions, IStocksService stocksService)
		{
			_finnhubService = finnhubService;
			_tradingOptions = tradingOptions;
			_stocksService = stocksService;
		}

		[Route("/")]
		[Route("trade/Index")]
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
		[Route("trade/BuyOrder")]
		[HttpPost]
		public IActionResult BuyOrder(BuyOrderRequest buyOrder)
		{
			buyOrder.DateAndTimeOfOrder = DateTime.Now;
			ModelState.Clear();
			TryValidateModel(buyOrder);
			if (!ModelState.IsValid)
			{
				ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
				StockTrade stockTrade = new StockTrade() { StockName = buyOrder.StockName, Quantity = buyOrder.Quantity, StockSymbol = buyOrder.StockSymbol };
				return View("Index",stockTrade);
			}
			
			var result=_stocksService.CreateBuyOrder(buyOrder);
			return RedirectToAction("AllOrders");
		}
		[Route("trade/SellOrder")]
		[HttpPost]
		public IActionResult SellOrder(SellOrderRequest sellOrder)
		{
			sellOrder.DateAndTimeOfOrder = DateTime.Now;
			ModelState.Clear();
			TryValidateModel(sellOrder);
			if (!ModelState.IsValid)
			{
				ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
				StockTrade stockTrade = new StockTrade() { StockName = sellOrder.StockName, Quantity = sellOrder.Quantity, StockSymbol = sellOrder.StockSymbol };
				return View("index",stockTrade);
			}

			var result=_stocksService.CreateSellOrder(sellOrder);
			return RedirectToAction("AllOrders");
		}
		[Route("trade/AllOrders")]
		public IActionResult  AllOrders()
		{
			var buyOrders=_stocksService.ListBuyOrders();
			var sellOrders=_stocksService.ListSellOrders();
			Orders orders = new Orders() {
			BuyOrders = buyOrders,
			SellOrders = sellOrders
			};
			return View(orders);
		}
	}
}
