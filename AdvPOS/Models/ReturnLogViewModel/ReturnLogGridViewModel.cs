using System;

namespace AdvPOS.Models.ReturnLogViewModel
{
    public class ReturnLogGridViewModel : EntityBase
    {
                public Int64 Id { get; set; }
        public Int64 RefId { get; set; }
        public Int64 CustomerId { get; set; }
        public string TranType { get; set; }
        public string Note { get; set; }


    }
}

