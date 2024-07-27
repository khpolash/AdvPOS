using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace AdvPOS.Models.CompanyInfoViewModel
{
    public class CompanyInfoCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        public Int64 Id { get; set; }
        [Display(Name = "Company Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Company Logo")]
        public string Logo { get; set; }
        public IFormFile CompanyLogo { get; set; }
        public string InvoiceNoPrefix { get; set; }
        public string QuoteNoPrefix { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        [Display(Name = "Shop No")]
        public string ShopNo { get; set; }
        [Display(Name = "Street Name")]
        public string StreetName { get; set; }
        [Display(Name = "Post Code")]
        public string PostCode { get; set; }
        public string Office { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        [Display(Name = "Host Name")]
        public string HostName { get; set; }
        [Display(Name = "Terms And Condition")]
        public string TermsAndCondition { get; set; }
        [Display(Name = "Terms And Condition Item Sale")]
        public string TermsAndConditionItemSale { get; set; }
        [Display(Name = "Company Number")]
        public string CompanyNumber { get; set; }
        [Display(Name = "Vat Number")]
        public string VatNumber { get; set; }
        [Display(Name = "Card Percentage")]
        public string CardPercentage { get; set; }
        [Display(Name = "Vat")]
        public bool IsVat { get; set; }
        [Display(Name = "Whitelist IP")]
        public string WhitelistIP { get; set; }


        public static implicit operator CompanyInfoCRUDViewModel(CompanyInfo _CompanyInfo)
        {
            return new CompanyInfoCRUDViewModel
            {
                Id = _CompanyInfo.Id,
                Name = _CompanyInfo.Name,
                Logo = _CompanyInfo.Logo,
                InvoiceNoPrefix = _CompanyInfo.InvoiceNoPrefix,
                QuoteNoPrefix = _CompanyInfo.QuoteNoPrefix,
                Address = _CompanyInfo.Address,
                City = _CompanyInfo.City,
                Country = _CompanyInfo.Country,
                Phone = _CompanyInfo.Phone,
                Email = _CompanyInfo.Email,
                Fax = _CompanyInfo.Fax,
                Website = _CompanyInfo.Website,
                ShopNo = _CompanyInfo.ShopNo,
                StreetName = _CompanyInfo.StreetName,
                PostCode = _CompanyInfo.PostCode,
                Office = _CompanyInfo.Office,
                Mobile = _CompanyInfo.Mobile,
                Password = _CompanyInfo.Password,
                HostName = _CompanyInfo.HostName,
                TermsAndCondition = _CompanyInfo.TermsAndCondition,
                TermsAndConditionItemSale = _CompanyInfo.TermsAndConditionItemSale,
                CompanyNumber = _CompanyInfo.CompanyNumber,
                VatNumber = _CompanyInfo.VatNumber,
                CardPercentage = _CompanyInfo.CardPercentage,
                IsVat = _CompanyInfo.IsVat,
                WhitelistIP = _CompanyInfo.WhitelistIP,
                CreatedDate = _CompanyInfo.CreatedDate,
                ModifiedDate = _CompanyInfo.ModifiedDate,
                CreatedBy = _CompanyInfo.CreatedBy,
                ModifiedBy = _CompanyInfo.ModifiedBy,
                Cancelled = _CompanyInfo.Cancelled,
            };
        }

        public static implicit operator CompanyInfo(CompanyInfoCRUDViewModel vm)
        {
            return new CompanyInfo
            {
                Id = vm.Id,
                Name = vm.Name,
                Logo = vm.Logo,
                InvoiceNoPrefix = vm.InvoiceNoPrefix,
                QuoteNoPrefix = vm.QuoteNoPrefix,
                Address = vm.Address,
                City = vm.City,
                Country = vm.Country,
                Phone = vm.Phone,
                Email = vm.Email,
                Fax = vm.Fax,
                Website = vm.Website,
                ShopNo = vm.ShopNo,
                StreetName = vm.StreetName,
                PostCode = vm.PostCode,
                Office = vm.Office,
                Mobile = vm.Mobile,
                Password = vm.Password,
                HostName = vm.HostName,
                TermsAndCondition = vm.TermsAndCondition,
                TermsAndConditionItemSale = vm.TermsAndConditionItemSale,
                CompanyNumber = vm.CompanyNumber,
                VatNumber = vm.VatNumber,
                CardPercentage = vm.CardPercentage,
                IsVat = vm.IsVat,
                WhitelistIP = vm.WhitelistIP,
                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled,
            };
        }
    }
}
