using System;
using System.ComponentModel.DataAnnotations;

namespace AdvPOS.Models.PurchasesPaymentViewModel
{
    public class PurchasesPaymentCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public Int64 Id { get; set; }
        [Display(Name = "Supplier")]
        [Required]
        public Int64 SupplierId { get; set; }
        public string SupplierName { get; set; }
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
        [Display(Name = "Branch")]
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
        public string ReturnNote {get; set; }

        public static implicit operator PurchasesPaymentCRUDViewModel(PurchasesPayment _PurchasesPayment)
        {
            return new PurchasesPaymentCRUDViewModel
            {
                Id = _PurchasesPayment.Id,
                SupplierId = _PurchasesPayment.SupplierId,
                InvoiceNo = _PurchasesPayment.InvoiceNo,
                QuoteNo = _PurchasesPayment.QuoteNo,
                CommonCharge = _PurchasesPayment.CommonCharge,
                Discount = _PurchasesPayment.Discount,
                DiscountAmount = _PurchasesPayment.DiscountAmount,
                VAT = _PurchasesPayment.VAT,
                VATAmount = _PurchasesPayment.VATAmount,
                SubTotal = _PurchasesPayment.SubTotal,
                GrandTotal = _PurchasesPayment.GrandTotal,
                PaidAmount = _PurchasesPayment.PaidAmount,
                DueAmount = _PurchasesPayment.DueAmount,
                ChangedAmount = _PurchasesPayment.ChangedAmount,
                CurrencyId = _PurchasesPayment.CurrencyId,
                BranchId = _PurchasesPayment.BranchId,
                PaymentStatus = _PurchasesPayment.PaymentStatus,
                Category = _PurchasesPayment.Category,
                PurchaseOrderNumber = _PurchasesPayment.PurchaseOrderNumber,
                CustomerNote = _PurchasesPayment.CustomerNote,
                PrivateNote = _PurchasesPayment.PrivateNote,
                ReturnType = _PurchasesPayment.ReturnType,

                CreatedDate = _PurchasesPayment.CreatedDate,
                ModifiedDate = _PurchasesPayment.ModifiedDate,
                CreatedBy = _PurchasesPayment.CreatedBy,
                ModifiedBy = _PurchasesPayment.ModifiedBy,
                Cancelled = _PurchasesPayment.Cancelled,
            };
        }

        public static implicit operator PurchasesPayment(PurchasesPaymentCRUDViewModel vm)
        {
            return new PurchasesPayment
            {
                Id = vm.Id,
                SupplierId = vm.SupplierId,
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


