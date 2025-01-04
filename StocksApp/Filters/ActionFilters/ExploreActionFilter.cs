using Microsoft.AspNetCore.Mvc.Filters;
using StocksApp.Controllers;

namespace StocksApp.Filters.ActionFilters
{
	public class ExploreActionFilter : IActionFilter
	{
		public void OnActionExecuted(ActionExecutedContext context)
		{
			StocksController tradeController=(StocksController)context.Controller;
			IDictionary<string, object?>? parameters = (IDictionary<string, object?>?)context.HttpContext.Items["arguments"];
			if (parameters != null) {
				if (parameters.ContainsKey("stock"))
				{
					tradeController.ViewData["stock"] = Convert.ToString(parameters["stock"]);
				}
			}
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			context.HttpContext.Items["arguments"] = context.ActionArguments;
		}
	}
}
