using System;

namespace AdvPOS.Models.EmailConfigViewModel
{
    public class EmailConfigGridViewModel : EntityBase
    {
        public Int64 Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Hostname { get; set; }
        public string Port { get; set; }
    }
}

