using System;
using System.ComponentModel.DataAnnotations;

namespace AdvPOS.Models.CustomerTypeViewModel
{
    public class CustomerTypeCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }



        public static implicit operator CustomerTypeCRUDViewModel(CustomerType _CustomerType)
        {
            return new CustomerTypeCRUDViewModel
            {
                Id = _CustomerType.Id,
                Name = _CustomerType.Name,
                Description = _CustomerType.Description,
                CreatedDate = _CustomerType.CreatedDate,
                ModifiedDate = _CustomerType.ModifiedDate,
                CreatedBy = _CustomerType.CreatedBy,
                ModifiedBy = _CustomerType.ModifiedBy,
                Cancelled = _CustomerType.Cancelled,

            };
        }

        public static implicit operator CustomerType(CustomerTypeCRUDViewModel vm)
        {
            return new CustomerType
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
