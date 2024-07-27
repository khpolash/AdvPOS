using System;

namespace AdvPOS.Models
{
    public class CustomerInfo : EntityBase
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public Int64 Type { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string AccountNo { get; set; }
        public string Notes { get; set; }
        public string Address { get; set; }
        public string AddressPostcode { get; set; }
        public string BillingAddress { get; set; }
        public string BillingAddressPostcode { get; set; }
    }
}
