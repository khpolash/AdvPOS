using System;
using System.ComponentModel.DataAnnotations;

namespace AdvPOS.Models.UnitsofMeasureViewModel
{
    public class UnitsofMeasureCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public Int64 Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string CurrentURL { get; set; }


        public static implicit operator UnitsofMeasureCRUDViewModel(UnitsofMeasure _UnitsofMeasure)
        {
            return new UnitsofMeasureCRUDViewModel
            {
                Id = _UnitsofMeasure.Id,
                Name = _UnitsofMeasure.Name,
                Description = _UnitsofMeasure.Description,
                CreatedDate = _UnitsofMeasure.CreatedDate,
                ModifiedDate = _UnitsofMeasure.ModifiedDate,
                CreatedBy = _UnitsofMeasure.CreatedBy,
                ModifiedBy = _UnitsofMeasure.ModifiedBy,
                Cancelled = _UnitsofMeasure.Cancelled,

            };
        }

        public static implicit operator UnitsofMeasure(UnitsofMeasureCRUDViewModel vm)
        {
            return new UnitsofMeasure
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
