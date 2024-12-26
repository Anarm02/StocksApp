using Microsoft.Extensions.Configuration;
using ServiceLayer.RepositoyContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceLayer.Repositories
{
	public class FinnhubRepostirory:IFinnhubRepository
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IConfiguration _configuration;

		public FinnhubRepostirory(IHttpClientFactory httpClientFactory, IConfiguration configuration)
		{
			_httpClientFactory = httpClientFactory;
			_configuration = configuration;
		}

		public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
		{
			using (HttpClient httpClient = _httpClientFactory.CreateClient())
			{
				HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
				{
					RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}"),
					Method = HttpMethod.Get,
				};
				HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
				Stream stream = await httpResponseMessage.Content.ReadAsStreamAsync();
				StreamReader streamReader = new StreamReader(stream);
				string content = streamReader.ReadToEnd();
				Dictionary<string, object> stocks = JsonSerializer.Deserialize<Dictionary<string, object>>(content);
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

		public async Task<List<Dictionary<string, string>>?> GetStocks()
		{
			HttpClient httpClient = _httpClientFactory.CreateClient();

			//create http request
			HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri($"https://finnhub.io/api/v1/stock/symbol?exchange=US&token={_configuration["FinnhubToken"]}") //URI includes the secret token
			};

			//send request
			HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

			//read response body
			string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();

			//convert response body (from JSON into Dictionary)
			List<Dictionary<string, string>>? responseDictionary = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(responseBody);

			if (responseDictionary == null)
				throw new InvalidOperationException("No response from server");

			//return response dictionary back to the caller
			return responseDictionary;
		}

		public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
		{
			using (HttpClient httpClient = _httpClientFactory.CreateClient())
			{
				HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
				{
					RequestUri = new Uri($"https://finnhub.io/api/v1/search?q={stockSymbolToSearch}&exchange=US&token={_configuration["FinnhubToken"]}"),
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
