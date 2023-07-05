using Infrastructure.Common.RedisCache.Context;
using Infrastructure.Common.RedisCache.Contract;
using Infrastructure.Common.RedisCache.Model;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace RedisCache; 

public class GenericTypeBaseRepository : BaseRepository, IGenericTypeBaseRepository
{
    public GenericTypeBaseRepository(IRedisContext context) : base(context)
    {
    }

    private RedisKey Getkey<T>(string id)
    {
        return $"urn:{GetEntityName<T>()}:{id}";
    }

    protected virtual string GetNewId<T>()
    {
        var key = $"{GetEntityName<T>()}_identity";
        _db.StringIncrement(key);
        return _db.StringGet(key);
    }

    public IList<T> GetAll<T>() where T : BaseModel
    {
        List<T> list = new List<T>();
        foreach (var ep in _connection.GetEndPoints())
        {
            var server = _connection.GetServer(ep);
            var keys = server.Keys(database: 0, pattern: $"urn:{GetEntityName<T>()}:" + "*").ToArray();
            RedisValue[] queueValues = _db.StringGet(keys);
            list = queueValues.Select(qv => JsonConvert.DeserializeObject<T>(qv)).ToList();
        }

        return list;
    }

    public T GetById<T>(string id) where T : BaseModel
    {
        var data = _db.StringGet(Getkey<T>(id));
        return data.HasValue ? Deserialize<T>(data) : null;
    }

    public bool Remove<T>(T entity) where T : BaseModel
    {
        _db.KeyDelete(Getkey<T>(entity.Id));
        return true;
    }

    public string Save<T>(T entity) where T : BaseModel
    {
        if (entity != null && string.IsNullOrEmpty(entity.Id))
            entity.Id = GetNewId<T>();

        _db.StringSet(Getkey<T>(entity.Id), ToJsonString(entity));

        return entity.Id;
    }
    public string Save<T>(T entity, TimeSpan expire) where T : BaseModel
    {
        if (entity != null && string.IsNullOrEmpty(entity.Id))
            entity.Id = GetNewId<T>();

        _db.StringSet(Getkey<T>(entity.Id), ToJsonString(entity),expire);

        return entity.Id;
    }

}
