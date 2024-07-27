using System;
using System.ComponentModel.DataAnnotations;

namespace AdvPOS.Models.ExpenseTypeViewModel
{
    public class ExpenseTypeCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public Int64 Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public static implicit operator ExpenseTypeCRUDViewModel(ExpenseType _ExpenseType)
        {
            return new ExpenseTypeCRUDViewModel
            {
                Id = _ExpenseType.Id,
                Name = _ExpenseType.Name,
                Description = _ExpenseType.Description,
                CreatedDate = _ExpenseType.CreatedDate,
                ModifiedDate = _ExpenseType.ModifiedDate,
                CreatedBy = _ExpenseType.CreatedBy,
                ModifiedBy = _ExpenseType.ModifiedBy,
                Cancelled = _ExpenseType.Cancelled,
            };
        }

        public static implicit operator ExpenseType(ExpenseTypeCRUDViewModel vm)
        {
            return new ExpenseType
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
