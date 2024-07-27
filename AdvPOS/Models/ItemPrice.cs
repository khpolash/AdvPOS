using System;

namespace AdvPOS.Models
{
    public class ItemPrice : EntityBase
    {
        public Int64 Id { get; set; }
        public double CostPrice { get; set; }
        public double NormalPrice { get; set; }
        public double TradePrice { get; set; }
        public double PremiumPrice { get; set; }
        public double OtherPrice { get; set; }
    }
}
