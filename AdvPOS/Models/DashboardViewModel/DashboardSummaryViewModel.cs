using System;
using System.Collections.Generic;

namespace AdvPOS.Models.DashboardViewModel
{
    public class DashboardSummaryViewModel
    {
        public int InvoiceToday { get; set; }
        public int InvoiceThisMonth { get; set; }
        public Int64 SalesToday { get; set; }
        public Int64 SalesThisMonth { get; set; }

        public int TotalItem { get; set; }
        public int TotalItemQuantity { get; set; }
        public int LowItems { get; set; }
        public int OutofStock { get; set; }
        public int DueInvoice { get; set; }

        public Int64 TotalCustomer { get; set; }
        public int TotalSupplier { get; set; }

        public Int64 TotalUser { get; set; }
        public Int64 TotalActive { get; set; }
        public Int64 TotalInActive { get; set; }
        public List<RecentInvoiceViewModel> listRecentInvoiceViewModel { get; set; }
        public List<UserProfile> listUserProfile { get; set; }
    }
}
