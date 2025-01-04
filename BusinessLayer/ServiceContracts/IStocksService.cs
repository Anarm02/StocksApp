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
		Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? request);
		Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? request);
		Task<List<BuyOrderResponse>> ListBuyOrders();
		Task<List<SellOrderResponse>> ListSellOrders();
		Task<bool> DeleteBuyOrder(Guid? id);
		Task<bool> DeleteSellOrder(Guid? id);
	}
}
