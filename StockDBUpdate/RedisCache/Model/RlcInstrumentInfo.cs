using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

[Serializable]
[JsonObject]
public class RlcMarketInstrumentInfo : BaseModel
{
    public RlcMarketInstrumentInfo()
    {
        this.BestLimit = new BestLimit();
        this.IndividualLegalTrades = new IndividualLegalTrades();
        this.LastTrade = new TradeInfo();
    }
    [DataMember]
    public string MessageId { get; set; }
    [DataMember]
    public string Code { get; set; }
    [DataMember]
    public string InstrumentId { get; set; }
    [DataMember]
    public string SymbolFa { get; set; }
    [DataMember]
    public string NameFa { get; set; }
    [DataMember]
    public string SymbolEn { get; set; }
    [DataMember]
    public string NameEn { get; set; }
    [DataMember]
    public decimal ClosingPrice { get; set; }
    [DataMember]
    public decimal DayAllowedMinPrice { get; set; }
    [DataMember]
    public decimal DayAllowedMaxPrice { get; set; }
    [DataMember]
    public decimal AllowedMinVolume { get; set; }
    [DataMember]
    public decimal AllowedMaxVolume { get; set; }
    [DataMember]
    public IndividualLegalTrades IndividualLegalTrades { get; set; }
    [DataMember]
    public BestLimit BestLimit { get; set; }
    [DataMember]
    public TradeInfo LastTrade { get; set; }
    [DataMember]
    public double EPS { get; set; }
    [DataMember]
    public int NAV { get; set; }
    [DataMember]
    public string NavTime { get; set; }
    [DataMember]
    public decimal BaseVol { get; set; }
    [DataMember]
    public decimal PriceYesterday { get; set; }
    [DataMember]
    public DateTime ServerTime { get; set; }
    [DataMember]
    public decimal YearPriceMax { get; set; }
    [DataMember]
    public decimal YearPriceMin { get; set; }
    [DataMember]
    public decimal WeekPriceMax { get; set; }
    [DataMember]
    public decimal WeekPriceMin { get; set; }
    [DataMember]
    public string State { get; set; }
    [DataMember]
    public string StateReason { get; set; }
    [DataMember]
    public InstrumentStatus TradeState;

    public string TradeStateDescription
    {
        get
        {
            return TradeState.GetDisplayName();
        }
    }


    [DataMember]

    public double PClosingChangePercent => PriceYesterday == 0 ? 0 : Math.Round((double)((ClosingPrice - PriceYesterday) / PriceYesterday), 4) * 100;

    [DataMember]
    public double PLastChangePercent => PriceYesterday == 0 ? 0 : Math.Round((double)((LastTrade.LastTradePrice - PriceYesterday) / PriceYesterday), 4) * 100;

    [DataMember]
    public bool IsBuyQueue
    {
        get
        {
            bool retValue = false;
            try
            {
                if (BestLimit.Items.Count > 0)
                {
                    var bestlimitItems = BestLimit.Items.Values.ToList().OrderBy(q => q.Level).ToList();
                    if (bestlimitItems[0].VolumeBuy > 0 && IsPriceInRange(bestlimitItems[0].PriceBuy))
                    {
                        if (bestlimitItems[0].VolumeSell == 0)
                        {
                            retValue = true;
                        }
                        else if (!IsPriceInRange(bestlimitItems[0].PriceSell))
                        {
                            retValue = true;
                        }
                    }
                }

            }
            catch { }
            return retValue;
        }
        set { }
    }

    [DataMember]
    public bool IsSellQueue
    {
        get
        {
            bool retValue = false;
            try
            {
                if (BestLimit.Items.Count > 0)
                {
                    var bestlimitItems = BestLimit.Items.Values.ToList().OrderBy(q => q.Level).ToList();
                    if (bestlimitItems[0].VolumeSell > 0 && IsPriceInRange(bestlimitItems[0].PriceSell))
                    {
                        if (bestlimitItems[0].VolumeBuy == 0)
                        {
                            retValue = true;
                        }
                        else if (!IsPriceInRange(bestlimitItems[0].PriceBuy))
                        {
                            retValue = true;
                        }
                    }
                }
            }
            catch { }
            return retValue;
        }
        set { }
    }

    public object Clone()
    {
        return this.MemberwiseClone();
    }

    public bool IsPriceInRange(decimal price)
    {
        return (price >= DayAllowedMinPrice && price <= DayAllowedMaxPrice);
    }

    public override string ToString()
    {
        return
            $"\\nI:{InstrumentId:N0} \\nS:{SymbolFa} \\nLastTrade:{LastTrade.ToString()} \\nIsBuyQueue:{IsBuyQueue} \\nIsSellQueue:{IsSellQueue} \\nDayAllowedMinPrice:{DayAllowedMinPrice} \\nDayAllowedMaxPrice:{DayAllowedMaxPrice}";
    }
}

[Serializable]
[JsonObject]
public class BestLimit
{
    public BestLimit()
    {
        this.Items = new Dictionary<int, BestLimitItem>();
    }

    public Dictionary<int, BestLimitItem> Items { get; set; }
}
[Serializable]
[JsonObject]
public class BestLimitItem
{
    [DataMember]
    [DisplayName("رتبه مظنه")]
    public int Level { get; set; }
    [DataMember]
    public int QtyBuy { get; set; }
    [DataMember]
    public decimal PriceBuy { get; set; }
    [DataMember]
    public int VolumeBuy { get; set; }
    [DataMember]
    public int QtySell { get; set; }
    [DataMember]
    public decimal PriceSell { get; set; }
    [DataMember]
    public int VolumeSell { get; set; }
    [DataMember]
    public DateTime OrderTime { get; set; }
    [DataMember]
    public double YTMSell { get; set; }
    [DataMember]
    public double YTMBuy { get; set; }
}

[Serializable]
[JsonObject]
public class IndividualLegalTrades
{
    [DataMember]
    [DisplayName("تعداد خرید حقیقی")]
    public int IndividualBuyQuantity { get; set; }
    [DataMember]
    [DisplayName("حجم خرید حقیقی")]
    public decimal IndividualBuyVolume { get; set; }
    [DataMember]
    [DisplayName("تعداد خرید حقوقی")]
    public int LegalBuyQuantity { get; set; }
    [DataMember]
    [DisplayName("حجم خرید حفوفی")]
    public decimal LegalBuyVolume { get; set; }
    [DataMember]
    [DisplayName("تعداد فروش حقیقی")]
    public int IndividualSellQuantity { get; set; }
    [DataMember]
    [DisplayName("حجم فروش حقیقی")]
    public decimal IndividualSellVolume { get; set; }
    [DataMember]
    [DisplayName("تعداد فروش حقوقی")]
    public int LegalSellQuantity { get; set; }
    [DataMember]
    [DisplayName("حجم فروش حقوقی")]
    public decimal LegalSellVolume { get; set; }
    [DataMember]
    [DisplayName("جمع تعداد خرید")]
    public int TotalBuyQuantity => this.IndividualBuyQuantity + this.LegalBuyQuantity;

    [DataMember]
    [DisplayName("جمع تعداد فروش")]
    public int TotalSellQuantity => this.IndividualSellQuantity + this.LegalSellQuantity;

    [DataMember]
    [DisplayName("جمع حجم خرید")]
    public decimal TotalBuyVolume => this.IndividualBuyVolume + this.LegalBuyVolume;

    [DataMember]
    [DisplayName("جمع حجم فروش")]
    public decimal TotalSellVolume => this.IndividualSellVolume + this.LegalSellVolume;
}
[Serializable]
[JsonObject]
public class TradeInfo
{
    [DataMember]
    public decimal LastTradePrice { get; set; }
    [DataMember]
    public decimal MinDayPrice { get; set; }
    [DataMember]
    public decimal MaxDayPrice { get; set; }
    [DataMember]
    public DateTime LastTradeTime { get; set; }
    [DataMember]
    public string LastTradeTimePersian { get; set; }
    [DataMember]
    public int TotalTradeVolume { get; set; }
    [DataMember]
    public int TotalTradeCount { get; set; }
    [DataMember]
    public decimal TotalTradeValue { get; set; }
    [DataMember]
    public int LastTradeVol { get; set; }

    public override string ToString()
    {
        return
            $"P:{LastTradePrice:N0} T:{LastTradeTime} Count:{TotalTradeCount:N0} TVol:{TotalTradeVolume:N0} TVal:{TotalTradeValue:N0}";
    }
}
