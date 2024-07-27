using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace AdvPOS.Models.EmailConfigViewModel
{
    public class SendEmailViewModel
    {
        public Int64 InvoiceId { get; set; }
        [Display(Name = "Sender Email")]
        [Required]
        public Int64 SenderEmailId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool IsSSL { get; set; }
        [Display(Name = "Sender Email")]
        [Required]
        public string SenderEmail { get; set; }
        public string SenderFullName { get; set; }
        [Display(Name = "Subject")]
        [Required]
        public string Subject { get; set; }
        [Display(Name = "Body")]
        [Required]
        public string Body { get; set; }
        [Display(Name = "Receiver Email")]
        [Required]
        public Int64 ReceiverEmailId { get; set; }
        [Display(Name = "Receiver Email")]
        [Required]
        public string ReceiverEmail { get; set; }
        public string ReceiverFullName { get; set; }
        public string FilePath { get; set; }
        public Stream FileStream { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public Int64 IsHideCompanyInfo { get; set; }
        public Int64 InvoiceDocType { get; set; }

        public static implicit operator SendEmailViewModel(EmailConfigCRUDViewModel _EmailConfigCRUDViewModel)
        {
            return new SendEmailViewModel
            {
                SenderEmail = _EmailConfigCRUDViewModel.Email,
                UserName = _EmailConfigCRUDViewModel.Email,
                Password = _EmailConfigCRUDViewModel.Password,
                Host = _EmailConfigCRUDViewModel.Hostname,
                Port = _EmailConfigCRUDViewModel.Port,
                SenderFullName = _EmailConfigCRUDViewModel.SenderFullName,
            };
        }
    }
}
