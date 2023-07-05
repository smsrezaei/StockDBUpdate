
using Infrastructure.Common.RedisCache.Context;
using Infrastructure.Common.RedisCache.Contract;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.ComponentModel;

namespace RedisCache;


public class BaseRepository : IBaseRepository
{
    protected static ConnectionMultiplexer _connection;
    private IRedisContext _rdx;
    protected IDatabase _db;
    public BaseRepository(IRedisContext redis)
    {
        _connection = redis.Connection;
        _db = redis.context;
    }

    protected string ToJsonString(object item)
    {
        var output = JsonConvert.SerializeObject(item);
        return output;
    }

    protected T Deserialize<T>(string itemStr)
    {
        var output = JsonConvert.DeserializeObject<T>(itemStr);
        return output;
    }

    protected string GetEntityName<T>()
    {
        return TypeDescriptor.GetClassName(typeof(T)).Split('.').Last().ToLower();
    }

    public void Dispose()
    {
        GC.Collect();
    }
}
