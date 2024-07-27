using AdvPOS.ConHelper;
using AdvPOS.Data;
using AdvPOS.Helpers;
using AdvPOS.Models.ItemCartViewModel;
using AdvPOS.Models.ItemsViewModel;
using AdvPOS.Models.PaymentModeHistoryViewModel;
using AdvPOS.Models.PaymentViewModel;
using AdvPOS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Linq.Dynamic.Core;

namespace AdvPOS.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ItemCartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommon _iCommon;
        private readonly ISalesService _iSalesService;
        private readonly IDBOperation _iDBOperation;

        public ItemCartController(ApplicationDbContext context, ICommon iCommon, ISalesService iSalesService, IDBOperation iDBOperation)
        {
            _context = context;
            _iCommon = iCommon;
            _iSalesService = iSalesService;
            _iDBOperation = iDBOperation;
        }

        [Authorize(Roles = Pages.MainMenu.ItemCart.RoleName)]
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public IActionResult GetDataTableItemCartList()
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

                var _GetGridItem = _iCommon.GetAllCartItemForCustomDT();

                //Search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    searchValue = searchValue.ToLower();
                    _GetGridItem = _GetGridItem.Where(obj => obj[0].Name.ToLower().Contains(searchValue)
                    || obj[0].Quantity.ToString().Contains(searchValue)
                    || obj[0].SellPrice.ToString().Contains(searchValue)
                    || obj[0].Barcode.ToLower().Contains(searchValue)

                    || obj[1].Name.ToLower().Contains(searchValue)
                    || obj[1].Quantity.ToString().Contains(searchValue)
                    || obj[1].SellPrice.ToString().Contains(searchValue)
                    || obj[1].Barcode.ToLower().Contains(searchValue)

                    || obj[2].Name.ToLower().Contains(searchValue)
                    || obj[2].Quantity.ToString().Contains(searchValue)
                    || obj[2].SellPrice.ToString().Contains(searchValue)
                    || obj[2].Barcode.ToLower().Contains(searchValue)

                    || obj[3].Name.ToLower().Contains(searchValue)
                    || obj[3].Quantity.ToString().Contains(searchValue)
                    || obj[3].SellPrice.ToString().Contains(searchValue)
                    || obj[3].Barcode.ToLower().Contains(searchValue));
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
        [Authorize(Roles = Pages.MainMenu.ItemCart.RoleName)]
        [HttpGet]
        public IActionResult ItemCartSideInvoice()
        {
            try
            {
                ViewBag.CategorieList = new SelectList(_iCommon.LoadddlCategories(), "Id", "Name");
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public IActionResult GetDataTableItemCartSideInvoice(bool IsFilterData, Int64 CategoriesId)
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

                IQueryable<List<ItemCartViewModel>> _GetItemCartDataList;
                if (IsFilterData)
                {
                    _GetItemCartDataList = _iCommon.GetItemCartDataList().Where(obj => obj[0].CategoriesId == CategoriesId
                    || obj[1].CategoriesId == CategoriesId || obj[2].CategoriesId == CategoriesId);
                }
                else
                {
                    _GetItemCartDataList = _iCommon.GetItemCartDataList();
                }


                //Search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    searchValue = searchValue.ToLower();
                    _GetItemCartDataList = _GetItemCartDataList.Where(obj => obj[0].Name.ToLower().Contains(searchValue)
                    || obj[0].Quantity.ToString().Contains(searchValue)
                    || obj[0].SellPrice.ToString().Contains(searchValue)
                    || obj[0].Barcode.ToLower().Contains(searchValue)
                    || obj[0].CategoriesName.ToLower().Contains(searchValue)

                    || obj[1].Name.ToLower().Contains(searchValue)
                    || obj[1].Quantity.ToString().Contains(searchValue)
                    || obj[1].SellPrice.ToString().Contains(searchValue)
                    || obj[1].Barcode.ToLower().Contains(searchValue)
                    || obj[1].CategoriesName.ToLower().Contains(searchValue)

                    || obj[2].Name.ToLower().Contains(searchValue)
                    || obj[2].Quantity.ToString().Contains(searchValue)
                    || obj[2].SellPrice.ToString().Contains(searchValue)
                    || obj[2].Barcode.ToLower().Contains(searchValue)
                    || obj[2].CategoriesName.ToLower().Contains(searchValue));
                }

                resultTotal = _GetItemCartDataList.Count();
                var result = _GetItemCartDataList.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = resultTotal, recordsTotal = resultTotal, data = result });

            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet]
        public JsonResult GetAllCartItem()
        {
            try
            {
                var result = _iCommon.GetAllCartItem().ToList();
                return new JsonResult(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public IActionResult GetItemByCategories(Int64 CategoriesId)
        {
            var _GetGridItem = _iCommon.GetItemCartDataList().Where(obj => obj[0].CategoriesId == CategoriesId
            || obj[1].CategoriesId == CategoriesId
            || obj[2].CategoriesId == CategoriesId);
            return new JsonResult(_GetGridItem);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDraftItemCart(ItemCartSideInvoiceViewModel vm)
        {
            PaymentCRUDViewModel _PaymentCRUDViewModel = new();
            string _UserName = HttpContext.User.Identity.Name;
            _PaymentCRUDViewModel.InvoiceNo = _iDBOperation.GetInvoiceNo(InvoiceType.DraftInvoice);
            _PaymentCRUDViewModel.QuoteNo = _iDBOperation.GetQuoteNo(InvoiceType.QueoteInvoice);
            _PaymentCRUDViewModel.CurrencySymbol = _context.Currency.FirstOrDefault(m => m.IsDefault == true).Symbol;
            _PaymentCRUDViewModel.UserName = _UserName;

            var _Payment = await _iDBOperation.CreateDraftInvoice(_PaymentCRUDViewModel);
            foreach (var item in vm.listPaymentDetail)
            {
                item.PaymentId = _Payment.Id;
                item.ItemDiscount = 0;
                //Convert VAT Amount to Percentage: =(24.92/89)*100
                var _ItemVAT = (item.ItemVAT / item.UnitPrice) * 100;
                item.ItemVAT = Convert.ToInt16(_ItemVAT);
                var _PaymentDetail = await _iDBOperation.CreatePaymentsDetail(item, _PaymentCRUDViewModel.UserName);

                ItemTranViewModel _ItemTranViewModel = new ItemTranViewModel
                {
                    ItemId = _PaymentDetail.ItemId,
                    TranQuantity = _PaymentDetail.Quantity,
                    ActionMessage = "Add new sell. Invoice No: " + _Payment.Id,
                    CurrentUserName = _PaymentCRUDViewModel.UserName
                };
                await _iCommon.CurrentItemsUpdate(_ItemTranViewModel);
            }

            //Add PaymentModeHistory 
            if (vm.IsSaveAndPrint)
            {
                PaymentModeHistoryCRUDViewModel _PaymentModeHistoryCRUDViewModel = new()
                {
                    PaymentId = _Payment.Id,
                    CreatedBy = _UserName,
                    ModifiedBy = _UserName,
                    ModeOfPayment = "Cash",
                    PaymentType = InvoicePaymentType.SalesInvoicePayment,
                    Amount = vm.PaidAmount
                };
                await _iDBOperation.CreatePaymentModeHistory(_PaymentModeHistoryCRUDViewModel);
            }

            _PaymentCRUDViewModel = _Payment;
            _PaymentCRUDViewModel.CustomerId = vm.CustomerId;
            _PaymentCRUDViewModel.Category = InvoiceType.RegularInvoice;
            _PaymentCRUDViewModel.BranchId = await _iCommon.GetBranchIdByUserName(_UserName);
            _PaymentCRUDViewModel.CurrencyId = _context.Currency.FirstOrDefault(m => m.IsDefault == true).Id;;
            await _iDBOperation.UpdatePayment(_PaymentCRUDViewModel, false);
            return new JsonResult(_Payment);
        }
    }
}
