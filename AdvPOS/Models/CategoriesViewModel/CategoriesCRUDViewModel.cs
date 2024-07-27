using System;
using System.ComponentModel.DataAnnotations;

namespace AdvPOS.Models.CategoriesViewModel
{
    public class CategoriesCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public Int64 Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string CurrentURL { get; set; }

        public static implicit operator CategoriesCRUDViewModel(Categories _Categories)
        {
            return new CategoriesCRUDViewModel
            {
                Id = _Categories.Id,
                Name = _Categories.Name,
                Description = _Categories.Description,
                CreatedDate = _Categories.CreatedDate,
                ModifiedDate = _Categories.ModifiedDate,
                CreatedBy = _Categories.CreatedBy,
                ModifiedBy = _Categories.ModifiedBy,
                Cancelled = _Categories.Cancelled,
            };
        }

        public static implicit operator Categories(CategoriesCRUDViewModel vm)
        {
            return new Categories
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
