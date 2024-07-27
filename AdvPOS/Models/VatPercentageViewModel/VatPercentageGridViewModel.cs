using System;

namespace AdvPOS.Models.VatPercentageViewModel
{
    public class VatPercentageGridViewModel : EntityBase
    {
                public Int64 Id { get; set; }
        public string Name { get; set; }
        public double Percentage { get; set; }
        public bool IsDefault { get; set; }


    }
}

