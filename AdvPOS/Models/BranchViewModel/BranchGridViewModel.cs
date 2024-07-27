using System;

namespace AdvPOS.Models.BranchViewModel
{
    public class BranchGridViewModel : EntityBase
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ShortDescription { get; set; }
    }
}

