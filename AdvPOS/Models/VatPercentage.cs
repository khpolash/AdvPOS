using System;

namespace AdvPOS.Models
{
    public class VatPercentage : EntityBase
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public double Percentage { get; set; }
        public bool IsDefault { get; set; }
    }
}
