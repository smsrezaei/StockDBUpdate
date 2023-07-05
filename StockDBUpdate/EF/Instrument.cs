using System;
using System.Collections.Generic;

namespace StockDBUpdate.EF
{
    public partial class Instrument
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public long? InsCode { get; set; }
        public string? Title { get; set; }
        public string? GroupName { get; set; }
        public string? Eps { get; set; }
    }
}
