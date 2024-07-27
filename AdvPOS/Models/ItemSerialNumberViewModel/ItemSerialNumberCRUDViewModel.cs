using System;
using System.ComponentModel.DataAnnotations;

namespace AdvPOS.Models.ItemSerialNumberViewModel
{
    public class ItemSerialNumberCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public Int64 Id { get; set; }
        public Int64 PaymentDetailId { get; set; }
        public string SerialNumber { get; set; }
        public string Note { get; set; }


        public static implicit operator ItemSerialNumberCRUDViewModel(ItemSerialNumber _ItemSerialNumber)
        {
            return new ItemSerialNumberCRUDViewModel
            {
                Id = _ItemSerialNumber.Id,
                PaymentDetailId = _ItemSerialNumber.PaymentDetailId,
                SerialNumber = _ItemSerialNumber.SerialNumber,
                Note = _ItemSerialNumber.Note,
                CreatedDate = _ItemSerialNumber.CreatedDate,
                ModifiedDate = _ItemSerialNumber.ModifiedDate,
                CreatedBy = _ItemSerialNumber.CreatedBy,
                ModifiedBy = _ItemSerialNumber.ModifiedBy,
                Cancelled = _ItemSerialNumber.Cancelled,

            };
        }

        public static implicit operator ItemSerialNumber(ItemSerialNumberCRUDViewModel vm)
        {
            return new ItemSerialNumber
            {
                Id = vm.Id,
                PaymentDetailId = vm.PaymentDetailId,
                SerialNumber = vm.SerialNumber,
                Note = vm.Note,
                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled,

            };
        }
    }
}
