using System;
using System.ComponentModel.DataAnnotations;

namespace AdvPOS.Models.WarehouseViewModel
{
    public class WarehouseCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public Int64 Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string CurrentURL { get; set; }


        public static implicit operator WarehouseCRUDViewModel(Warehouse _Warehouse)
        {
            return new WarehouseCRUDViewModel
            {
                Id = _Warehouse.Id,
                Name = _Warehouse.Name,
                Description = _Warehouse.Description,
                CreatedDate = _Warehouse.CreatedDate,
                ModifiedDate = _Warehouse.ModifiedDate,
                CreatedBy = _Warehouse.CreatedBy,
                ModifiedBy = _Warehouse.ModifiedBy,
                Cancelled = _Warehouse.Cancelled,

            };
        }

        public static implicit operator Warehouse(WarehouseCRUDViewModel vm)
        {
            return new Warehouse
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
