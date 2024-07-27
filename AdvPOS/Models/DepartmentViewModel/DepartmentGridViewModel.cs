using System;

namespace AdvPOS.Models.DepartmentViewModel
{
    public class DepartmentGridViewModel : EntityBase
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
