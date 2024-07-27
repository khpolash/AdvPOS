using System;

namespace AdvPOS.Models.PaymentModeHistoryViewModel
{
    public class PaymentModeHistoryGridViewModel : EntityBase
    {
        public Int64 Id { get; set; }
        public Int64 PaymentId { get; set; }
        public string PaymentType { get; set; }
        public double? Amount { get; set; }
        public string ReferenceNo { get; set; }
    }
}
