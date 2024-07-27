using System;

namespace AdvPOS.Models
{
    public class PaymentDetail : EntityBase
    {
        public Int64 Id { get; set; }
        public Int64 PaymentId { get; set; }
        public Int64 ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public double? UnitPrice { get; set; }
        public double? ItemVAT { get; set; }
        public double? ItemVATAmount { get; set; }
        public double? ItemDiscount { get; set; }
        public double? ItemDiscountAmount { get; set; }
        public double? TotalAmount { get; set; }
        public bool IsReturn { get; set; }
    }
}
