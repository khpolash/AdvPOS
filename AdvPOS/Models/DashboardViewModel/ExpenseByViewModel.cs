using System;
using System.Collections.Generic;

namespace AdvPOS.Models.DashboardViewModel
{
    public class ExpenseByViewModel
    {
        public Int64 Id { get; set; }
        public string TranDate { get; set; }      
        public int TotalTran { get; set; }
        public int TotalQuantity { get; set; }
        public double? TotalExpense { get; set; }
        public double TotalPaid { get; set; }
        public double TotalDue { get; set; }
        public List<ExpenseByViewModel> listExpenseByViewModel { get; set; }
    }
}
