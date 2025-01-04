using EntityLayer.DTOs;
using Microsoft.AspNetCore.Mvc.Filters;
using StocksApp.Controllers;
using StocksApp.Models;

namespace StocksApp.Filters.ActionFilters
{
	public class CreateBuyAndSellOrderActionFilter : IAsyncActionFilter
	{
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			if (context.Controller is TradeController controller)
			{
				var orderRequest = context.ActionArguments["order"] as IOrderRequest;

				if (orderRequest != null)
				{

					//update date of order
					orderRequest.DateAndTimeOfOrder = DateTime.Now;

					//re-validate the model object after updating the date
					controller.ModelState.Clear();
					controller.TryValidateModel(orderRequest);


					if (!controller.ModelState.IsValid)
					{
						controller.ViewBag.Errors = controller.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();



						StockTrade stockTrade = new StockTrade() { StockName = orderRequest.StockName, Quantity = orderRequest.Quantity, StockSymbol = orderRequest.StockSymbol };

						context.Result = controller.View(nameof(TradeController.Index), stockTrade); //short-circuits or skips the subsequent action filters & action method
					}
					else
					{
						await next();
					}
				}
				else
				{
					await next();
				}
			}
		}
	}
}
