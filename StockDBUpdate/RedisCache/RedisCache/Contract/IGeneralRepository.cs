using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.RedisCache.Contract
{
    public interface IGeneralRepository : IBaseRepository
    {
        T Get<T>(string key);
        bool Set<T>(string key, T item);
    }
}
