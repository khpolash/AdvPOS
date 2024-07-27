using System;
using System.Collections.Generic;

namespace AdvPOS.Models.DashboardViewModel
{
    public class SummaryReportViewModel
    {
        public Int64 Id { get; set; }
        public string TranDate { get; set; }
        public double SalesTotal { get; set; }
        public double SalesPaidTotal { get; set; }
        public double SalesDueTotal { get; set; }
        public double PurchasesTotal { get; set; }
        public double PurchasesPaidTotal { get; set; }
        public double PurchasesDueTotal { get; set; }
        public double ExpenseTotal { get; set; }
        public double ExpensePaidTotal { get; set; }
        public double ExpenseDueTotal { get; set; }
        //public List<SummaryReportViewModel> listSummaryReportViewModel { get; set; }
    }
}
