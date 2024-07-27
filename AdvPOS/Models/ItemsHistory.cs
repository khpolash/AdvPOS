using System;

namespace AdvPOS.Models
{
    public class ItemsHistory : EntityBase
    {
        public Int64 Id { get; set; }
        public Int64 ItemId { get; set; }
        public double CostPrice { get; set; }
        public double NormalPrice { get; set; }
        public double OldUnitPrice { get; set; }
        public double OldSellPrice { get; set; }
        public int OldQuantity { get; set; }
        public int NewQuantity { get; set; }
        public int TranQuantity { get; set; }
        public string Action { get; set; }
    }
}