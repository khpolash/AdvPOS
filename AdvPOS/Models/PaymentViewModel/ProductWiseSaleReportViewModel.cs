using System;

namespace AdvPOS.Models.PaymentViewModel
{
    public class ProductWiseSaleReportViewModel
    {
        public Int64 Id { get; set; }
        public DateTime SalesDate { get; set; }
        public Int64 ItemId { get; set; }
        public string ItemName { get; set; }
        public string InvoiceNo { get; set; }
        public string CustomerName { get; set; }
        public double? UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double? Discount { get; set; }
        public double? VAT { get; set; }
        public double? Total { get; set; }
    }
}
