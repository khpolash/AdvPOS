using System;

namespace AdvPOS.Models
{
    public class ExpenseDetails : EntityBase
    {
        public Int64 Id { get; set; }
        public Int64 ExpenseSummaryId { get; set; }
        public Int64 ExpenseTypeId { get; set; }
        public string ExpenseType { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
    }
}
