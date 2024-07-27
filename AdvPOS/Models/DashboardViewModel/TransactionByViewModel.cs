using System;
using System.Collections.Generic;

namespace AdvPOS.Models.DashboardViewModel
{
    public class TransactionByViewModel
    {
        public Int64 Id { get; set; }
        public string TranDate { get; set; }
        public int QuantitySold { get; set; }
        public int TotalTran { get; set; }
        public double? TotalEarned { get; set; }
        public double TotalDue { get; set; }
        public List<TransactionByViewModel> listTransactionByViewModel { get; set; }
    }
}
