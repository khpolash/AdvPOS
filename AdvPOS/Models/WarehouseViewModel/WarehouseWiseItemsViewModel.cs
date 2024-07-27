using System;

namespace AdvPOS.Models.WarehouseViewModel
{
    public class WarehouseWiseItemsViewModel
    {
        public Int64 WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public Int64 TotalAvailableItem { get; set; }
        public Int64 TotalAvailableQuantity { get; set; }
    }
}
