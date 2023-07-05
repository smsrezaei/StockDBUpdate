using System;
using System.Collections.Generic;

namespace StockDBUpdate.EF
{
    public partial class Type1Stock
    {
        public long Id { get; set; }
        public string Symbol { get; set; } = null!;
        public long? InsCode { get; set; }
        public decimal? LastPrice { get; set; }
        public decimal? SupportPrice { get; set; }
        public decimal? TargetPrice1 { get; set; }
        public decimal? TargetPrice2 { get; set; }
        public decimal? TargetPrice3 { get; set; }
        public DateTime Tmst { get; set; }
    }
}
