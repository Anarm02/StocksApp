﻿namespace StocksApp.Models
{
	public class Stock
	{
		public string? StockName { get; set; }
		public string? StockSymbol { get; set; }
		public override bool Equals(object? obj)
		{
			if (obj == null) return false;
			if (obj is not Stock) return false;

			Stock other = (Stock)obj;
			return StockSymbol == other.StockSymbol && StockName == other.StockName;
		}

	}
}
