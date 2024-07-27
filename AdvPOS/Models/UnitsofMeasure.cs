using System;

namespace AdvPOS.Models
{
    public class UnitsofMeasure : EntityBase
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
