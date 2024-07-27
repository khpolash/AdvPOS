using System;
using System.ComponentModel.DataAnnotations;

namespace AdvPOS.Models.SupplierViewModel
{
    public class SupplierCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public Int64 Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string CurrentURL { get; set; }
        public string AlertMessage { get; set; }

        public static implicit operator SupplierCRUDViewModel(Supplier _Supplier)
        {
            return new SupplierCRUDViewModel
            {
                Id = _Supplier.Id,
                Name = _Supplier.Name,
                ContactPerson = _Supplier.ContactPerson,
                Email = _Supplier.Email,
                Phone = _Supplier.Phone,
                Address = _Supplier.Address,
                CreatedDate = _Supplier.CreatedDate,
                ModifiedDate = _Supplier.ModifiedDate,
                CreatedBy = _Supplier.CreatedBy,
                ModifiedBy = _Supplier.ModifiedBy,
                Cancelled = _Supplier.Cancelled,
            };
        }

        public static implicit operator Supplier(SupplierCRUDViewModel vm)
        {
            return new Supplier
            {
                Id = vm.Id,
                Name = vm.Name,
                ContactPerson = vm.ContactPerson,
                Email = vm.Email,
                Phone = vm.Phone,
                Address = vm.Address,
                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled,
            };
        }
    }
}
