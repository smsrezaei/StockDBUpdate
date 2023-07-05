namespace Infrastructure.Common.RedisCache.Contract;

public interface IListBaseRepository<T> : IBaseRepository
{
    IList<string> GetAllKeys();
    IList<string> SearchKeys(string pattern);
    IList<T> GetAll(string key);
    IList<T> GetAll();
    void Add(string key, T entity);
}