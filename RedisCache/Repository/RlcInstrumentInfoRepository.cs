using Infrastructure.Common.RedisCache.Context;
using Infrastructure.Common.RedisCache.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisCache.Repository;

public interface IRlcInstrumentInfoRepository : ITypeBaseRepository<RlcMarketInstrumentInfo>
{
}

public class RlcInstrumentInfoRepository : TypeBaseRepository<RlcMarketInstrumentInfo>, IRlcInstrumentInfoRepository
{
    public RlcInstrumentInfoRepository(IRedisContext redis) : base(redis)
    {
    }
}
