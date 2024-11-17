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
		public async Task CreateBuyOrder_NullArgument()
		{
			BuyOrderRequest request = null;
			 await Assert.ThrowsAsync<ArgumentNullException>( async () =>
			{
				 await stocksService.CreateBuyOrder(request);
			});
		}
		[Fact]
		public async Task CreateBuyOrder_InvalidQuantity()
		{
			BuyOrderRequest request=new BuyOrderRequest() { 
			Quantity=0,
			Price=100,
			DateAndTimeOfOrder= DateTime.Parse("2001-01-01"),
			StockName="something",
			StockSymbol="msft"
			};
			await Assert.ThrowsAsync<ArgumentException>( async () =>
			{
				await stocksService.CreateBuyOrder(request);
			});
		}
		[Fact]
		public async Task CreateBuyOrder_InvalidQuantity2()
		{
			BuyOrderRequest request = new BuyOrderRequest()
			{
				Quantity = 100001,
				Price = 100,
				DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
				StockName = "something",
				StockSymbol = "msft"
			};
			await Assert.ThrowsAsync<ArgumentException>( async () =>
			{
				await stocksService.CreateBuyOrder(request);
			});
		}
		[Fact]
		public async Task CreateBuyOrder_InvalidPrice()
		{
			BuyOrderRequest request = new BuyOrderRequest()
			{
				Quantity = 1000,
				Price = 0,
				DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
				StockName = "something",
				StockSymbol = "msft"
			};
			await Assert.ThrowsAsync<ArgumentException>(	 async () =>
			{
				await stocksService.CreateBuyOrder(request);
			});
		}
		[Fact]
		public async Task CreateBuyOrder_InvalidPrice2()
		{
			BuyOrderRequest request = new BuyOrderRequest()
			{
				Quantity = 1000,
				Price = 10001,
				DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
				StockName = "something",
				StockSymbol = "msft"
			};
			await Assert.ThrowsAsync<ArgumentException>( async () =>
			{
				await  stocksService.CreateBuyOrder(request);
			});
		}
		[Fact]
		public async Task CreateBuyOrder_NullSymbole()
		{
			BuyOrderRequest request = new BuyOrderRequest()
			{
				Quantity = 1000,
				Price = 1001,
				DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
				StockName = "something",
				StockSymbol = null
			};
			await Assert.ThrowsAsync<ArgumentException>( async () =>
			{
				await stocksService.CreateBuyOrder(request);
			});
		}
		[Fact]
		public async Task CreateBuyOrder_InvalidDate()
		{
			BuyOrderRequest request = new BuyOrderRequest()
			{
				Quantity = 1000,
				Price = 1001,
				DateAndTimeOfOrder = DateTime.Parse("1991-01-01"),
				StockName = "something",
				StockSymbol = "msft"
			};
			await Assert.ThrowsAsync<ArgumentException>( async () =>
			{
				await stocksService.CreateBuyOrder(request);
			});
		}
		[Fact]
		public async Task CreateBuyOrder_Proper()
		{
			BuyOrderRequest request = new BuyOrderRequest()
			{
				Quantity = 1000,
				Price = 1001,
				DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
				StockName = "something",
				StockSymbol = "msft"
			};
			BuyOrderResponse response =await stocksService.CreateBuyOrder(request);
			List<BuyOrderResponse> responses =await  stocksService.ListBuyOrders();
			Assert.True(response.Id!=Guid.Empty);
			Assert.Contains(response,responses);
		}
		#endregion
		#region CreateSellOrder
		[Fact]
		public async Task CreateSellOrder_NullArgument()
		{
			SellOrderRequest request = null;
			await Assert.ThrowsAsync<ArgumentNullException>(async () =>
			{
			await	stocksService.CreateSellOrder(request);
			});
		}
		[Fact]
		public async Task CreateSellOrder_InvalidQuantity()
		{
			SellOrderRequest request = new SellOrderRequest()
			{
				Quantity = 0,
				Price = 100,
				DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
				StockName = "something",
				StockSymbol = "msft"
			};
			await Assert.ThrowsAsync<ArgumentException>(async () =>
			{
			await	stocksService.CreateSellOrder(request);
			});
		}
		[Fact]
		public async Task CreateSellOrder_InvalidQuantity2()
		{
			SellOrderRequest request = new SellOrderRequest()
			{
				Quantity = 100001,
				Price = 100,
				DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
				StockName = "something",
				StockSymbol = "msft"
			};
			await Assert.ThrowsAsync<ArgumentException>(async () =>
			{
			await	stocksService.CreateSellOrder(request);
			});
		}
		[Fact]
		public async Task CreateSellOrder_InvalidPrice()
		{
			SellOrderRequest request = new SellOrderRequest()
			{
				Quantity = 1000,
				Price = 0,
				DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
				StockName = "something",
				StockSymbol = "msft"
			};
			await Assert.ThrowsAsync<ArgumentException>(async () =>
			{
				await stocksService.CreateSellOrder(request);
			});
		}
		[Fact]
		public async Task CreateSellOrder_InvalidPrice2()
		{
			SellOrderRequest request = new SellOrderRequest()
			{
				Quantity = 1000,
				Price = 10001,
				DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
				StockName = "something",
				StockSymbol = "msft"
			};
			await Assert.ThrowsAsync<ArgumentException>(async () =>
			{
				await stocksService.CreateSellOrder(request);
			});
		}
		[Fact]
		public async Task CreateSellOrder_NullSymbole()
		{
			SellOrderRequest request = new SellOrderRequest()
			{
				Quantity = 1000,
				Price = 1001,
				DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
				StockName = "something",
				StockSymbol = null
			};
			await Assert.ThrowsAsync<ArgumentException>(async () =>
			{
				await stocksService.CreateSellOrder(request);
			});
		}
		[Fact]
		public async Task CreateSellOrder_InvalidDate()
		{
			SellOrderRequest request = new SellOrderRequest()
			{
				Quantity = 1000,
				Price = 1001,
				DateAndTimeOfOrder = DateTime.Parse("1991-01-01"),
				StockName = "something",
				StockSymbol = "msft"
			};
			await Assert.ThrowsAsync<ArgumentException>(async () =>
			{
			await	stocksService.CreateSellOrder(request);
			});
		}
		[Fact]
		public async Task CreateSellOrder_Proper()
		{
			SellOrderRequest request = new SellOrderRequest()
			{
				Quantity = 1000,
				Price = 1001,
				DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
				StockName = "something",
				StockSymbol = "msft"
			};
			SellOrderResponse response =await stocksService.CreateSellOrder(request);
			List<SellOrderResponse> responses =await stocksService.ListSellOrders();
			Assert.True(response.Id != Guid.Empty);
			Assert.Contains(response, responses);
		}
		#endregion
		#region GetBuyOrders
		[Fact]
		public async Task GetBuyOrders_EmptyList()
		{
			List<BuyOrderResponse> responses =await stocksService.ListBuyOrders();
			Assert.Empty(responses);
		}
		[Fact]
		public async Task GetBuyOrders_Proper()
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
			buyOrders.Add(await stocksService.CreateBuyOrder(req));
			}
			List<BuyOrderResponse> responses =await stocksService.ListBuyOrders();
			foreach (var response in responses) {
			Assert.Contains(response,buyOrders);
			}
		}
		#endregion
		#region GetSellOrders
		[Fact]
		public async Task GetSellOrders_EmptyList()
		{
			List<SellOrderResponse> responses =await stocksService.ListSellOrders();
			Assert.Empty(responses);
		}
		[Fact]
		public async Task GetSellOrders_Proper()
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
				sellOrders.Add( await stocksService.CreateSellOrder(req));
			}
			List<SellOrderResponse> responses =await stocksService.ListSellOrders();
			foreach (var response in responses)
			{
				Assert.Contains(response, sellOrders);
			}
		}
		#endregion
	}
}
