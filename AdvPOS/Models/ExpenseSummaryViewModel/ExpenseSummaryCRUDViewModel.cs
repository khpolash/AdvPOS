using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdvPOS.Models.ExpenseSummaryViewModel
{
    public class ExpenseSummaryCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public Int64 Id { get; set; }
        public string Title { get; set; }
        public double GrandTotal { get; set; }
        public double PaidAmount { get; set; }
        public double DueAmount { get; set; }
        public double ChangeAmount { get; set; }
        [Display(Name = "Currency")]
        public Int64 CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public Int64 BranchId { get; set; }
        public string BranchName { get; set; }
        public string CurrencySymbol { get; set; }
        public int Action { get; set; }
        public string CurrentURL { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public ExpenseDetails ExpenseDetails { get; set; }
        public List<ExpenseDetailsCRUDViewModel> listExpenseDetails { get; set; }
        public static implicit operator ExpenseSummaryCRUDViewModel(ExpenseSummary _ExpenseSummary)
        {
            return new ExpenseSummaryCRUDViewModel
            {
                Id = _ExpenseSummary.Id,
                Title = _ExpenseSummary.Title,
                GrandTotal = _ExpenseSummary.GrandTotal,
                PaidAmount = _ExpenseSummary.PaidAmount,
                DueAmount = _ExpenseSummary.DueAmount,
                ChangeAmount = _ExpenseSummary.ChangeAmount,
                CurrencyCode = _ExpenseSummary.CurrencyCode,
                BranchId = _ExpenseSummary.BranchId,
                Action = _ExpenseSummary.Action,
                CreatedDate = _ExpenseSummary.CreatedDate,
                ModifiedDate = _ExpenseSummary.ModifiedDate,
                CreatedBy = _ExpenseSummary.CreatedBy,
                ModifiedBy = _ExpenseSummary.ModifiedBy,
                Cancelled = _ExpenseSummary.Cancelled,
            };
        }

        public static implicit operator ExpenseSummary(ExpenseSummaryCRUDViewModel vm)
        {
            return new ExpenseSummary
            {
                Id = vm.Id,
                Title = vm.Title,
                GrandTotal = vm.GrandTotal,
                PaidAmount = vm.PaidAmount,
                DueAmount = vm.DueAmount,
                ChangeAmount = vm.ChangeAmount,
                CurrencyCode = vm.CurrencyCode,
                BranchId = vm.BranchId,
                Action = vm.Action,
                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled,
            };
        }
    }
}
