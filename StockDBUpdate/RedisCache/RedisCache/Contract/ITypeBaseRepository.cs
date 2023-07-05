

using Infrastructure.Common.RedisCache.Model;

namespace Infrastructure.Common.RedisCache.Contract
{
    public interface ITypeBaseRepository<T> : IBaseRepository where T : BaseModel
    {
        IList<T> GetAll();
        T GetById(string id);
        IDisposable LockEntity(string id);
        bool Remove(T entity);
        string Save(T entity);
        string Save(T entity, TimeSpan expire);
        void BulkSave(IList<T> entityList);
        void BulkSave(IList<T> entityList, TimeSpan expire);
        void UnlockEntity(string id);
        void Dispose(string id);
    }
}