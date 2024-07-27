using System;

namespace AdvPOS.Models.ItemsViewModel
{
    public class ItemsDetailsViewModel : EntityBase
    {
        public Int64 Id { get; set; }
        public string Code { get; set; }
        public string ItemName { get; set; }
        public string CompanyName { get; set; }
        public string Measure { get; set; }
        public double? MeasureValue { get; set; }
        public double CostPrice { get; set; }
        public double NormalPrice { get; set; }
        public double OldUnitPrice { get; set; }
        public double OldSellPrice { get; set; }
        public int Quantity { get; set; }
        public int CategoriesId { get; set; }
        public Int64 WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public string Note { get; set; }
        public string UpdateQntType { get; set; }
        public string UpdateQntNote { get; set; }
        public string StockKeepingUnit { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Barcode { get; set; }
        public string ImageURL { get; set; }
    }
}