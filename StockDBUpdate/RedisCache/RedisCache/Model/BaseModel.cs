using System;
using System.Runtime.Serialization;

namespace Infrastructure.Common.RedisCache.Model;

[DataContract]
public class BaseModel
{
    [DataMember(Order = 1)]
    public string Id { get; set; }

}
