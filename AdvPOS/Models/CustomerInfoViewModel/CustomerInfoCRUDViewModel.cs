using System;
using System.ComponentModel.DataAnnotations;

namespace AdvPOS.Models.CustomerInfoViewModel
{
    public class CustomerInfoCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public Int64 Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        public Int64 Type { get; set; }
        public string TypeDisplay { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [Display(Name = "Account No")]
        public string AccountNo { get; set; }
        public string Notes { get; set; }
        public string Address { get; set; }
        [Display(Name = "Postcode")]
        public string AddressPostcode { get; set; }
        public bool UseThisAsBillingAddress { get; set; }
        [Display(Name = "Billing Address")]
        public string BillingAddress { get; set; }
        [Display(Name = "Postcode")]
        public string BillingAddressPostcode { get; set; }
        public string AlertMessage { get; set; }


        public static implicit operator CustomerInfoCRUDViewModel(CustomerInfo _CustomerInfo)
        {
            return new CustomerInfoCRUDViewModel
            {
                Id = _CustomerInfo.Id,
                Name = _CustomerInfo.Name,
                CompanyName = _CustomerInfo.CompanyName,
                Type = _CustomerInfo.Type,
                Phone = _CustomerInfo.Phone,
                Email = _CustomerInfo.Email,
                AccountNo = _CustomerInfo.AccountNo,
                Notes = _CustomerInfo.Notes,
                Address = _CustomerInfo.Address,
                AddressPostcode = _CustomerInfo.AddressPostcode,
                BillingAddress = _CustomerInfo.BillingAddress,
                BillingAddressPostcode = _CustomerInfo.BillingAddressPostcode,
                CreatedDate = _CustomerInfo.CreatedDate,
                ModifiedDate = _CustomerInfo.ModifiedDate,
                CreatedBy = _CustomerInfo.CreatedBy,
                ModifiedBy = _CustomerInfo.ModifiedBy,
                Cancelled = _CustomerInfo.Cancelled,
            };
        }

        public static implicit operator CustomerInfo(CustomerInfoCRUDViewModel vm)
        {
            return new CustomerInfo
            {
                Id = vm.Id,
                Name = vm.Name,
                CompanyName = vm.CompanyName,
                Type = vm.Type,
                Phone = vm.Phone,
                Email = vm.Email,
                AccountNo = vm.AccountNo,
                Notes = vm.Notes,
                Address = vm.Address,
                AddressPostcode = vm.AddressPostcode,
                BillingAddress = vm.BillingAddress,
                BillingAddressPostcode = vm.BillingAddressPostcode,
                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled,
            };
        }
    }
}
