
using Infrastructure.Common.RedisCache.Model;

namespace Infrastructure.Common.RedisCache.Contract
{
    public interface IGenericTypeBaseRepository : IBaseRepository
    {
        IList<T> GetAll<T>() where T : BaseModel;
        T GetById<T>(string id) where T : BaseModel;
        bool Remove<T>(T entity) where T : BaseModel;
        string Save<T>(T entity) where T : BaseModel;
        public string Save<T>(T entity, TimeSpan expire) where T : BaseModel;
    }
}