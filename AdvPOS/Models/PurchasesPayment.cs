using System;

namespace AdvPOS.Models
{
    public class PurchasesPayment : EntityBase
    {
        public Int64 Id { get; set; }
        public Int64 SupplierId { get; set; }
        public string InvoiceNo { get; set; }
        public string QuoteNo { get; set; }
        public double CommonCharge { get; set; }
        public double Discount { get; set; }
        public double DiscountAmount { get; set; }
        public double VAT { get; set; }
        public double VATAmount { get; set; }
        public double SubTotal { get; set; }
        public double? GrandTotal { get; set; }
        public double PaidAmount { get; set; }
        public double DueAmount { get; set; }
        public double ChangedAmount { get; set; }
        public Int64 CurrencyId { get; set; }
        public Int64 BranchId { get; set; }
        public Int64 PaymentStatus { get; set; }
        public int Category { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string CustomerNote { get; set; }
        public string PrivateNote { get; set; }
        public int ReturnType { get; set; }
    }
}
