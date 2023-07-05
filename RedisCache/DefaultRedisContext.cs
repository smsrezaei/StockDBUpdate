using Infrastructure.Common.RedisCache.Config;
using Infrastructure.Common.RedisCache.Context;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisCache;


public class DefaultRedisContext : IRedisContext, IDisposable
{

    public IDatabase context { get; }

    public ConnectionMultiplexer Connection { get; }

    private readonly RadisDBConfigModel _radisDBConfig;
    public DefaultRedisContext(IOptions<RadisDBConfigs> options)
    {
        try
        {
            _radisDBConfig = options.Value.Configs.FirstOrDefault();

            Connection = ConnectionMultiplexer.Connect(_radisDBConfig.Host + ":" + _radisDBConfig.Port);
            context = Connection.GetDatabase(_radisDBConfig.DBNumber);
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }
    public void Dispose()
    {
        GC.Collect();
    }

}