using AdvPOS.Data;
using AdvPOS.Models.AttendanceViewModel;
using AdvPOS.Models.DashboardViewModel;
using AdvPOS.Models.ItemsViewModel;
using AdvPOS.Models.PaymentViewModel;
using AdvPOS.Pages;
using AdvPOS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace AdvPOS.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class SalesReportController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommon _iCommon;
        private readonly ISalesService _iSalesService;
        public SalesReportController(ApplicationDbContext context, ICommon iCommon, ISalesService iSalesService)
        {
            _context = context;
            _iCommon = iCommon;
            _iSalesService = iSalesService;
        }
        [Authorize(Roles = MainMenu.TransactionByDay.RoleName)]
        [HttpGet]
        public IActionResult SummaryReport()
        {
            try
            {
                return View();
            }
            catch (Exception) { throw; }
        }

        [Authorize(Roles = MainMenu.TransactionByDay.RoleName)]
        [HttpGet]
        public IActionResult TransactionByDay()
        {
            try
            {
                TransactionByViewModel vm = new();
                vm.listTransactionByViewModel = _iSalesService.SalesTransactionBy("Day");
                ViewBag.ReportTitle = "Transaction By Day";
                return View("TransactionBy", vm);
            }
            catch (Exception) { throw; }
        }
        [Authorize(Roles = MainMenu.TransactionByMonth.RoleName)]
        [HttpGet]
        public IActionResult TransactionByMonth()
        {
            try
            {
                TransactionByViewModel vm = new();
                vm.listTransactionByViewModel = _iSalesService.SalesTransactionBy("Month");
                ViewBag.ReportTitle = "Transaction By Month";
                return View("TransactionBy", vm);
            }
            catch (Exception) { throw; }
        }
        [Authorize(Roles = MainMenu.TransactionByYear.RoleName)]
        [HttpGet]
        public IActionResult TransactionByYear()
        {
            try
            {
                TransactionByViewModel vm = new();
                vm.listTransactionByViewModel = _iSalesService.SalesTransactionBy("Year");
                ViewBag.ReportTitle = "Transaction By Year";
                return View("TransactionBy", vm);
            }
            catch (Exception) { throw; }
        }
        [Authorize(Roles = MainMenu.HighInDemand.RoleName)]
        [HttpGet]
        public IActionResult HighInDemand()
        {
            try
            {
                GroupByViewModel vm = new();
                vm.listGroupByViewModel = _iCommon.GetItemDemandList().OrderByDescending(x => x.ItemTotal).Take(20).ToList();
                ViewBag.ReportTitle = "Item: High In Demand";
                return View("ItemTrend", vm);
            }
            catch (Exception) { throw; }
        }
        [Authorize(Roles = MainMenu.LowInDemand.RoleName)]
        [HttpGet]
        public IActionResult LowInDemand()
        {
            try
            {
                GroupByViewModel vm = new();
                vm.listGroupByViewModel = _iCommon.GetItemDemandList().OrderBy(x => x.ItemTotal).Take(20).ToList();
                ViewBag.ReportTitle = "Item: Low In Demand";
                return View("ItemTrend", vm);
            }
            catch (Exception) { throw; }
        }
        [Authorize(Roles = MainMenu.HighestEarning.RoleName)]
        [HttpGet]
        public IActionResult HighestEarning()
        {
            try
            {
                GroupByViewModel vm = new();
                vm.listGroupByViewModel = _iCommon.GetItemEarningList().OrderByDescending(x => x.ItemTotal).Take(20).ToList();
                ViewBag.ReportTitle = "Item: Highest Earning";
                return View("ItemTrend", vm);
            }
            catch (Exception) { throw; }
        }
        [Authorize(Roles = MainMenu.LowestEarning.RoleName)]
        [HttpGet]
        public IActionResult LowestEarning()
        {
            try
            {
                GroupByViewModel vm = new();
                vm.listGroupByViewModel = _iCommon.GetItemEarningList().OrderBy(x => x.ItemTotal).Take(20).ToList();
                ViewBag.ReportTitle = "Item: Lowest Earning";
                return View("ItemTrend", vm);
            }
            catch (Exception) { throw; }
        }

        [Authorize(Roles = MainMenu.PrintBarcode.RoleName)]
        [HttpGet]
        public async Task<IActionResult> PrintBarcode(string FullList, int currentPage, int pageSize = 20)
        {
            BarcodeViewModel _BarcodeViewModel = new BarcodeViewModel();
            var _GetBarcodeList = await _iCommon.GetBarcodeList().ToListAsync();

            ViewBag.TotalPages = (int)Math.Ceiling(decimal.Divide(_GetBarcodeList.Count, pageSize));
            ViewBag.currentPage = currentPage;
            var result = _GetBarcodeList.OrderByDescending(d => d.Id).Skip((currentPage - 1) * pageSize).Take(pageSize);
            ViewBag.TotalShowing = "Showing " + result.Count() + " out of " + _GetBarcodeList.Count + " entries";

            _BarcodeViewModel.listBarcodeViewModel = result.ToList();

            var listCount = _BarcodeViewModel.listBarcodeViewModel.Count / 4;
            var _listBarcodeViewModel = _BarcodeViewModel.listBarcodeViewModel
                .Select((x, i) => new { Index = i, Value = x }).GroupBy(x => x.Index / listCount)
                .Select(x => x.Select(v => v.Value).ToList()).ToList();

            return View(_BarcodeViewModel);
        }

        [Authorize(Roles = MainMenu.PaymentSummaryReport.RoleName)]
        [HttpGet]
        public IActionResult PaymentSummaryReport()
        {
            try
            {
                ViewBag.ddlBranch = new SelectList(_iCommon.GetCommonddlData("Branch"), "Id", "Name");
                return View();
            }
            catch (Exception) { throw; }
        }
        [HttpPost]
        public IActionResult GetDataTablePaymentSummaryList(bool IsFilterData, Int64 BranchId)
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();

                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnAscDesc = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int resultTotal = 0;


                IQueryable<PaymentGridViewModel> _GetGridItem;
                if (IsFilterData)
                {
                    _GetGridItem = _iSalesService.GetPaymentSummaryReportList().Where(obj => obj.BranchId == BranchId);
                }
                else
                {
                    _GetGridItem = _iSalesService.GetPaymentSummaryReportList();
                }

                //Sorting
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnAscDesc)))
                {
                    _GetGridItem = _GetGridItem.OrderBy(sortColumn + " " + sortColumnAscDesc);
                }

                //Search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    searchValue = searchValue.ToLower();
                    _GetGridItem = _GetGridItem.Where(obj => obj.Id.ToString().Contains(searchValue)
                    || obj.CustomerName.ToLower().Contains(searchValue)
                    || obj.Discount.ToString().Contains(searchValue)
                    || obj.VAT.ToString().Contains(searchValue)
                    || obj.SubTotal.ToString().Contains(searchValue)
                    || obj.GrandTotal.ToString().Contains(searchValue)
                    || obj.PaidAmount.ToString().Contains(searchValue)
                    || obj.DueAmount.ToString().Contains(searchValue)

                    || obj.CreatedDate.ToString().Contains(searchValue));
                }

                resultTotal = _GetGridItem.Count();
                var result = _GetGridItem.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = resultTotal, recordsTotal = resultTotal, data = result });

            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize(Roles = MainMenu.PaymentDetailReport.RoleName)]
        [HttpGet]
        public IActionResult PaymentDetailReport()
        {
            try
            {
                return View();
            }
            catch (Exception) { throw; }
        }
        [HttpPost]
        public IActionResult GetDataTablePaymentDetailList()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();

                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnAscDesc = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int resultTotal = 0;

                var _GetGridItem = _iSalesService.GetPaymentDetailList().Where(x => x.IsReturn == false);
                //Sorting
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnAscDesc)))
                {
                    _GetGridItem = _GetGridItem.OrderBy(sortColumn + " " + sortColumnAscDesc);
                }

                //Search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    searchValue = searchValue.ToLower();
                    _GetGridItem = _GetGridItem.Where(obj => obj.Id.ToString().Contains(searchValue)
                    || obj.PaymentId.ToString().Contains(searchValue)
                    || obj.ItemId.ToString().Contains(searchValue)
                    || obj.ItemName.ToLower().Contains(searchValue)
                    || obj.Quantity.ToString().Contains(searchValue)
                    || obj.UnitPrice.ToString().Contains(searchValue)
                    || obj.TotalAmount.ToString().Contains(searchValue)

                    || obj.CreatedDate.ToString().Contains(searchValue));
                }

                resultTotal = _GetGridItem.Count();
                var result = _GetGridItem.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = resultTotal, recordsTotal = resultTotal, data = result });

            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = MainMenu.ProductWiseSale.RoleName)]
        [HttpGet]
        public IActionResult ProductWiseSale()
        {
            try
            {
                return View();
            }
            catch (Exception) { throw; }
        }
        [HttpPost]
        public IActionResult ProductWiseSaleDataTable()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();

                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnAscDesc = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int resultTotal = 0;

                var _GetGridItem = _iSalesService.GetProductWiseSaleList();
                //Sorting
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnAscDesc)))
                {
                    _GetGridItem = _GetGridItem.OrderBy(sortColumn + " " + sortColumnAscDesc);
                }

                //Search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    searchValue = searchValue.ToLower();
                    _GetGridItem = _GetGridItem.Where(obj => obj.Id.ToString().Contains(searchValue)
                    || obj.SalesDate.ToString().Contains(searchValue)
                    || obj.ItemName.ToLower().Contains(searchValue)
                    || obj.InvoiceNo.ToLower().Contains(searchValue)
                    || obj.CustomerName.ToLower().Contains(searchValue)
                    || obj.UnitPrice.ToString().Contains(searchValue)
                    || obj.Quantity.ToString().Contains(searchValue)
                    || obj.Discount.ToString().Contains(searchValue)
                    || obj.VAT.ToString().Contains(searchValue)
                    || obj.Total.ToString().Contains(searchValue));
                }

                resultTotal = _GetGridItem.Count();
                var result = _GetGridItem.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = resultTotal, recordsTotal = resultTotal, data = result });

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}