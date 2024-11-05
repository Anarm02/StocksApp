using EntityLayer.Entities;
using EntityLayer.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs
{
	public class SellOrderRequest
	{
		[Required(ErrorMessage = "Stock symbol can't be empty")]
		public string StockSymbol { get; set; }
		[Required(ErrorMessage = "Stock name can't be empty")]
		public string StockName { get; set; }
		[DateAfter("2000-01-01")]
		public DateTime DateAndTimeOfOrder { get; set; }
		[Range(1, 100000, ErrorMessage = "Value should be between 1 and 1000")]
		public uint Quantity { get; set; }
		[Range(1, 10000, ErrorMessage = "Value should be between 1 and 1000")]
		public double Price { get; set; }
		public SellOrder ToSellOrder()
		{
			return new SellOrder()
			{
				Price = Price,
				Quantity = Quantity,
				StockName = StockName,
				StockSymbol = StockSymbol,
				DateAndTimeOfOrder = DateAndTimeOfOrder

			};
		}
	}
}
