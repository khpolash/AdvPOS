using System;
using System.ComponentModel.DataAnnotations;

namespace AdvPOS.Models.WarehouseViewModel
{
    public class TransferItemViewModel : EntityBase
    {
        [Required]
        [Display(Name = "Item Name")]
        public Int64 ItemId { get; set; }
        [Display(Name = "Current Total Stock")]
        public int CurrentTotalStock { get; set; }
        [Display(Name = "Total Transfer Item")]
        [Required]
        public int TotalTransferItem { get; set; }
        [Display(Name = "From Warehouse")]
        [Required]
        public Int64 FromWarehouseId { get; set; }
        [Display(Name = "To Warehouse")]
        [Required]
        public Int64 ToWarehouseId { get; set; }
        [Display(Name = "Reason Of Transfer")]
        public string ReasonOfTransfer { get; set; }
    }
}