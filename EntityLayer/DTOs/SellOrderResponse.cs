using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs
{
	public class SellOrderResponse
	{
		public Guid Id { get; set; }
		[Required(ErrorMessage = "Stock symbol can't be empty")]
		public string StockSymbol { get; set; }
		[Required(ErrorMessage = "Stock name can't be empty")]
		public string StockName { get; set; }
		public DateTime DateAndTimeOfOrder { get; set; }
		[Range(1, 100000, ErrorMessage = "Value should be between 1 and 1000")]
		public uint Quantity { get; set; }
		[Range(1, 10000, ErrorMessage = "Value should be between 1 and 1000")]
		public double Price { get; set; }
		public double TradeAmount { get; set; }
		public override bool Equals(object? obj)
		{
			if (obj == null) return false;
			if (obj is not SellOrderResponse) return false;

			SellOrderResponse other = (SellOrderResponse)obj;
			return Id == other.Id && StockSymbol == other.StockSymbol && StockName == other.StockName && DateAndTimeOfOrder == other.DateAndTimeOfOrder && Quantity == other.Quantity && Price == other.Price;
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
	public static class SellOrderExtension
	{
		public static SellOrderResponse ToSellOrderResponse(this SellOrder order)
		{
			return new SellOrderResponse()
			{
				Id = order.Id,
				StockSymbol = order.StockSymbol,
				DateAndTimeOfOrder = order.DateAndTimeOfOrder,
				Price = order.Price,
				Quantity = order.Quantity,
				StockName = order.StockName,
				TradeAmount = order.Price * order.Quantity
			};
		}
	}
}
