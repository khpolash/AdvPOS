using System;
using System.ComponentModel.DataAnnotations;

namespace AdvPOS.Models.PaymentStatusViewModel
{
    public class PaymentStatusCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public Int64 Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }


        public static implicit operator PaymentStatusCRUDViewModel(PaymentStatus _PaymentStatus)
        {
            return new PaymentStatusCRUDViewModel
            {
                Id = _PaymentStatus.Id,
                Name = _PaymentStatus.Name,
                Description = _PaymentStatus.Description,
                CreatedDate = _PaymentStatus.CreatedDate,
                ModifiedDate = _PaymentStatus.ModifiedDate,
                CreatedBy = _PaymentStatus.CreatedBy,
                ModifiedBy = _PaymentStatus.ModifiedBy,
                Cancelled = _PaymentStatus.Cancelled,

            };
        }

        public static implicit operator PaymentStatus(PaymentStatusCRUDViewModel vm)
        {
            return new PaymentStatus
            {
                Id = vm.Id,
                Name = vm.Name,
                Description = vm.Description,
                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled,
            };
        }
    }
}
