using System;

namespace AdvPOS.Models.ItemsViewModel
{
    public class ItemGridViewModel
    {
        public Int64? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string SupplierName { get; set; }
        public string MeasureName { get; set; }
        public string StockKeepingUnit { get; set; }
        public double CostPrice { get; set; }
        public double NormalPrice { get; set; }
        public int? Quantity { get; set; }
        public int? TotalSold { get; set; }
        public double? TotalEarned { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ImageURL { get; set; }
    }
}