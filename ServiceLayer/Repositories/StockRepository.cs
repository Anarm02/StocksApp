﻿using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Context;
using ServiceLayer.RepositoyContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repositories
{
	public class StockRepository : IStockRepository
	{
		private readonly AppDbContext _appDbContext;

		public StockRepository(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public async Task<BuyOrder> CreateBuyOrder(BuyOrder buyOrder)
		{
			await _appDbContext.BuyOrders.AddAsync(buyOrder);
			await _appDbContext.SaveChangesAsync();
			return buyOrder;
		}
		
		public async Task<SellOrder> CreateSellOrder(SellOrder sellOrder)
		{
			await _appDbContext.SellOrders.AddAsync(sellOrder);
			await _appDbContext.SaveChangesAsync();
			return sellOrder;
		}
		public async Task<bool> DeleteBuyOrder(Guid id)
		{
			var order=await _appDbContext.BuyOrders.FirstOrDefaultAsync(x => x.Id==id);
			_appDbContext.BuyOrders.Remove(order);
			int count=await _appDbContext.SaveChangesAsync();
			return count > 0;
		}
		public async Task<List<BuyOrder>> GetAllBuyOrders()
		{
			var orders = await _appDbContext.BuyOrders.ToListAsync();
			return orders;
		}
		public async Task<bool> DeleteSellOrder(Guid id)
		{
			var order = await _appDbContext.SellOrders.FirstOrDefaultAsync(x => x.Id == id);
			_appDbContext.SellOrders.Remove(order);
			int count = await _appDbContext.SaveChangesAsync();
			return count > 0;
		}
		public async Task<List<SellOrder>> GetAllSellOrders()
		{
			var orders = await _appDbContext.SellOrders.ToListAsync();
			return orders;
		}
	}
}
