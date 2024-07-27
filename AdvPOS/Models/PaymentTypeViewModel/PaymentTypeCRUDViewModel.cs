using System;
using System.ComponentModel.DataAnnotations;

namespace AdvPOS.Models.PaymentTypeViewModel
{
    public class PaymentTypeCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public Int64 Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public static implicit operator PaymentTypeCRUDViewModel(PaymentType _PaymentType)
        {
            return new PaymentTypeCRUDViewModel
            {
                Id = _PaymentType.Id,
                Name = _PaymentType.Name,
                Description = _PaymentType.Description,
                CreatedDate = _PaymentType.CreatedDate,
                ModifiedDate = _PaymentType.ModifiedDate,
                CreatedBy = _PaymentType.CreatedBy,
                ModifiedBy = _PaymentType.ModifiedBy,
                Cancelled = _PaymentType.Cancelled
            };
        }

        public static implicit operator PaymentType(PaymentTypeCRUDViewModel vm)
        {
            return new PaymentType
            {
                Id = vm.Id,
                Name = vm.Name,
                Description = vm.Description,
                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled
            };
        }
    }
}
