using StocksApp.ServiceContracts;
using System.Text.Json;

namespace StocksApp.Services
{
	public class FinnhubService : IFinnhubService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IConfiguration _configuration;

		public FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
		{
			_httpClientFactory = httpClientFactory;
			_configuration = configuration;
		}

		public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
		{
			using(HttpClient httpClient=_httpClientFactory.CreateClient())
			{
				HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
				{
					RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}"),
					Method = HttpMethod.Get,
				};
				HttpResponseMessage httpResponseMessage=await httpClient.SendAsync(httpRequestMessage);
				Stream stream= await httpResponseMessage.Content.ReadAsStreamAsync();
				StreamReader streamReader = new StreamReader(stream);
				string content = streamReader.ReadToEnd();
				Dictionary<string, object> stocks=JsonSerializer.Deserialize<Dictionary<string, object>>(content);
				if (stocks == null)
					throw new InvalidOperationException("Something wrong with server");
				if (stocks.ContainsKey("error"))
					throw new InvalidOperationException(Convert.ToString(stocks["error"]));
				return stocks;
			}
		}

		public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
		{
			using (HttpClient httpClient = _httpClientFactory.CreateClient())
			{
				HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
				{
					RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}"),
					Method = HttpMethod.Get,
				};
				HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

				Stream stream = await httpResponseMessage.Content.ReadAsStreamAsync();
				StreamReader streamReader = new StreamReader(stream);
				string content = streamReader.ReadToEnd();
				Dictionary<string, object> stockDict = JsonSerializer.Deserialize<Dictionary<string, object>>(content);
				if (stockDict == null)
					throw new InvalidOperationException("No response from finhub server");
				if (stockDict.ContainsKey("error"))
					throw new InvalidOperationException(Convert.ToString(stockDict["error"]));
				return stockDict;

			}
		}
	}
}
