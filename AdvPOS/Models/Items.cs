using System;

namespace AdvPOS.Models
{
    public class Items : EntityBase
    {
        public Int64 Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }        
        public double OldUnitPrice { get; set; }
        public double OldSellPrice { get; set; }
        public int Quantity { get; set; }
        public Int64? CategoriesId { get; set; }
        public Int64? WarehouseId { get; set; }
        public Int64? SupplierId { get; set; }
        public Int64? MeasureId { get; set; }
        public double? MeasureValue { get; set; }
        public string Note { get; set; }
        public string UpdateQntType { get; set; }
        public string UpdateQntNote { get; set; }
        public string StockKeepingUnit { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Barcode { get; set; }
        public string ProductLevel { get; set; }
        public double CostPrice { get; set; }
        public double NormalPrice { get; set; }
        public double? TradePrice { get; set; }
        public double? PremiumPrice { get; set; }
        public double? OtherPrice { get; set; }
        public double? CostVAT { get; set; }
        public double? NormalVAT { get; set; }
        public double? PremiumVAT { get; set; }
        public double? TradeVAT { get; set; }
        public double? OtherVAT { get; set; }
        public double VatPercentage { get; set; }      
        public string ImageURL { get; set; }
    }
}
