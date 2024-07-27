using AdvPOS.Models.PurchasesPaymentViewModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace AdvPOS.Models.PurchasesPaymentDetailViewModel
{
    public class PurchasesPaymentDetailCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public Int64 Id { get; set; }
        public Int64 PaymentId { get; set; }
        public Int64 ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public double? UnitPrice { get; set; }
        public double? ItemVAT { get; set; }
        public double? ItemVATAmount { get; set; }
        public double? ItemDiscount { get; set; }
        public double? ItemDiscountAmount { get; set; }
        public double? TotalAmount { get; set; }
        public bool IsReturn { get; set; }
        public string IsReturnDisplay { get; set; }
        public string UserName { get; set; }
        public PurchasesPaymentCRUDViewModel PurchasesPaymentCRUDViewModel { get; set; }


        public static implicit operator PurchasesPaymentDetailCRUDViewModel(PurchasesPaymentDetail _PurchasesPaymentDetail)
        {
            return new PurchasesPaymentDetailCRUDViewModel
            {
                Id = _PurchasesPaymentDetail.Id,
                PaymentId = _PurchasesPaymentDetail.PaymentId,
                ItemId = _PurchasesPaymentDetail.ItemId,
                ItemName = _PurchasesPaymentDetail.ItemName,
                Quantity = _PurchasesPaymentDetail.Quantity,
                UnitPrice = _PurchasesPaymentDetail.UnitPrice,                
                ItemVAT = _PurchasesPaymentDetail.ItemVAT,
                ItemVATAmount = _PurchasesPaymentDetail.ItemVATAmount,
                ItemDiscount = _PurchasesPaymentDetail.ItemDiscount,
                ItemDiscountAmount = _PurchasesPaymentDetail.ItemDiscountAmount,
                TotalAmount = _PurchasesPaymentDetail.TotalAmount,
                IsReturn = _PurchasesPaymentDetail.IsReturn,
                
                CreatedDate = _PurchasesPaymentDetail.CreatedDate,
                ModifiedDate = _PurchasesPaymentDetail.ModifiedDate,
                CreatedBy = _PurchasesPaymentDetail.CreatedBy,
                ModifiedBy = _PurchasesPaymentDetail.ModifiedBy,
                Cancelled = _PurchasesPaymentDetail.Cancelled,
            };
        }

        public static implicit operator PurchasesPaymentDetail(PurchasesPaymentDetailCRUDViewModel vm)
        {
            return new PurchasesPaymentDetail
            {
                Id = vm.Id,
                PaymentId = vm.PaymentId,
                ItemId = vm.ItemId,
                ItemName = vm.ItemName,
                Quantity = vm.Quantity,
                UnitPrice = vm.UnitPrice,
                ItemVAT = vm.ItemVAT,
                ItemVATAmount = vm.ItemVATAmount,
                ItemDiscount = vm.ItemDiscount,
                ItemDiscountAmount = vm.ItemDiscountAmount,
                TotalAmount = vm.TotalAmount,
                IsReturn = vm.IsReturn,

                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled,
            };
        }
    }
}
