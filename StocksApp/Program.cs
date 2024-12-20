using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using ServiceLayer.Context;
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
			builder.Services.AddScoped<IStocksService,StocksService>();
			builder.Services.AddDbContext<AppDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});
			var app = builder.Build();
			RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath : "Rotativa");
			app.UseStaticFiles();
			app.UseRouting();
			app.MapControllers();

			app.Run();
		}
	}
}
