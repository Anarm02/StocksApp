﻿using EntityLayer.DTOs;

namespace StocksApp.Models
{
	public class Orders
	{
        public List<BuyOrderResponse> BuyOrders { get; set; }
        public List<SellOrderResponse> SellOrders { get; set; }
    }
}
