using System;
using System.ComponentModel.DataAnnotations;

namespace AdvPOS.Models.DamageItemDeatilsViewModel
{
    public class DamageItemDeatilsCRUDViewModel : EntityBase
    {
        public Int64 Id { get; set; }
        [Required]
        [Display(Name = "Item Name")]
        public Int64 ItemId { get; set; }
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
        [Display(Name = "Current Total Stock")]
        public int CurrentTotalStock { get; set; }
        [Display(Name = "Total Damage Item")]
        [Required]
        public int TotalDamageItem { get; set; }
        [Display(Name = "Reason Of Damage")]
        [Required]
        public string ReasonOfDamage { get; set; }



        public static implicit operator DamageItemDeatilsCRUDViewModel(DamageItemDeatils _DamageItemDeatils)
        {
            return new DamageItemDeatilsCRUDViewModel
            {
                Id = _DamageItemDeatils.Id,
                ItemId = _DamageItemDeatils.ItemId,
                TotalDamageItem = _DamageItemDeatils.TotalDamageItem,
                ReasonOfDamage = _DamageItemDeatils.ReasonOfDamage,


                CreatedDate = _DamageItemDeatils.CreatedDate,
                ModifiedDate = _DamageItemDeatils.ModifiedDate,
                CreatedBy = _DamageItemDeatils.CreatedBy,
                ModifiedBy = _DamageItemDeatils.ModifiedBy,
                Cancelled = _DamageItemDeatils.Cancelled
            };
        }

        public static implicit operator DamageItemDeatils(DamageItemDeatilsCRUDViewModel vm)
        {
            return new DamageItemDeatils
            {
                Id = vm.Id,
                ItemId = vm.ItemId,
                TotalDamageItem = vm.TotalDamageItem,
                ReasonOfDamage = vm.ReasonOfDamage,

                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled
            };
        }
    }
}
