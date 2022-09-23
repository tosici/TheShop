using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Serilog;
using Shop.WebApi.DatabaseLayer;
using Shop.WebApi.Interfaces;
using Shop.WebApi.Models;
using Shop.WebApi.Repository;
using Shop.WebApi.Services;
using System.Runtime.Intrinsics.Arm;

namespace Shop.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddScoped<IShopService,ShopService>();
            builder.Services.AddScoped<IShopRepository, ShopRepository>();
            builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
            builder.Services.AddSingleton<IDb, Db>();
            builder.Services.Configure<List<Dealer>>(builder.Configuration.GetSection("Dealers"));

            builder.Logging.ClearProviders();
            var path = builder.Configuration.GetValue<string>("LogFilePath");
            var logger = new LoggerConfiguration()
                .WriteTo.File(path,rollingInterval: RollingInterval.Day)
                .CreateLogger();
            builder.Logging.AddSerilog(logger);

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}