using Infrastructure.Common.RedisCache.Context;
using Infrastructure.Common.RedisCache.Contract;
using Infrastructure.Common.RedisCache.Model;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace RedisCache;

public class TypeBaseRepository<T> : BaseRepository, ITypeBaseRepository<T> where T : BaseModel
{
    public TypeBaseRepository(IRedisContext context) : base(context)
    {
    }

    public virtual IList<T> GetAll()
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
    public string Save(T entity)
    {
        if (entity != null && string.IsNullOrEmpty(entity.Id))
            entity.Id = GetNewId();

        _db.StringSet(Getkey(entity.Id), ToJsonString(entity));

        return entity.Id;
    }
    public string Save(T entity, TimeSpan expire)
    {
        if (entity != null && string.IsNullOrEmpty(entity.Id))
            entity.Id = GetNewId();

        _db.StringSet(Getkey(entity.Id), ToJsonString(entity), expire);

        return entity.Id;
    }
    public void BulkSave(IList<T> entityList)
    {
        foreach (var entity in entityList)
        {
            if (entity != null && string.IsNullOrEmpty(entity.Id))
                entity.Id = GetNewId();

            _db.StringSet(Getkey(entity.Id), ToJsonString(entity));
        }
    }

    public void BulkSave(IList<T> entityList, TimeSpan expire)
    {
        foreach (var entity in entityList)
        {
            if (entity != null && string.IsNullOrEmpty(entity.Id))
                entity.Id = GetNewId();

            _db.StringSet(Getkey(entity.Id), ToJsonString(entity), expire);
        }
    }

    private RedisKey Getkey(string id)
    {
        return $"urn:{GetEntityName<T>()}:{id}";
    }

    protected virtual string GetNewId()
    {
        var key = $"{GetEntityName<T>()}_identity";
        _db.StringIncrement(key);
        return _db.StringGet(key);
    }

    public bool Remove(T entity)
    {
        _db.KeyDelete(Getkey(entity.Id));
        return true;
    }
    public T GetById(string id)
    {
        var data = _db.StringGet(Getkey(id));

        return data.HasValue ? Deserialize<T>(data) : null;
    }
    public IDisposable LockEntity(string id)
    {
        _connection.GetDatabase().LockTake/*.LockExtend*/(Getkey(id), Getkey(id).ToString(), TimeSpan.FromMinutes(30));
        return this;
    }
    public void UnlockEntity(string id)
    {
        _connection.GetDatabase().LockRelease(Getkey(id), Getkey(id).ToString());
    }
    public void Dispose(string id)
    {
        _connection.GetDatabase().LockRelease(Getkey(id), Getkey(id).ToString());
        base.Dispose();
    }
}

// https://github.com/catcherwong/Demos/blob/master/src/RedisLockDemo/RedisLockDemo/Program.cs

// https://stackoverflow.com/questions/25127172/stackexchange-redis-locktake-lockrelease-usage