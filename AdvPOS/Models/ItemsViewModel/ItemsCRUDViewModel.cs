using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace AdvPOS.Models.ItemsViewModel
{
    public class ItemsCRUDViewModel : EntityBase
    {
        public Int64 Id { get; set; }
        public string Code { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Cost Price")]
        [Required]
        public double CostPrice { get; set; }
        [Display(Name = "Normal Price")]
        [Required]
        public double NormalPrice { get; set; }
        [Display(Name = "Trade Price")]
        [Required]
        public double? TradePrice { get; set; }
        [Display(Name = "Premium Price")]
        [Required]
        public double? PremiumPrice { get; set; }
        [Display(Name = "Other Price")]
        [Required]
        public double? OtherPrice { get; set; }
        [Display(Name = "VAT")]
        public double? CostVAT { get; set; }
        [Display(Name = "VAT")]
        public double? NormalVAT { get; set; }
        [Display(Name = "VAT")]
        public double? PremiumVAT { get; set; }
        [Display(Name = "VAT")]
        public double? TradeVAT { get; set; }
        [Display(Name = "VAT")]
        public double? OtherVAT { get; set; }

        [Display(Name = "Old Unit Price")]
        [Required]
        public double OldUnitPrice { get; set; }
        [Display(Name = "Old Sell Price")]
        [Required]
        public double OldSellPrice { get; set; }
        [Display(Name = "Quantity")]
        [Required]
        public int Quantity { get; set; }
        [Display(Name = "New Quantity")]
        [Required]
        public int NewQuantity { get; set; }
        [Display(Name = "Categories")]
        public Int64? CategoriesId { get; set; }
        public string CategoriesDisplay { get; set; }
        [Display(Name = "Warehouse")]
        public Int64? WarehouseId { get; set; }
        public string WarehouseDisplay { get; set; }
        [Display(Name = "Supplier")]
        public Int64? SupplierId { get; set; }
        public string SupplierDisplay { get; set; }
        [Display(Name = "Measure")]
        public Int64? MeasureId { get; set; }
        public string MeasureDisplay { get; set; }
        [Display(Name = "Measure Value")]
        public double? MeasureValue { get; set; }
        public string Note { get; set; }
        [Display(Name = "Add New Quantity")]
        [Required]
        public int AddNewQuantity { get; set; }
        [Display(Name = "Update Qnt Type")]
        public string UpdateQntType { get; set; }
        [Display(Name = "Update Qnt Note")]
        public string UpdateQntNote { get; set; }
        public string CurrentURL { get; set; }
        [Display(Name = "SKU")]
        public string StockKeepingUnit { get; set; }
        [Display(Name = "Manufacture")]
        public DateTime ManufactureDate { get; set; } = DateTime.Now;
        [Display(Name = "Expiration")]
        public DateTime ExpirationDate { get; set; } = DateTime.Now.AddYears(3);
        public string Barcode { get; set; }
        [Display(Name = "Product Level")]
        public string ProductLevel { get; set; }
        [Display(Name = "Image")]
        public string ImageURL { get; set; } = "/upload/blank-item.png";
        public IFormFile ImageURLDetails { get; set; }
        [Display(Name = "Vat Percentage")]
        public double VatPercentage { get; set; }


        public static implicit operator ItemsCRUDViewModel(Items _Items)
        {
            return new ItemsCRUDViewModel
            {
                Id = _Items.Id,
                Code = _Items.Code,
                Name = _Items.Name,
                CostPrice = _Items.CostPrice,
                NormalPrice = _Items.NormalPrice,
                TradePrice = _Items.TradePrice,
                PremiumPrice = _Items.PremiumPrice,
                OtherPrice = _Items.OtherPrice,
                CostVAT = _Items.CostVAT,
                NormalVAT = _Items.NormalVAT,                
                TradeVAT = _Items.TradeVAT,
                PremiumVAT = _Items.PremiumVAT,
                OtherVAT = _Items.OtherVAT,

                OldUnitPrice = _Items.OldUnitPrice,
                OldSellPrice = _Items.OldSellPrice,
                Quantity = _Items.Quantity,
                CategoriesId = _Items.CategoriesId,
                WarehouseId = _Items.WarehouseId,
                SupplierId = _Items.SupplierId,
                MeasureId = _Items.MeasureId,
                MeasureValue = _Items.MeasureValue,
                Note = _Items.Note,

                UpdateQntType = _Items.UpdateQntType,
                UpdateQntNote = _Items.UpdateQntNote,
                StockKeepingUnit = _Items.StockKeepingUnit,
                ManufactureDate = _Items.ManufactureDate,
                ExpirationDate = _Items.ExpirationDate,
                Barcode = _Items.Barcode,
                ProductLevel = _Items.ProductLevel,
                VatPercentage = _Items.VatPercentage,
                ImageURL = _Items.ImageURL,

                CreatedDate = _Items.CreatedDate,
                ModifiedDate = _Items.ModifiedDate,
                CreatedBy = _Items.CreatedBy,
                ModifiedBy = _Items.ModifiedBy,
                Cancelled = _Items.Cancelled
            };
        }

        public static implicit operator Items(ItemsCRUDViewModel vm)
        {
            return new Items
            {
                Id = vm.Id,
                Code = vm.Code,
                Name = vm.Name,
                
                CostPrice = vm.CostPrice,
                NormalPrice = vm.NormalPrice,
                TradePrice = vm.TradePrice,
                PremiumPrice = vm.PremiumPrice,
                OtherPrice = vm.OtherPrice,
                CostVAT = vm.CostVAT,
                NormalVAT = vm.NormalVAT,
                TradeVAT = vm.TradeVAT,
                PremiumVAT = vm.PremiumVAT,
                OtherVAT = vm.OtherVAT,

                OldUnitPrice = vm.OldUnitPrice,
                OldSellPrice = vm.OldSellPrice,
                Quantity = vm.Quantity,
                CategoriesId = vm.CategoriesId,
                WarehouseId = vm.WarehouseId,
                SupplierId = vm.SupplierId,
                MeasureId = vm.MeasureId,
                MeasureValue = vm.MeasureValue,
                Note = vm.Note,

                UpdateQntType = vm.UpdateQntType,
                UpdateQntNote = vm.UpdateQntNote,
                StockKeepingUnit = vm.StockKeepingUnit,
                ManufactureDate = vm.ManufactureDate,
                ExpirationDate = vm.ExpirationDate,
                Barcode = vm.Barcode,
                ProductLevel = vm.ProductLevel,
                VatPercentage = vm.VatPercentage,
                ImageURL = vm.ImageURL,

                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled
            };
        }
    }
}