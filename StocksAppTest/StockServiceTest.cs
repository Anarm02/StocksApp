using EntityLayer.DTOs;
using ServiceLayer.ServiceContracts;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAppTest
{
	public class StockServiceTest
	{
		private readonly IStocksService stocksService;
        public StockServiceTest()
        {
            stocksService = new StocksService();
        }
		#region CreateBuyOrder
		[Fact]
		public void CreateBuyOrder_NullArgument()
		{
			BuyOrderRequest request = null;
			 Assert.Throws<ArgumentNullException>( () =>
			{
				 stocksService.CreateBuyOrder(request);
			});
		}
		[Fact]
		public void CreateBuyOrder_InvalidQuantity()
		{
			BuyOrderRequest request=new BuyOrderRequest() { 
			Quantity=0,
			Price=100,
			DateAndTimeOfOrder= DateTime.Parse("2001-01-01"),
			StockName="something",
			StockSymbol="msft"
			};
			Assert.Throws<ArgumentException>( () =>
			{
				stocksService.CreateBuyOrder(request);
			});
		}
		[Fact]
		public void CreateBuyOrder_InvalidQuantity2()
		{
			BuyOrderRequest request = new BuyOrderRequest()
			{
				Quantity = 100001,
				Price = 100,
				DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
				StockName = "something",
				StockSymbol = "msft"
			};
			Assert.Throws<ArgumentException>( () =>
			{
				stocksService.CreateBuyOrder(request);
			});
		}
		[Fact]
		public void CreateBuyOrder_InvalidPrice()
		{
			BuyOrderRequest request = new BuyOrderRequest()
			{
				Quantity = 1000,
				Price = 0,
				DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
				StockName = "something",
				StockSymbol = "msft"
			};
			Assert.Throws<ArgumentException>(	 () =>
			{
				stocksService.CreateBuyOrder(request);
			});
		}
		[Fact]
		public void CreateBuyOrder_InvalidPrice2()
		{
			BuyOrderRequest request = new BuyOrderRequest()
			{
				Quantity = 1000,
				Price = 10001,
				DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
				StockName = "something",
				StockSymbol = "msft"
			};
			Assert.Throws<ArgumentException>( () =>
			{
				 stocksService.CreateBuyOrder(request);
			});
		}
		[Fact]
		public void CreateBuyOrder_NullSymbole()
		{
			BuyOrderRequest request = new BuyOrderRequest()
			{
				Quantity = 1000,
				Price = 1001,
				DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
				StockName = "something",
				StockSymbol = null
			};
			Assert.Throws<ArgumentException>( () =>
			{
				 stocksService.CreateBuyOrder(request);
			});
		}
		[Fact]
		public void CreateBuyOrder_InvalidDate()
		{
			BuyOrderRequest request = new BuyOrderRequest()
			{
				Quantity = 1000,
				Price = 1001,
				DateAndTimeOfOrder = DateTime.Parse("1991-01-01"),
				StockName = "something",
				StockSymbol = "msft"
			};
			Assert.Throws<ArgumentException>( () =>
			{
				 stocksService.CreateBuyOrder(request);
			});
		}
		[Fact]
		public void CreateBuyOrder_Proper()
		{
			BuyOrderRequest request = new BuyOrderRequest()
			{
				Quantity = 1000,
				Price = 1001,
				DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
				StockName = "something",
				StockSymbol = "msft"
			};
			BuyOrderResponse response = stocksService.CreateBuyOrder(request);
			List<BuyOrderResponse> responses =  stocksService.ListBuyOrders();
			Assert.True(response.Id!=Guid.Empty);
			Assert.Contains(response,responses);
		}
		#endregion
		#region CreateSellOrder
		[Fact]
		public void CreateSellOrder_NullArgument()
		{
			SellOrderRequest request = null;
			Assert.Throws<ArgumentNullException>(() =>
			{
				stocksService.CreateSellOrder(request);
			});
		}
		[Fact]
		public void CreateSellOrder_InvalidQuantity()
		{
			SellOrderRequest request = new SellOrderRequest()
			{
				Quantity = 0,
				Price = 100,
				DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
				StockName = "something",
				StockSymbol = "msft"
			};
			Assert.Throws<ArgumentException>(() =>
			{
				stocksService.CreateSellOrder(request);
			});
		}
		[Fact]
		public void CreateSellOrder_InvalidQuantity2()
		{
			SellOrderRequest request = new SellOrderRequest()
			{
				Quantity = 100001,
				Price = 100,
				DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
				StockName = "something",
				StockSymbol = "msft"
			};
			Assert.Throws<ArgumentException>(() =>
			{
				stocksService.CreateSellOrder(request);
			});
		}
		[Fact]
		public void CreateSellOrder_InvalidPrice()
		{
			SellOrderRequest request = new SellOrderRequest()
			{
				Quantity = 1000,
				Price = 0,
				DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
				StockName = "something",
				StockSymbol = "msft"
			};
			Assert.Throws<ArgumentException>(() =>
			{
				stocksService.CreateSellOrder(request);
			});
		}
		[Fact]
		public void CreateSellOrder_InvalidPrice2()
		{
			SellOrderRequest request = new SellOrderRequest()
			{
				Quantity = 1000,
				Price = 10001,
				DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
				StockName = "something",
				StockSymbol = "msft"
			};
			Assert.Throws<ArgumentException>(() =>
			{
				stocksService.CreateSellOrder(request);
			});
		}
		[Fact]
		public void CreateSellOrder_NullSymbole()
		{
			SellOrderRequest request = new SellOrderRequest()
			{
				Quantity = 1000,
				Price = 1001,
				DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
				StockName = "something",
				StockSymbol = null
			};
			Assert.Throws<ArgumentException>(() =>
			{
				stocksService.CreateSellOrder(request);
			});
		}
		[Fact]
		public void CreateSellOrder_InvalidDate()
		{
			SellOrderRequest request = new SellOrderRequest()
			{
				Quantity = 1000,
				Price = 1001,
				DateAndTimeOfOrder = DateTime.Parse("1991-01-01"),
				StockName = "something",
				StockSymbol = "msft"
			};
			Assert.Throws<ArgumentException>(() =>
			{
				stocksService.CreateSellOrder(request);
			});
		}
		[Fact]
		public void CreateSellOrder_Proper()
		{
			SellOrderRequest request = new SellOrderRequest()
			{
				Quantity = 1000,
				Price = 1001,
				DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
				StockName = "something",
				StockSymbol = "msft"
			};
			SellOrderResponse response = stocksService.CreateSellOrder(request);
			List<SellOrderResponse> responses = stocksService.ListSellOrders();
			Assert.True(response.Id != Guid.Empty);
			Assert.Contains(response, responses);
		}
		#endregion
		#region GetBuyOrders
		[Fact]
		public void GetBuyOrders_EmptyList()
		{
			List<BuyOrderResponse> responses = stocksService.ListBuyOrders();
			Assert.Empty(responses);
		}
		[Fact]
		public void GetBuyOrders_Proper()
		{
			BuyOrderRequest request = new BuyOrderRequest()
			{
				Quantity = 1000,
				Price = 1001,
				DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
				StockName = "something",
				StockSymbol = "msft"
			};
			BuyOrderRequest request1 = new BuyOrderRequest()
			{
				Quantity = 1000,
				Price = 1001,
				DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
				StockName = "sometehing",
				StockSymbol = "mst"
			};
			BuyOrderRequest request2 = new BuyOrderRequest()
			{
				Quantity = 1000,
				Price = 1001,
				DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
				StockName = "somethingi",
				StockSymbol = "msi"
			};
			List<BuyOrderRequest> requests=new List<BuyOrderRequest>() {request,request1,request2 };
			List<BuyOrderResponse> buyOrders = new();
			foreach (var req in requests) {
			buyOrders.Add(stocksService.CreateBuyOrder(req));
			}
			List<BuyOrderResponse> responses = stocksService.ListBuyOrders();
			foreach (var response in responses) {
			Assert.Contains(response,buyOrders);
			}
		}
		#endregion
		#region GetSellOrders
		[Fact]
		public void GetSellOrders_EmptyList()
		{
			List<SellOrderResponse> responses = stocksService.ListSellOrders();
			Assert.Empty(responses);
		}
		[Fact]
		public void GetSellOrders_Proper()
		{
			SellOrderRequest request = new SellOrderRequest()
			{
				Quantity = 1000,
				Price = 1001,
				DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
				StockName = "something",
				StockSymbol = "msft"
			};
			SellOrderRequest request1 = new SellOrderRequest()
			{
				Quantity = 1000,
				Price = 1001,
				DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
				StockName = "sometehing",
				StockSymbol = "mst"
			};
			SellOrderRequest request2 = new SellOrderRequest()
			{
				Quantity = 1000,
				Price = 1001,
				DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
				StockName = "somethingi",
				StockSymbol = "msi"
			};
			List<SellOrderRequest> requests = new List<SellOrderRequest>() { request, request1, request2 };
			List<SellOrderResponse> sellOrders = new();
			foreach (var req in requests)
			{
				sellOrders.Add(stocksService.CreateSellOrder(req));
			}
			List<SellOrderResponse> responses = stocksService.ListSellOrders();
			foreach (var response in responses)
			{
				Assert.Contains(response, sellOrders);
			}
		}
		#endregion
	}
}
