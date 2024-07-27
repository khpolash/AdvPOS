using AdvPOS.Models.PaymentViewModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace AdvPOS.Models.PaymentDetailViewModel
{
    public class PaymentDetailCRUDViewModel : EntityBase
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
        public PaymentCRUDViewModel PaymentCRUDViewModel { get; set; }


        public static implicit operator PaymentDetailCRUDViewModel(PaymentDetail _PaymentDetail)
        {
            return new PaymentDetailCRUDViewModel
            {
                Id = _PaymentDetail.Id,
                PaymentId = _PaymentDetail.PaymentId,
                ItemId = _PaymentDetail.ItemId,
                ItemName = _PaymentDetail.ItemName,
                Quantity = _PaymentDetail.Quantity,
                UnitPrice = _PaymentDetail.UnitPrice,                
                ItemVAT = _PaymentDetail.ItemVAT,
                ItemVATAmount = _PaymentDetail.ItemVATAmount,
                ItemDiscount = _PaymentDetail.ItemDiscount,
                ItemDiscountAmount = _PaymentDetail.ItemDiscountAmount,
                TotalAmount = _PaymentDetail.TotalAmount,
                IsReturn = _PaymentDetail.IsReturn,
                
                CreatedDate = _PaymentDetail.CreatedDate,
                ModifiedDate = _PaymentDetail.ModifiedDate,
                CreatedBy = _PaymentDetail.CreatedBy,
                ModifiedBy = _PaymentDetail.ModifiedBy,
                Cancelled = _PaymentDetail.Cancelled,
            };
        }

        public static implicit operator PaymentDetail(PaymentDetailCRUDViewModel vm)
        {
            return new PaymentDetail
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
