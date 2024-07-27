using System;

namespace AdvPOS.Models
{
    public class ExpenseSummary : EntityBase
    {
        public Int64 Id { get; set; }
        public string Title { get; set; }
        public double GrandTotal { get; set; }
        public double PaidAmount { get; set; }
        public double DueAmount { get; set; }
        public double ChangeAmount { get; set; }
        public Int64 CurrencyCode { get; set; }
        public Int64 BranchId { get; set; }
        public int Action { get; set; }
    }
}
