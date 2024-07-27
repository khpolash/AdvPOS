using System;

namespace AdvPOS.Models.ItemCartViewModel
{
    public class ItemCartSideInvoiceViewModel
    {
        public Int64 Id { get; set; }
        public Int64 CustomerId { get; set; }
        public double SubTotal { get; set; }
        public double GrandTotal { get; set; }
        public double PaidAmount { get; set; }
        public double ChangedAmount { get; set; }
        public IEnumerable<PaymentDetail> listPaymentDetail { get; set; }
        public bool IsSaveAndPrint { get; set; }
    }
}
