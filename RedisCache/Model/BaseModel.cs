using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

[DataContract]
public class BaseModel
{
    [DataMember(Order = 1)]
    public string Id { get; set; }

}