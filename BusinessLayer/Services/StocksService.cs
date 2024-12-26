using EntityLayer.DTOs;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Context;
using ServiceLayer.Helpers;
using ServiceLayer.RepositoyContracts;
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
		private readonly IStockRepository _stockRepository;

		public StocksService(IStockRepository stockRepository)
		{
			_stockRepository = stockRepository;
		}

		public StocksService()
		{

		}
		

		
		public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? request)
		{
			if (request == null) throw new ArgumentNullException(nameof(request));
			ValidationHelper.ModelValidation(request);
			BuyOrder order = request.ToBuyOrder();
			order.Id = Guid.NewGuid();
			await _stockRepository.CreateBuyOrder(order);
			return order.ToBuyOrderResponse();
		}

		public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? request)
		{
			if (request == null) throw new ArgumentNullException(nameof(request));
			ValidationHelper.ModelValidation(request);
			SellOrder order = request.ToSellOrder();
			order.Id = Guid.NewGuid();
			await _stockRepository.CreateSellOrder(order);
			return order.ToSellOrderResponse();
		}

		public async Task<List<BuyOrderResponse>> ListBuyOrders()
		{
			var result =await _stockRepository.GetAllBuyOrders();
			return result.Select(bo => bo.ToBuyOrderResponse()).ToList();
		}

		public async Task<List<SellOrderResponse> > ListSellOrders()
		{
			var result = await _stockRepository.GetAllSellOrders();
			return result.Select(bo => bo.ToSellOrderResponse()).ToList();
		}
	}
}
