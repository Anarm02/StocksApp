using ServiceLayer.ServiceContracts;
using ServiceLayer.Services;
using StocksApp.ServiceContracts;
using StocksApp.Services;

namespace StocksApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddControllersWithViews();
			builder.Services.AddHttpClient();
			builder.Services.AddScoped<IFinnhubService,FinnhubService>();
			builder.Services.AddSingleton<IStocksService,StocksService>();
			var app = builder.Build();

			app.UseStaticFiles();
			app.UseRouting();
			app.MapControllers();

			app.Run();
		}
	}
}
