using System;
using System.ComponentModel.DataAnnotations;

namespace AdvPOS.Models.BranchViewModel
{
    public class BranchCRUDViewModel : EntityBase
    {
        [Display(Name = "SL"), Required]
        public Int64 Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Contact Person"), Required]
        public string ContactPerson { get; set; }
        [Display(Name = "Phone Number"), Required]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }


        public static implicit operator BranchCRUDViewModel(Branch _Branch)
        {
            return new BranchCRUDViewModel
            {
                Id = _Branch.Id,
                Name = _Branch.Name,
                ContactPerson = _Branch.ContactPerson,
                PhoneNumber = _Branch.PhoneNumber,
                Address = _Branch.Address,
                ShortDescription = _Branch.ShortDescription,
                CreatedDate = _Branch.CreatedDate,
                ModifiedDate = _Branch.ModifiedDate,
                CreatedBy = _Branch.CreatedBy,
                ModifiedBy = _Branch.ModifiedBy,
                Cancelled = _Branch.Cancelled,
            };
        }

        public static implicit operator Branch(BranchCRUDViewModel vm)
        {
            return new Branch
            {
                Id = vm.Id,
                Name = vm.Name,
                ContactPerson = vm.ContactPerson,
                PhoneNumber = vm.PhoneNumber,
                Address = vm.Address,
                ShortDescription = vm.ShortDescription,
                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled,
            };
        }
    }
}
