using System;

namespace AdvPOS.Models.CustomerInfoViewModel
{
    public class CustomerInfoGridViewModel : EntityBase
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string BillingAddress { get; set; }


    }
}

