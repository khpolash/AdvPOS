using System;
using System.ComponentModel.DataAnnotations;

namespace AdvPOS.Models.EmailConfigViewModel
{
    public class EmailConfigCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public Int64 Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Hostname { get; set; }
        [Required]
        public int Port { get; set; }
        [Display(Name = "SSL Enabled")]
        public bool SSLEnabled { get; set; }
        [Display(Name = "Sender Full Name")]
        public string SenderFullName { get; set; }
        public bool IsDefault { get; set; }
        public string IsDefaultDisplay { get; set; }

        public static implicit operator EmailConfigCRUDViewModel(EmailConfig _EmailConfig)
        {
            return new EmailConfigCRUDViewModel
            {
                Id = _EmailConfig.Id,
                Email = _EmailConfig.Email,
                Password = _EmailConfig.Password,
                Hostname = _EmailConfig.Hostname,
                Port = _EmailConfig.Port,
                SSLEnabled = _EmailConfig.SSLEnabled,
                SenderFullName = _EmailConfig.SenderFullName,
                IsDefault = _EmailConfig.IsDefault,
                CreatedDate = _EmailConfig.CreatedDate,
                ModifiedDate = _EmailConfig.ModifiedDate,
                CreatedBy = _EmailConfig.CreatedBy,
                ModifiedBy = _EmailConfig.ModifiedBy,
                Cancelled = _EmailConfig.Cancelled,
            };
        }

        public static implicit operator EmailConfig(EmailConfigCRUDViewModel vm)
        {
            return new EmailConfig
            {
                Id = vm.Id,
                Email = vm.Email,
                Password = vm.Password,
                Hostname = vm.Hostname,
                Port = vm.Port,
                SSLEnabled = vm.SSLEnabled,
                SenderFullName = vm.SenderFullName,
                IsDefault = vm.IsDefault,
                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled,
            };
        }
    }
}
