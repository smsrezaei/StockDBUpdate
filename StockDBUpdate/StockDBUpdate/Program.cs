using Infrastructure.Common.RedisCache.Config;
using Infrastructure.Common.RedisCache.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedisCache;
using RedisCache.Repository;
using StockDBUpdate.EF;

namespace StockDBUpdate;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Init App.");

        var configuration = new ConfigurationBuilder()
             .AddJsonFile($"appsettings.json");
        var config = configuration.Build();
        var connectionString = config.GetConnectionString("sqlConnection");

        var dbOptions = new DbContextOptionsBuilder<stockdbContext>()
            .UseSqlServer(config.GetConnectionString("sqlConnection"))
            .Options;

        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var redis = (IRlcInstrumentInfoRepository)serviceProvider.GetService(typeof(IRlcInstrumentInfoRepository));

        using (var dbContext = new stockdbContext(dbOptions))
        {
            Console.WriteLine("Fetch DB Instrument.");
            var instrument = dbContext.Type1Stocks.ToList();

            foreach (var instrumentItem in instrument) {
                if (instrumentItem.InsCode != null){
                    Console.WriteLine("Fetch Redis Data for " + instrumentItem.InsCode);
                    try { 
                    var redisItem = redis.GetById(instrumentItem.InsCode.ToString());
                    if (redisItem != null){
                        Console.WriteLine("Redis data for " + instrumentItem.InsCode  + " is " + redisItem.LastTrade?.LastTradePrice);
                        var instrumentHistory = new InstrumentHistory();
                        instrumentHistory.Symbol = instrumentItem.Symbol;
                        instrumentHistory.InsCode = instrumentItem.InsCode;
                        instrumentHistory.LastPrice = redisItem.LastTrade?.LastTradePrice;
                        instrumentHistory.Tmst = DateTime.Now;
                        Console.WriteLine("Insert Data to DB for instrument " + instrumentItem.InsCode 
                            + " price " + redisItem.LastTrade?.LastTradePrice);
                        dbContext.InstrumentHistories.Add(instrumentHistory);
                        dbContext.SaveChanges();
                    }
                    }catch(Exception ex) { Console.WriteLine("Error " + ex.ToString()); }
                }
            }

            //var redisData = redis.GetAll();
        }

        Console.WriteLine("Job Done!");
        Console.ReadLine();
    }
    private static void ConfigureServices(IServiceCollection services)
    {
        IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appSettings.json", false)
        .Build();

        services.AddSingleton<IConfiguration>(configuration);
        services.Configure<RadisDBConfigs>(options => configuration.GetSection("RadisDBConfigs").Bind(options));

        services.AddScoped<IRedisContext, DefaultRedisContext>();
        services.AddScoped<IRlcInstrumentInfoRepository, RlcInstrumentInfoRepository>();
    }

}