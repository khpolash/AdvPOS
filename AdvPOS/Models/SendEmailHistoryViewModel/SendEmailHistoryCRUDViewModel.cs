using System;
using System.ComponentModel.DataAnnotations;

namespace AdvPOS.Models.SendEmailHistoryViewModel
{
    public class SendEmailHistoryCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public Int64 Id { get; set; }
        public Int64 InvoiceId { get; set; }
        public string SenderEmail { get; set; }
        public string ReceiverEmail { get; set; }
        public string Result { get; set; }
        public string UserName { get; set; }

        public static implicit operator SendEmailHistoryCRUDViewModel(SendEmailHistory _SendEmailHistory)
        {
            return new SendEmailHistoryCRUDViewModel
            {
                Id = _SendEmailHistory.Id,
                InvoiceId = _SendEmailHistory.InvoiceId,
                SenderEmail = _SendEmailHistory.SenderEmail,
                ReceiverEmail = _SendEmailHistory.ReceiverEmail,
                Result = _SendEmailHistory.Result,
                CreatedDate = _SendEmailHistory.CreatedDate,
                ModifiedDate = _SendEmailHistory.ModifiedDate,
                CreatedBy = _SendEmailHistory.CreatedBy,
                ModifiedBy = _SendEmailHistory.ModifiedBy,
                Cancelled = _SendEmailHistory.Cancelled,
            };
        }

        public static implicit operator SendEmailHistory(SendEmailHistoryCRUDViewModel vm)
        {
            return new SendEmailHistory
            {
                Id = vm.Id,
                InvoiceId = vm.InvoiceId,
                SenderEmail = vm.SenderEmail,
                ReceiverEmail = vm.ReceiverEmail,
                Result = vm.Result,
                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled,
            };
        }
    }
}
