
using Infrastructure.Common.RedisCache.Context;
using Infrastructure.Common.RedisCache.Contract;

namespace RedisCache; 

public class GeneralRepository : BaseRepository, IGeneralRepository
{
    public GeneralRepository(IRedisContext context) : base(context) { }

    public T Get<T>(string key)
    {
        var data = _db.StringGet(key);
        return data.HasValue ? Deserialize<T>(data) : default(T);
    }

    public bool Set<T>(string key, T item)
    {
        _db.StringSet(key, ToJsonString(item));
        return true;
    }
}
