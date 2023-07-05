using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.RedisCache.Config;

public class RadisDBConfigModel
{
    public string Name { get; set; }
    public int DBNumber { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
}

public class RadisDBConfigs
{
    public List<RadisDBConfigModel> Configs { get; set; }
}
