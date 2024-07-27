using System;
using System.ComponentModel.DataAnnotations;

namespace AdvPOS.Models.CurrencyViewModel
{
    public class CurrencyCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public Int64 Id { get; set; }
        public Int64 OldId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Code { get; set; }
        public string Symbol { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        [Display(Name = "Is Default")]
        public bool IsDefault { get; set; }
        public string IsDefaultDisplay { get; set; }


        public static implicit operator CurrencyCRUDViewModel(Currency _Currency)
        {
            return new CurrencyCRUDViewModel
            {
                Id = _Currency.Id,
                Name = _Currency.Name,
                Code = _Currency.Code,
                Symbol = _Currency.Symbol,
                Country = _Currency.Country,
                Description = _Currency.Description,
                IsDefault = _Currency.IsDefault,
                CreatedDate = _Currency.CreatedDate,
                ModifiedDate = _Currency.ModifiedDate,
                CreatedBy = _Currency.CreatedBy,
                ModifiedBy = _Currency.ModifiedBy,
                Cancelled = _Currency.Cancelled,

            };
        }

        public static implicit operator Currency(CurrencyCRUDViewModel vm)
        {
            return new Currency
            {
                Id = vm.Id,
                Name = vm.Name,
                Code = vm.Code,
                Symbol = vm.Symbol,
                Country = vm.Country,
                Description = vm.Description,
                IsDefault = vm.IsDefault,
                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled,

            };
        }
    }
}
