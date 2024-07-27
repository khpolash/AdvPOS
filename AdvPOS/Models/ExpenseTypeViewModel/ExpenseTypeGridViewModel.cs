using System;

namespace AdvPOS.Models.ExpenseTypeViewModel
{
    public class ExpenseTypeGridViewModel : EntityBase
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

