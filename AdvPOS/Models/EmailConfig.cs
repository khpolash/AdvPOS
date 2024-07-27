using System;

namespace AdvPOS.Models
{
    public class EmailConfig : EntityBase
    {
        public Int64 Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Hostname { get; set; }
        public int Port { get; set; }
        public bool SSLEnabled { get; set; }
        public string SenderFullName { get; set; }
        public bool IsDefault { get; set; }
    }
}
