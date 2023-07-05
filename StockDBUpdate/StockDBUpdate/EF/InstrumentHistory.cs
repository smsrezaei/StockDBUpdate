using System;
using System.Collections.Generic;

namespace StockDBUpdate.EF
{
    public partial class InstrumentHistory
    {
        public long Id { get; set; }
        public string Symbol { get; set; } = null!;
        public long? InsCode { get; set; }
        public decimal? LastPrice { get; set; }
        public DateTime Tmst { get; set; }
    }
}
