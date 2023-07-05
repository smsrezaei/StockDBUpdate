using System.Runtime.Serialization;

namespace Infrastructure.Common.RedisCache.Model;

[DataContract]
public class ListBaseModel<T> : BaseModel
{
    [DataMember(Order = 1)]
    public List<T> Items { get; set; }

    public ListBaseModel()
    {
        Items = new List<T>();
    }
}
