using EntityLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceContracts
{
	public interface IStocksService
	{
		BuyOrderResponse CreateBuyOrder(BuyOrderRequest? request);
		SellOrderResponse CreateSellOrder(SellOrderRequest? request);
		List<BuyOrderResponse> ListBuyOrders();
		List<SellOrderResponse> ListSellOrders();
	}
}
