
using Infrastructure.Common.RedisCache.Context;
using Infrastructure.Common.RedisCache.Contract;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace RedisCache;

public class ListBaseRepository<T> : BaseRepository, IListBaseRepository<T>
{

    public ListBaseRepository(IRedisContext context) : base(context)
    {
    }

    public IList<string> GetAllKeys()
    {

        List<string> list = new List<string>();

        foreach (var ep in _connection.GetEndPoints())
        {
            var server = _connection.GetServer(ep);
            var keys = server.Keys(/*database: 1*/).ToArray();

            foreach (var item in keys)
            {
                list.Add(item.ToString());
            }
        }

        return list;

    }

    public virtual void Add(string key, T entity)
    {
        _db.StringSet(GetFullKeyName(key), ToJsonString(entity));
    }

    public IList<T> GetAll(string key)
    {
        List<T> list = new List<T>();

        foreach (var ep in _connection.GetEndPoints())
        {
            var server = _connection.GetServer(ep);
            var keys = server.Keys(/*database: 0,*/ pattern: GetFullKeyName(key)).ToArray();
            RedisValue[] queueValues = _db.StringGet(keys);
            list = queueValues.Select(qv => JsonConvert.DeserializeObject<T>(qv)).ToList();
        }

        return list;
    }

    public IList<T> GetAll()
    {
        List<T> list = new List<T>();

        foreach (var ep in _connection.GetEndPoints())
        {
            var server = _connection.GetServer(ep);
            var keys = server.Keys(/*database: 1,*/ pattern: $"urn:{GetEntityName<T>()}:" + "*").ToArray();
            RedisValue[] queueValues = _db.StringGet(keys);
            list = queueValues.Select(qv => JsonConvert.DeserializeObject<T>(qv)).ToList();
        }

        return list;
    }

    public IList<string> SearchKeys(string pattern)
    {
        List<string> list = new List<string>();

        foreach (var ep in _connection.GetEndPoints())
        {
            var server = _connection.GetServer(ep);
            var keys = server.Keys(/*database: 1*/).ToArray();

            foreach (var item in keys)
            {
                list.Add(item.ToString());
            }
        }

        return list;
    }

    protected virtual string GetFullKeyName(string key)
    {
        return $"{GetEntityName<T>()}:{key}";
    }

}
