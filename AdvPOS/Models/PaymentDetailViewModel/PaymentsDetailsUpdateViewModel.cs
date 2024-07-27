using System;

namespace AdvPOS.Models.PaymentDetailViewModel
{
    public class PaymentDetailUpdateViewModel
    {
        public Int64 Id { get; set; }
        public int Quantity { get; set; }
        public double? UnitPrice { get; set; }
        public double? TotalAmount { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }


        public static implicit operator PaymentDetailUpdateViewModel(PaymentDetail _PaymentDetail)
        {
            return new PaymentDetailUpdateViewModel
            {
                Id = _PaymentDetail.Id,
                Quantity = _PaymentDetail.Quantity,
                UnitPrice = _PaymentDetail.UnitPrice,
                TotalAmount = _PaymentDetail.TotalAmount,
                ModifiedDate = _PaymentDetail.ModifiedDate,
                ModifiedBy = _PaymentDetail.ModifiedBy,
            };
        }

        public static implicit operator PaymentDetail(PaymentDetailUpdateViewModel vm)
        {
            return new PaymentDetail
            {
                Id = vm.Id,
                Quantity = vm.Quantity,
                UnitPrice = vm.UnitPrice,
                TotalAmount = vm.TotalAmount,
                ModifiedDate = vm.ModifiedDate,
                ModifiedBy = vm.ModifiedBy,
            };
        }
    }
}
