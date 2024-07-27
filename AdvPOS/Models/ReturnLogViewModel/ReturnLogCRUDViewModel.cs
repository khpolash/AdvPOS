using System;
using System.ComponentModel.DataAnnotations;

namespace AdvPOS.Models.ReturnLogViewModel
{
    public class ReturnLogCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public Int64 Id { get; set; }
        public Int64 RefId { get; set; }
        public string InvoiceNo { get; set; }
        public Int64 CustomerId { get; set; }
        public string CustomerDisplay { get; set; }
        public string TranType { get; set; }
        public string Note { get; set; }
        public string UserName { get; set; }

        public static implicit operator ReturnLogCRUDViewModel(ReturnLog _ReturnLog)
        {
            return new ReturnLogCRUDViewModel
            {
                Id = _ReturnLog.Id,
                RefId = _ReturnLog.RefId,
                InvoiceNo = _ReturnLog.InvoiceNo,
                CustomerId = _ReturnLog.CustomerId,
                TranType = _ReturnLog.TranType,
                Note = _ReturnLog.Note,
                CreatedDate = _ReturnLog.CreatedDate,
                ModifiedDate = _ReturnLog.ModifiedDate,
                CreatedBy = _ReturnLog.CreatedBy,
                ModifiedBy = _ReturnLog.ModifiedBy,
                Cancelled = _ReturnLog.Cancelled,
            };
        }

        public static implicit operator ReturnLog(ReturnLogCRUDViewModel vm)
        {
            return new ReturnLog
            {
                Id = vm.Id,
                RefId = vm.RefId,
                InvoiceNo = vm.InvoiceNo,
                CustomerId = vm.CustomerId,
                TranType = vm.TranType,
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
