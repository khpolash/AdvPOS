using System;

namespace AdvPOS.Models.PaymentViewModel
{
    public class PaymentGridViewModel
    {
        public Int64 Id { get; set; }
        public string CustomerName { get; set; }
        public double Discount { get; set; }
        public double VAT { get; set; }
        public double SubTotal { get; set; }
        public double? GrandTotal { get; set; }
        public double? PaidAmount { get; set; }
        public double? DueAmount { get; set; }
        public int Category { get; set; }
        public Int64 BranchId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}


