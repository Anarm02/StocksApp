using EntityLayer.DTOs;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Context;
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
		private readonly AppDbContext _appDbContext;

		public StocksService()
		{

		}
		public StocksService(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		
		public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? request)
		{
			if (request == null) throw new ArgumentNullException(nameof(request));
			ValidationHelper.ModelValidation(request);
			BuyOrder order = request.ToBuyOrder();
			order.Id = Guid.NewGuid();
			await _appDbContext.BuyOrders.AddAsync(order);
			await _appDbContext.SaveChangesAsync();
			return order.ToBuyOrderResponse();
		}

		public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? request)
		{
			if (request == null) throw new ArgumentNullException(nameof(request));
			ValidationHelper.ModelValidation(request);
			SellOrder order = request.ToSellOrder();
			order.Id = Guid.NewGuid();
			await _appDbContext.SellOrders.AddAsync(order);
			await _appDbContext.SaveChangesAsync();
			return order.ToSellOrderResponse();
		}

		public async Task<List<BuyOrderResponse>> ListBuyOrders()
		{
			var result =await _appDbContext.BuyOrders.Select(bo => bo.ToBuyOrderResponse()).ToListAsync();
			return result;
		}

		public async Task<List<SellOrderResponse> > ListSellOrders()
		{
			return await _appDbContext.SellOrders.Select(so => so.ToSellOrderResponse()).ToListAsync();
		}
	}
}
