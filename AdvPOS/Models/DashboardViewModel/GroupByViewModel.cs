using System.Collections.Generic;

namespace AdvPOS.Models.DashboardViewModel
{
    public class GroupByViewModel
    {
        public string ItemName { get; set; }
        public double? ItemTotal { get; set; }
        public List<GroupByViewModel> listGroupByViewModel { get; set; }
    }
}
