using System;
using System.ComponentModel.DataAnnotations;

namespace AdvPOS.Models.ExpenseSummaryViewModel
{
    public class ExpenseDetailsCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public Int64 Id { get; set; }
        public Int64 ExpenseSummaryId { get; set; }
        public Int64 ExpenseTypeId { get; set; }
        public string ExpenseType { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
        public string UserName { get; set; }
        public ExpenseSummaryCRUDViewModel ExpenseSummaryCRUDViewModel { get; set; }
        public static implicit operator ExpenseDetailsCRUDViewModel(ExpenseDetails _ExpenseDetails)
        {
            return new ExpenseDetailsCRUDViewModel
            {
                Id = _ExpenseDetails.Id,
                ExpenseSummaryId = _ExpenseDetails.ExpenseSummaryId,
                ExpenseTypeId = _ExpenseDetails.ExpenseTypeId,
                ExpenseType = _ExpenseDetails.ExpenseType,
                Description = _ExpenseDetails.Description,
                Quantity = _ExpenseDetails.Quantity,
                UnitPrice = _ExpenseDetails.UnitPrice,
                TotalPrice = _ExpenseDetails.TotalPrice,
                CreatedDate = _ExpenseDetails.CreatedDate,
                ModifiedDate = _ExpenseDetails.ModifiedDate,
                CreatedBy = _ExpenseDetails.CreatedBy,
                ModifiedBy = _ExpenseDetails.ModifiedBy,
                Cancelled = _ExpenseDetails.Cancelled,
            };
        }

        public static implicit operator ExpenseDetails(ExpenseDetailsCRUDViewModel vm)
        {
            return new ExpenseDetails
            {
                Id = vm.Id,
                ExpenseSummaryId = vm.ExpenseSummaryId,
                ExpenseTypeId = vm.ExpenseTypeId,
                ExpenseType = vm.ExpenseType,
                Description = vm.Description,
                Quantity = vm.Quantity,
                UnitPrice = vm.UnitPrice,
                TotalPrice = vm.TotalPrice,
                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled,
            };
        }
    }
}
