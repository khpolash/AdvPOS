using System;

namespace AdvPOS.Models.PurchasesPaymentViewModel
{
    public class RecentPurchasesInvoiceViewModel
    {
        public Int64 Id { get; set; }
        public string InvoiceNo { get; set; }
        public string CustomerName { get; set; }
        public double Amount { get; set; }
        public double PaidAmount { get; set; }
        public double DueAmount { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime InvoiceDate { get; set; }
    }
}
