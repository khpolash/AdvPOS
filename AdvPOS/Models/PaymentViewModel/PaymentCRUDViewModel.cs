using System;
using System.ComponentModel.DataAnnotations;

namespace AdvPOS.Models.PaymentViewModel
{
    public class PaymentCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public Int64 Id { get; set; }
        [Display(Name = "Customer")]
        [Required]
        public Int64 CustomerId { get; set; }
        public string CustomerName { get; set; }
        [Display(Name = "Invoice No")]
        public string InvoiceNo { get; set; }
        [Display(Name = "Quote No")]
        public string QuoteNo { get; set; }
        [Display(Name = "Shipping Charge")]
        public double CommonCharge { get; set; }
        [Display(Name = "Discount(%)")]
        public double Discount { get; set; }
        public double DiscountAmount { get; set; }
        [Display(Name = "Tax(%)")]
        public double VAT { get; set; }
        [Display(Name = "VAT Amount")]
        public double VATAmount { get; set; }
        [Display(Name = "Sub Total")]
        public double SubTotal { get; set; }
        [Display(Name = "Grand Total")]
        public double? GrandTotal { get; set; }
        [Display(Name = "Paid Amount")]
        public double PaidAmount { get; set; }
        [Display(Name = "Due Amount")]
        public double DueAmount { get; set; }
        [Display(Name = "Changed Amount")]
        public double ChangedAmount { get; set; }
        [Display(Name = "Currency")]
        public Int64 CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        [Display(Name = "Currency")]
        public Int64 BranchId { get; set; }
        public string BranchName { get; set; }
        public string CurrencySymbol { get; set; }
        public int MedicineSellState { get; set; } = 0;
        [Display(Name = "Payment Status")]
        public Int64 PaymentStatus { get; set; }
        public string PaymentStatusDisplay { get; set; }
        public string CurrentURL { get; set; }
        public int Category { get; set; }
        [Display(Name = "Purchase Order Number")]
        public string PurchaseOrderNumber { get; set; }
        [Display(Name = "Customer Note")]
        public string CustomerNote { get; set; }
        [Display(Name = "Private Note")]
        public string PrivateNote { get; set; }
        public int ReturnType { get; set; }
        [Display(Name = "Item Barcode")]
        public string ItemBarcode { get; set; }
        [Display(Name = "VAT")]
        public string IsVat { get; set; }
        [Display(Name = "Prevous Balance")]
        public double PrevousBalance { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public Int64 QuoteNoRef { get; set; }
        [Display(Name = "Return Note")]
        public string ReturnNote { get; set; }
        public bool IsSaveAndPrint { get; set; }

        public static implicit operator PaymentCRUDViewModel(Payment _Payments)
        {
            return new PaymentCRUDViewModel
            {
                Id = _Payments.Id,
                CustomerId = _Payments.CustomerId,
                InvoiceNo = _Payments.InvoiceNo,
                QuoteNo = _Payments.QuoteNo,
                CommonCharge = _Payments.CommonCharge,
                Discount = _Payments.Discount,
                DiscountAmount = _Payments.DiscountAmount,
                VAT = _Payments.VAT,
                VATAmount = _Payments.VATAmount,
                SubTotal = _Payments.SubTotal,
                GrandTotal = _Payments.GrandTotal,
                PaidAmount = _Payments.PaidAmount,
                DueAmount = _Payments.DueAmount,
                ChangedAmount = _Payments.ChangedAmount,
                CurrencyId = _Payments.CurrencyId,
                BranchId = _Payments.BranchId,
                PaymentStatus = _Payments.PaymentStatus,
                Category = _Payments.Category,
                PurchaseOrderNumber = _Payments.PurchaseOrderNumber,
                CustomerNote = _Payments.CustomerNote,
                PrivateNote = _Payments.PrivateNote,
                ReturnType = _Payments.ReturnType,

                CreatedDate = _Payments.CreatedDate,
                ModifiedDate = _Payments.ModifiedDate,
                CreatedBy = _Payments.CreatedBy,
                ModifiedBy = _Payments.ModifiedBy,
                Cancelled = _Payments.Cancelled,
            };
        }

        public static implicit operator Payment(PaymentCRUDViewModel vm)
        {
            return new Payment
            {
                Id = vm.Id,
                CustomerId = vm.CustomerId,
                InvoiceNo = vm.InvoiceNo,
                QuoteNo = vm.QuoteNo,
                CommonCharge = vm.CommonCharge,
                Discount = vm.Discount,
                DiscountAmount = vm.DiscountAmount,
                VAT = vm.VAT,
                VATAmount = vm.VATAmount,
                SubTotal = vm.SubTotal,
                GrandTotal = vm.GrandTotal,
                PaidAmount = vm.PaidAmount,
                DueAmount = vm.DueAmount,
                ChangedAmount = vm.ChangedAmount,
                CurrencyId = vm.CurrencyId,
                BranchId = vm.BranchId,
                PaymentStatus = vm.PaymentStatus,
                Category = vm.Category,
                PurchaseOrderNumber = vm.PurchaseOrderNumber,
                CustomerNote = vm.CustomerNote,
                PrivateNote = vm.PrivateNote,
                ReturnType = vm.ReturnType,

                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled,
            };
        }
    }
}


