using AdvPOS.ConHelper;
using AdvPOS.Data;
using AdvPOS.Helpers;
using AdvPOS.Models;
using AdvPOS.Models.CommonViewModel;
using AdvPOS.Models.ItemsViewModel;
using AdvPOS.Models.PaymentDetailViewModel;
using AdvPOS.Models.PaymentModeHistoryViewModel;
using AdvPOS.Models.PaymentViewModel;
using AdvPOS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace AdvPOS.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommon _iCommon;
        private readonly ISalesService _iSalesService;
        private readonly IDBOperation _iDBOperation;
        private string _StartDate = null;
        private string _EndDate = null;

        public PaymentController(ApplicationDbContext context, ICommon iCommon, IDBOperation iDBOperation, ISalesService iSalesService)
        {
            _context = context;
            _iCommon = iCommon;
            _iSalesService = iSalesService;
            _iDBOperation = iDBOperation;
        }

        [Authorize(Roles = Pages.MainMenu.Payment.RoleName)]
        [HttpGet]
        public IActionResult Index(string StartDate, string EndDate)
        {
            if (StartDate != null && EndDate != null)
            {
                HttpContext.Session.SetString("_StartDate", StartDate);
                HttpContext.Session.SetString("_EndDate", EndDate);
                ViewBag.StartDate = StartDate;
                ViewBag.EndDate = EndDate;
            }
            else
            {
                HttpContext.Session.SetString("_StartDate", string.Empty);
                HttpContext.Session.SetString("_EndDate", string.Empty);
                ViewBag.StartDate = "Min";
                ViewBag.EndDate = "Max";
            }
            return View();
        }

        [HttpPost]
        public IActionResult GetDataTabelData()
        {
            try
            {
                _StartDate = this.HttpContext.Session.GetString("_StartDate");
                _EndDate = this.HttpContext.Session.GetString("_EndDate");

                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();

                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnAscDesc = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int resultTotal = 0;


                IQueryable<PaymentCRUDViewModel> _GetGridItem = null;
                if (_StartDate != null && _EndDate != null && _StartDate != "" && _EndDate != "")
                {
                    _GetGridItem = _iSalesService.GetPaymentGridData().Where(x => x.Category == InvoiceType.RegularInvoice && x.CreatedDate >= Convert.ToDateTime(_StartDate) && x.CreatedDate <= Convert.ToDateTime(_EndDate).AddDays(1));
                }
                else
                {
                    _GetGridItem = _iSalesService.GetPaymentGridData().Where(x => x.Category == InvoiceType.RegularInvoice);
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
                    || obj.SubTotal.ToString().ToLower().Contains(searchValue)
                    || obj.DiscountAmount.ToString().ToLower().Contains(searchValue)
                    || obj.VATAmount.ToString().ToLower().Contains(searchValue)
                    || obj.GrandTotal.ToString().ToLower().Contains(searchValue)
                    || obj.PaidAmount.ToString().ToLower().Contains(searchValue)
                    || obj.DueAmount.ToString().ToLower().Contains(searchValue)
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
        [HttpGet]
        public async Task<IActionResult> Details(Int64 id)
        {
            var result = await _iSalesService.GetByPaymentDetail(id);
            if (result == null) return NotFound();
            return PartialView("_Detail", result);
        }
        [HttpGet]
        public async Task<IActionResult> GetSalesReturnItemDetails(Int64 id)
        {
            var result = await _iSalesService.GetByPaymentDetailInReturn(id);
            if (result == null) return NotFound();
            return PartialView("_Detail", result);
        }
        [HttpGet]
        public async Task<IActionResult> GetPaymentSummary(Int64 Id)
        {
            var result = await _context.Payment.Where(x => x.Id == Id).SingleOrDefaultAsync();
            return new JsonResult(result);
        }
        [HttpGet]
        public async Task<IActionResult> POSReport(Int64 id)
        {
            var _PrintPaymentInvoice = await _iSalesService.PrintPaymentInvoice(id);
            return PartialView("_PaymentInvoicePOS", _PrintPaymentInvoice);
        }
        [HttpGet]
        public async Task<IActionResult> PrintPOSReport(Int64 id)
        {
            var _PrintPaymentInvoice = await _iSalesService.PrintPaymentInvoice(id);
            return View("ThermalPaymentInvoicePOS", _PrintPaymentInvoice);
        }
        [HttpGet]
        public async Task<IActionResult> PrintPaymentInvoice(Int64 _PaymentId, bool IsSaveAndPrint)
        {
            var _PrintPaymentInvoice = await _iSalesService.PrintPaymentInvoice(_PaymentId);
            _PrintPaymentInvoice.PaymentCRUDViewModel.IsSaveAndPrint = IsSaveAndPrint;
            return View(_PrintPaymentInvoice);
        }
        [HttpGet]
        public async Task<IActionResult> PrintSaveAndPrintPaymentInvoice(Int64 _PaymentId, bool IsSaveAndPrint)
        {
            var _PrintPaymentInvoice = await _iSalesService.PrintPaymentInvoice(_PaymentId);
            _PrintPaymentInvoice.PaymentCRUDViewModel.IsSaveAndPrint = IsSaveAndPrint;
            return RedirectToAction(nameof(PrintPaymentInvoice), _PrintPaymentInvoice);
        }
        [HttpGet]
        public async Task<IActionResult> ThermalPrintPaymentInvoice(Int64 _PaymentId)
        {
            var _PrintPaymentInvoice = await _iSalesService.PrintPaymentInvoice(_PaymentId);
            return View(_PrintPaymentInvoice);
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(Int64 id)
        {
            var _IsVat = _context.CompanyInfo.FirstOrDefault(m => m.Cancelled == false).IsVat;
            ViewBag._LoadddlInventoryItem = new SelectList(_iCommon.LoadddlInventoryItem(_IsVat), "Id", "Name");
            ViewBag._LoadddlCustomerInfo = new SelectList(_iCommon.LoadddlCustomerInfo(), "Id", "Name");
            ViewBag._LoadddlPaymentType = new SelectList(_iCommon.LoadddlPaymentType(), "Id", "Name");
            ViewBag.GetddlPaymentStatus = new SelectList(_iCommon.GetddlPaymentStatus(), "Id", "Name");
            ViewBag.GetddlCustomerType = new SelectList(_iCommon.GetddlCustomerType(), "Id", "Name");

            ViewBag.ddlCurrency = new SelectList(_iCommon.LoadddlCurrencyItem(), "Id", "Name");
            ViewBag.ddlBranch = new SelectList(_iCommon.GetCommonddlData("Branch"), "Id", "Name");

            ManagePaymentViewModel vm = new();
            if (id > 0)
            {
                vm = await _iSalesService.GetByPaymentDetail(id);
            }
            else
            {
                Payment _Payment = new();
                vm.PaymentCRUDViewModel = new();
                vm.PaymentCRUDViewModel.InvoiceNo = _iDBOperation.GetInvoiceNo(InvoiceType.DraftInvoice);
                vm.PaymentCRUDViewModel.QuoteNo = _iDBOperation.GetQuoteNo(InvoiceType.QueoteInvoice);
                vm.PaymentCRUDViewModel.CurrencySymbol = _context.Currency.FirstOrDefault(m => m.IsDefault == true).Symbol;

                //Set Branch By User
                var _UserName = User.Identity.Name;
                vm.PaymentCRUDViewModel.BranchId = await _iCommon.GetBranchIdByUserName(_UserName);

                vm.PaymentCRUDViewModel.UserName = HttpContext.User.Identity.Name;
                _Payment = await _iDBOperation.CreateDraftInvoice(vm.PaymentCRUDViewModel);

                vm.PaymentCRUDViewModel.Id = _Payment.Id;
                vm.PaymentCRUDViewModel.Category = InvoiceType.DraftInvoice;
                vm.PaymentCRUDViewModel.CreatedDate = _Payment.CreatedDate;
                vm.PaymentCRUDViewModel.CreatedBy = _Payment.CreatedBy;
            }

            vm.PaymentCRUDViewModel.IsVat = _IsVat == true ? "Yes" : "No";
            vm.PaymentCRUDViewModel.QuoteNoRef = id;
            var _IsInRole = User.IsInRole("Admin");
            vm.PaymentCRUDViewModel.RoleName = _IsInRole == true ? "Admin" : "Other";
            return PartialView("_AddEdit", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(PaymentCRUDViewModel vm)
        {
            JsonResultViewModel _JsonResultViewModel = new();
            try
            {
                string strUserName = HttpContext.User.Identity.Name;

                if (vm.Category == InvoiceType.RegularInvoice)
                {
                    vm.QuoteNo = null;
                    if (vm.InvoiceNo.Contains("D"))
                    {
                        vm.InvoiceNo = _iDBOperation.GetInvoiceNo(InvoiceType.RegularInvoice);
                    }
                }
                else if (vm.Category == InvoiceType.DraftInvoice)
                {
                    vm.InvoiceNo = _iDBOperation.GetInvoiceNo(InvoiceType.DraftInvoice);
                }
                else if (vm.Category == InvoiceType.QueoteInvoice)
                {
                    vm.InvoiceNo = null;
                }

                var result = await _iDBOperation.UpdatePayment(vm, true);
                _JsonResultViewModel.CurrentURL = vm.CurrentURL;
                _JsonResultViewModel.IsSuccess = true;
                _JsonResultViewModel.Id = result.Id;
                _JsonResultViewModel.ModelObject = vm;

                if (vm.Category == InvoiceType.DraftInvoice)
                {
                    _JsonResultViewModel.AlertMessage = "Draft Invoice Saved Successfully. Invoice ID: " + vm.InvoiceNo;
                }
                else if (vm.Category == InvoiceType.RegularInvoice)
                {
                    _JsonResultViewModel.AlertMessage = "Invoice Saved Successfully. Invoice ID: " + vm.InvoiceNo;
                }
                else
                {
                    _JsonResultViewModel.AlertMessage = "Quote Invoice Saved Successfully. Quote ID: " + vm.QuoteNo;
                }
                return new JsonResult(_JsonResultViewModel);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _JsonResultViewModel.IsSuccess = false;
                _JsonResultViewModel.AlertMessage = ex.Message;
                return new JsonResult(_JsonResultViewModel);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPaymentDetail(PaymentDetailCRUDViewModel vm)
        {
            string _CurrentUserName = HttpContext.User.Identity.Name;
            var _PaymentCRUDViewModel = vm.PaymentCRUDViewModel;
            vm = await _iDBOperation.CreatePaymentsDetail(vm, _CurrentUserName);

            var _Payment = _context.Payment.Where(x => x.Id == _PaymentCRUDViewModel.Id).SingleOrDefault();
            _PaymentCRUDViewModel.Category = _Payment.Category;
            vm.PaymentCRUDViewModel = await _iDBOperation.UpdatePayment(_PaymentCRUDViewModel, false);

            ItemTranViewModel _ItemTranViewModel = new ItemTranViewModel
            {
                ItemId = vm.ItemId,
                TranQuantity = vm.Quantity,
                ActionMessage = "Add new sell. Invoice No: " + vm.PaymentCRUDViewModel.Id,
                CurrentUserName = _CurrentUserName
            };

            await _iCommon.CurrentItemsUpdate(_ItemTranViewModel);
            return new JsonResult(vm);
        }


        [HttpPost]
        public async Task<IActionResult> UpdatePaymentDetail(PaymentDetailCRUDViewModel vm)
        {
            var _PaymentCRUDViewModel = vm.PaymentCRUDViewModel;
            vm.UserName = HttpContext.User.Identity.Name;

            vm = await _iDBOperation.UpdatePaymentDetail(vm);
            vm.PaymentCRUDViewModel = await _iDBOperation.UpdatePayment(_PaymentCRUDViewModel, false);
            return new JsonResult(vm);
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePaymentDetailIMENo(PaymentDetailCRUDViewModel vm)
        {
            var _PaymentDetail = await _context.PaymentDetail.FindAsync(vm.Id);
            _PaymentDetail.ItemName = vm.ItemName;
            _PaymentDetail.ModifiedDate = DateTime.Now;
            _PaymentDetail.ModifiedBy = HttpContext.User.Identity.Name;
            _context.Update(_PaymentDetail);
            await _context.SaveChangesAsync();
            return new JsonResult(_PaymentDetail);
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePaymentDetail(PaymentDetailCRUDViewModel vm)
        {
            try
            {
                var _PaymentDetail = await _context.PaymentDetail.Where(x => x.Id == vm.Id).SingleOrDefaultAsync();
                _PaymentDetail.ModifiedDate = DateTime.Now;
                _PaymentDetail.ModifiedBy = HttpContext.User.Identity.Name;
                _PaymentDetail.Cancelled = true;
                _context.Update(_PaymentDetail);
                await _context.SaveChangesAsync();

                var result = await _iDBOperation.UpdatePayment(vm.PaymentCRUDViewModel, false);

                ItemTranViewModel _ItemTranViewModel = new ItemTranViewModel
                {
                    ItemId = _PaymentDetail.ItemId,
                    TranQuantity = _PaymentDetail.Quantity,
                    IsAddition = true,
                    ActionMessage = "Delete sell item. Invoice ID: " + result.Id,
                    CurrentUserName = HttpContext.User.Identity.Name
                };
                await _iCommon.CurrentItemsUpdate(_ItemTranViewModel);
                return new JsonResult(result);
            }
            catch (Exception) { throw; }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Int64 id)
        {
            try
            {
                var _Payment = await _context.Payment.FindAsync(id);
                _Payment.ModifiedDate = DateTime.Now;
                _Payment.ModifiedBy = HttpContext.User.Identity.Name;
                _Payment.Cancelled = true;
                _context.Update(_Payment);
                await _context.SaveChangesAsync();

                var _GetPaymentDetailList = await _iSalesService.GetPaymentDetailList().Where(x => x.PaymentId == _Payment.Id).ToListAsync();
                foreach (var item in _GetPaymentDetailList)
                {
                    ItemTranViewModel _ItemTranViewModel = new ItemTranViewModel
                    {
                        ItemId = item.ItemId,
                        TranQuantity = item.Quantity,
                        ActionMessage = "Delete new sell. Invoice Id:  " + _Payment.InvoiceNo,
                        CurrentUserName = HttpContext.User.Identity.Name
                    };
                    await _iCommon.CurrentItemsUpdate(_ItemTranViewModel);
                }
                return new JsonResult(_Payment);
            }
            catch (Exception) { throw; }
        }
        [HttpPost]
        public async Task<IActionResult> SavePaymentModeHistory(PaymentModeHistoryCRUDViewModel vm)
        {
            string UserName = HttpContext.User.Identity.Name;
            var _PaymentCRUDViewModel = vm.PaymentCRUDViewModel;
            vm.CreatedBy = UserName;
            vm.ModifiedBy = UserName;
            vm.PaymentType = InvoicePaymentType.SalesInvoicePayment;

            vm = await _iDBOperation.CreatePaymentModeHistory(vm);
            vm.PaymentCRUDViewModel = await _iDBOperation.UpdatePayment(_PaymentCRUDViewModel, false);
            return new JsonResult(vm);
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePaymentModeHistory(PaymentModeHistoryCRUDViewModel vm)
        {
            var _PaymentCRUDViewModel = vm.PaymentCRUDViewModel;
            var _PaymentModeHistory = await _context.PaymentModeHistory.Where(x => x.Id == vm.Id && vm.PaymentType == vm.PaymentType).SingleOrDefaultAsync();
            _PaymentModeHistory.ModifiedDate = DateTime.Now;
            _PaymentModeHistory.ModifiedBy = HttpContext.User.Identity.Name;
            _PaymentModeHistory.Cancelled = true;

            _context.Update(_PaymentModeHistory);
            await _context.SaveChangesAsync();

            vm.PaymentCRUDViewModel = await _iDBOperation.UpdatePayment(_PaymentCRUDViewModel, false);
            return new JsonResult(vm);
        }

        [HttpGet]
        public async Task<IActionResult> GetPriceModel(int Id)
        {
            var result = await _iCommon.LoadddlInventoryItem(true).ToListAsync();
            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetItemByItemBarcode(string ItemBarcode)
        {
            var _Items = await _context.Items.Where(x => x.Code == ItemBarcode).SingleOrDefaultAsync();
            return new JsonResult(_Items);
        }
        [HttpGet]
        public IActionResult GetCustomerHistory(Int64 CustomerId)
        {
            var result = _iDBOperation.GetCustomerHistory(CustomerId);
            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaymentSummaryData()
        {
            PaymentCRUDViewModel _PaymentCRUDViewModel = new();
            var _Payment = _context.Payment.Where(x => x.Cancelled == false);

            _PaymentCRUDViewModel.SubTotal = await _Payment.SumAsync(x => x.SubTotal);
            _PaymentCRUDViewModel.DiscountAmount = await _Payment.SumAsync(x => x.DiscountAmount);
            _PaymentCRUDViewModel.VATAmount = await _Payment.SumAsync(x => x.VATAmount);
            _PaymentCRUDViewModel.GrandTotal = await _Payment.SumAsync(x => x.GrandTotal);

            _PaymentCRUDViewModel.DueAmount = await _Payment.SumAsync(x => x.DueAmount);
            _PaymentCRUDViewModel.PaidAmount = await _Payment.SumAsync(x => x.PaidAmount);
            _PaymentCRUDViewModel.ChangedAmount = await _Payment.SumAsync(x => x.ChangedAmount);
            return new JsonResult(_PaymentCRUDViewModel);
        }
    }
}
