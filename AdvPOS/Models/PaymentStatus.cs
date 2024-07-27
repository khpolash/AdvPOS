using System;

namespace AdvPOS.Models
{
    public class PaymentStatus : EntityBase
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
