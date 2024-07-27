using System;
using System.ComponentModel.DataAnnotations;

namespace AdvPOS.Models.VatPercentageViewModel
{
    public class VatPercentageCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public Int64 Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Percentage { get; set; }
        [Display(Name = "Is Default")]
        public bool IsDefault { get; set; }


        public static implicit operator VatPercentageCRUDViewModel(VatPercentage _VatPercentage)
        {
            return new VatPercentageCRUDViewModel
            {
                Id = _VatPercentage.Id,
                Name = _VatPercentage.Name,
                Percentage = _VatPercentage.Percentage,
                IsDefault = _VatPercentage.IsDefault,
                CreatedDate = _VatPercentage.CreatedDate,
                ModifiedDate = _VatPercentage.ModifiedDate,
                CreatedBy = _VatPercentage.CreatedBy,
                ModifiedBy = _VatPercentage.ModifiedBy,
                Cancelled = _VatPercentage.Cancelled,

            };
        }

        public static implicit operator VatPercentage(VatPercentageCRUDViewModel vm)
        {
            return new VatPercentage
            {
                Id = vm.Id,
                Name = vm.Name,
                Percentage = vm.Percentage,
                IsDefault = vm.IsDefault,
                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled,

            };
        }
    }
}
