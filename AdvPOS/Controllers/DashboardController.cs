using AdvPOS.Data;
using AdvPOS.Models.DashboardViewModel;
using AdvPOS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace AdvPOS.Controllers
{
    [Authorize]
    //[Route("[controller]/[action]")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ISalesService _iSalesService;
        DateTime TodayZeroHour = DateTime.Today;
        DateTime Today24Hour = DateTime.Today.AddDays(1).AddTicks(-1);
        public DashboardController(ApplicationDbContext context, ISalesService iSalesService)
        {
            _context = context;
            _iSalesService = iSalesService;
        }

        [Authorize(Roles = Pages.MainMenu.Dashboard.RoleName)]
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                DashboardSummaryViewModel vm = new();
                var _Payment = _context.Payment.Where(x => x.Cancelled == false && x.Category == 1);
                var _Items = _context.Items.Where(x => x.Cancelled == false);
                var _CustomerInfo = _context.CustomerInfo.Where(x => x.Cancelled == false);
                var _Supplier = _context.Supplier.Where(x => x.Cancelled == false);

                DateTime _Now = DateTime.Now;
                var CMStartDate = new DateTime(_Now.Year, _Now.Month, 1);
                var CMEndDate = CMStartDate.AddMonths(1).AddDays(-1);

                vm.InvoiceToday = _Payment.Where(x => x.CreatedDate >= TodayZeroHour && x.CreatedDate <= Today24Hour).Count();
                vm.InvoiceThisMonth = _Payment.Where(x => x.CreatedDate >= CMStartDate && x.CreatedDate <= CMEndDate).Count();
                vm.SalesToday = (long)_Payment.Where(x => x.CreatedDate >= TodayZeroHour && x.CreatedDate <= Today24Hour).Sum(x => x.GrandTotal);
                vm.SalesThisMonth = (long)_Payment.Where(x => x.CreatedDate >= CMStartDate && x.CreatedDate <= CMEndDate).Sum(x => x.GrandTotal);

                vm.TotalItem = _Items.Count();
                vm.TotalItemQuantity = _Items.Sum(x => x.Quantity);
                vm.TotalCustomer = _CustomerInfo.Count();
                vm.TotalSupplier = _Supplier.Count();

                vm.listRecentInvoiceViewModel = GetRecentInvoicesList();
                var _UserProfile = _context.UserProfile.ToList();
                vm.TotalUser = _UserProfile.Count();
                vm.TotalActive = _UserProfile.Where(x => x.Cancelled == false).Count();
                vm.TotalInActive = _UserProfile.Where(x => x.Cancelled == true).Count();
                vm.listUserProfile = _UserProfile.Where(x => x.Cancelled == false).OrderByDescending(x => x.CreatedDate).Take(10).ToList();

                return View(vm);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<RecentInvoiceViewModel> GetRecentInvoicesList()
        {
            try
            {
                var result = (from obj in _context.Payment
                              join _CustomerInfo in _context.CustomerInfo on obj.CustomerId equals _CustomerInfo.Id
                              where (obj.Cancelled == false && obj.Category == 1)
                              select new RecentInvoiceViewModel
                              {
                                  Id = obj.Id,
                                  InvoiceNo = obj.InvoiceNo,
                                  CustomerName = _CustomerInfo.Name,
                                  Amount = (double)obj.GrandTotal,
                                  PaidAmount = obj.PaidAmount,
                                  DueAmount = obj.DueAmount,
                                  PaymentStatus = obj.DueAmount.ToString(),
                                  InvoiceDate = obj.CreatedDate
                              }).OrderByDescending(x => x.InvoiceDate).Take(10).ToList();


                foreach (var item in result)
                {
                    if (Convert.ToDouble(item.PaymentStatus) <= 0)
                        item.PaymentStatus = "Paid";
                    else
                        item.PaymentStatus = "Unpaid";
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public JsonResult GetHighestEarningByItem()
        {
            var result = GetGroupByEarnedList().OrderByDescending(x => x.ItemTotal).Take(10).ToList();
            return new JsonResult(result.ToDictionary(x => x.ItemName, x => x.ItemTotal));
        }
        [HttpGet]
        public JsonResult GetMonthlySales()
        {
            var result = _iSalesService.SalesTransactionBy("Month");
            return new JsonResult(result.ToDictionary(x => x.TranDate, x => x.TotalEarned));
        }
        private List<GroupByViewModel> GetGroupByEarnedList()
        {
            var PaymentDetailGroupBy = _context.PaymentDetail.Where(x => x.Cancelled == false).GroupBy(p => p.ItemId).Select(g => new
            {
                ItemId = g.Key,
                TotalAmount = g.Sum(t => t.TotalAmount)
            }).ToList();

            var result = (from _PaymentDetailGroupBy in PaymentDetailGroupBy
                          join _Items in _context.Items on _PaymentDetailGroupBy.ItemId equals _Items.Id
                          where _PaymentDetailGroupBy.ItemId == _Items.Id
                          select new GroupByViewModel
                          {
                              ItemName = _Items.Id + "-" + _Items.Name,
                              ItemTotal = _PaymentDetailGroupBy.TotalAmount,
                          }).ToList();

            return result;
        }
    }
}