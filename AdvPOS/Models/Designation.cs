using System;

namespace AdvPOS.Models
{
    public class Designation : EntityBase
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }      
    }
}
