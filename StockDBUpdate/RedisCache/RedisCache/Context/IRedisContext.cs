
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.RedisCache.Context;

public interface IRedisContext : System.IDisposable
{
    StackExchange.Redis.IDatabase context { get; }
    ConnectionMultiplexer Connection { get; }

}