using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.RepositoyContracts
{
	public interface IStockRepository
	{
		Task<BuyOrder> CreateBuyOrder(BuyOrder buyOrder);	
		Task<SellOrder> CreateSellOrder(SellOrder sellOrder);
		Task<List<BuyOrder>> GetAllBuyOrders();
		Task<List<SellOrder>> GetAllSellOrders();
	}
}
