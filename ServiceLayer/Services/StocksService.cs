using EntityLayer.DTOs;
using EntityLayer.Entities;
using ServiceLayer.Helpers;
using ServiceLayer.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
	public class StocksService : IStocksService
	{
		private readonly List<BuyOrder> _buyOrders;
		private readonly List<SellOrder> _sellOrders;
        public StocksService()
        {
            _buyOrders = new List<BuyOrder>();
			_sellOrders = new List<SellOrder>();
        }
        public BuyOrderResponse CreateBuyOrder(BuyOrderRequest? request)
		{
			if (request == null) throw new ArgumentNullException(nameof(request));
			ValidationHelper.ModelValidation(request);
			BuyOrder order=request.ToBuyOrder();
			order.Id=Guid.NewGuid();
			_buyOrders.Add(order);
			return order.ToBuyOrderResponse();
		}

		public SellOrderResponse CreateSellOrder(SellOrderRequest? request)
		{
			if (request == null) throw new ArgumentNullException(nameof(request));
			ValidationHelper.ModelValidation(request);
			SellOrder order = request.ToSellOrder();
			order.Id = Guid.NewGuid();
			_sellOrders.Add(order);
			return order.ToSellOrderResponse();
		}

		public List<BuyOrderResponse> ListBuyOrders()
		{
			var result = _buyOrders.Select(bo => bo.ToBuyOrderResponse()).ToList();
			return result;
		}

		public List<SellOrderResponse> ListSellOrders()
		{
			return _sellOrders.Select(so=>so.ToSellOrderResponse()).ToList();
		}
	}
}
