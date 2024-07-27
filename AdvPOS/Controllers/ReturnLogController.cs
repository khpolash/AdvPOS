using AdvPOS.ConHelper;
using AdvPOS.Data;
using AdvPOS.Helpers;
using AdvPOS.Models.ItemsViewModel;
using AdvPOS.Models.ReturnLogViewModel;
using AdvPOS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace AdvPOS.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ReturnLogController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommon _iCommon;
        private readonly ISalesService _iSalesService;
        private readonly IDBOperation _iDBOperation;

        public ReturnLogController(ApplicationDbContext context, ICommon iCommon, IDBOperation iDBOperation, ISalesService iSalesService)
        {
            _context = context;
            _iCommon = iCommon;
            _iDBOperation = iDBOperation;
            _iSalesService = iSalesService;
        }

        [Authorize(Roles = Pages.MainMenu.SalesReturnLog.RoleName)]
        [HttpGet]
        public IActionResult SalesReturnIndex()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetSalesRetrurnDataTable()
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

                var _GetGridItem = GetGridItem().Where(x => x.TranType == ReturnLogType.Sales);
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
                    || obj.RefId.ToString().ToLower().Contains(searchValue)
                    || obj.CustomerDisplay.ToLower().Contains(searchValue)
                    || obj.TranType.ToLower().Contains(searchValue)
                    || obj.Note.ToLower().Contains(searchValue)

                    || obj.CreatedDate.ToString().Contains(searchValue));
                }

                resultTotal = _GetGridItem.Count();

                var result = _GetGridItem.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = resultTotal, recordsTotal = resultTotal, data = result });

            }
            catch (Exception) { throw; }
        }

        [HttpGet]
        public async Task<IActionResult> ReturnLogDetails(Int64 id)
        {
            var result = await GetGridItem().Where(x => x.Id == id).SingleOrDefaultAsync();
            if (result == null) return NotFound();
            return PartialView("_Details", result);
        }


        [HttpGet]
        public async Task<IActionResult> GetSalesRetrurn(Int64 id)
        {
            var result = await _iSalesService.GetByPaymentDetailInReturn(id);
            if (result == null) return NotFound();
            return PartialView("_GetSalesRetrurn", result);
        }

        [HttpPost]
        public async Task<JsonResult> SaveSalesRetrurn(Int64 id, string _ReturnNote)
        {
            string _UserName = HttpContext.User.Identity.Name;
            try
            {
                //Update Payment
                var _Payment = await _context.Payment.FindAsync(id);
                _Payment.ModifiedDate = DateTime.Now;
                _Payment.ModifiedBy = _UserName;
                _Payment.ReturnType = TranReturnType.FullReturn;
                _context.Update(_Payment);
                await _context.SaveChangesAsync();

                //Add Return Log
                ReturnLogCRUDViewModel _ReturnLogCRUDViewModel = new();
                _ReturnLogCRUDViewModel.RefId = _Payment.Id;
                _ReturnLogCRUDViewModel.InvoiceNo = _Payment.InvoiceNo;
                _ReturnLogCRUDViewModel.CustomerId = _Payment.CustomerId;
                _ReturnLogCRUDViewModel.TranType = ReturnLogType.Sales;
                _ReturnLogCRUDViewModel.Note = _ReturnNote;
                _ReturnLogCRUDViewModel.UserName = _UserName;
                await _iDBOperation.AddReturnLog(_ReturnLogCRUDViewModel);


                var listPaymentDetail = await _context.PaymentDetail.Where(x => x.PaymentId == _Payment.Id).ToListAsync();
                foreach (var item in listPaymentDetail)
                {
                    var _PaymentDetailId = await _context.PaymentDetail.FindAsync(item.Id);
                    _PaymentDetailId.ModifiedDate = DateTime.Now;
                    _PaymentDetailId.ModifiedBy = _UserName;
                    _PaymentDetailId.IsReturn = true;
                    _PaymentDetailId.Cancelled = true;
                    _context.Update(_PaymentDetailId);
                    await _context.SaveChangesAsync();
                }
                var result = await _iDBOperation.UpdatePayment(_Payment, false);

                foreach (var item in listPaymentDetail)
                {
                    ItemTranViewModel _ItemTranViewModel = new ItemTranViewModel
                    {
                        ItemId = item.ItemId,
                        TranQuantity = item.Quantity,
                        IsAddition = true,
                        ActionMessage = "Return sell item. Invoice Id:  " + _Payment.InvoiceNo,
                        CurrentUserName = _UserName
                    };
                    await _iCommon.CurrentItemsUpdate(_ItemTranViewModel);
                }
                return new JsonResult(_Payment);
            }
            catch (Exception) { throw; }
        }

        [HttpPost]
        public async Task<JsonResult> SingleItemSalesRetrurn(Int64 PaymentId, Int64 PaymentDetailId)
        {
            string _UserName = HttpContext.User.Identity.Name;
            try
            {
                //Update Payment
                var _Payment = await _context.Payment.FindAsync(PaymentId);
                var IsPartilaReturnCount = await _context.PaymentDetail.Where(x => x.PaymentId == PaymentId && x.IsReturn == false).ToListAsync();
                if (IsPartilaReturnCount.Count == 1)
                {
                    _Payment.ReturnType = TranReturnType.FullReturn;
                }
                else
                {
                    _Payment.ReturnType = TranReturnType.PartilaReturn;
                }
                _Payment.ModifiedDate = DateTime.Now;
                _Payment.ModifiedBy = _UserName;
                _context.Update(_Payment);
                await _context.SaveChangesAsync();

                var _PaymentDetail = await _context.PaymentDetail.Where(x => x.Id == PaymentDetailId).SingleOrDefaultAsync();
                //Add Return Log
                ReturnLogCRUDViewModel _ReturnLogCRUDViewModel = new();
                _ReturnLogCRUDViewModel.RefId = _Payment.Id;
                _ReturnLogCRUDViewModel.InvoiceNo = _Payment.InvoiceNo;
                _ReturnLogCRUDViewModel.CustomerId = _Payment.CustomerId;
                _ReturnLogCRUDViewModel.TranType = ReturnLogType.Sales;
                _ReturnLogCRUDViewModel.Note = "Single Item Sales Return. Item Name: " + _PaymentDetail.ItemName;
                _ReturnLogCRUDViewModel.UserName = _UserName;
                await _iDBOperation.AddReturnLog(_ReturnLogCRUDViewModel);

                //Update Payment Detail
                _PaymentDetail.ModifiedDate = DateTime.Now;
                _PaymentDetail.ModifiedBy = _UserName;
                _PaymentDetail.IsReturn = true;
                _PaymentDetail.Cancelled = true;
                _context.Update(_PaymentDetail);
                await _context.SaveChangesAsync();
                var result = await _iDBOperation.UpdatePayment(_Payment, false);

                //Update Item with History
                ItemTranViewModel _ItemTranViewModel = new ItemTranViewModel
                {
                    ItemId = _PaymentDetail.ItemId,
                    TranQuantity = _PaymentDetail.Quantity,
                    IsAddition = true,
                    ActionMessage = "Return sell item. Invoice Id:  " + _Payment.InvoiceNo,
                    CurrentUserName = _UserName
                };
                await _iCommon.CurrentItemsUpdate(_ItemTranViewModel);

                return new JsonResult(_PaymentDetail);
            }
            catch (Exception) { throw; }
        }


        private IQueryable<ReturnLogCRUDViewModel> GetGridItem()
        {
            try
            {
                var result = (from _ReturnLog in _context.ReturnLog
                              join _CustomerInfo in _context.CustomerInfo on _ReturnLog.CustomerId equals _CustomerInfo.Id
                              join _Supplier in _context.Supplier on _ReturnLog.CustomerId equals _Supplier.Id
                              where _ReturnLog.Cancelled == false
                              select new ReturnLogCRUDViewModel
                              {
                                  Id = _ReturnLog.Id,
                                  RefId = _ReturnLog.RefId,
                                  InvoiceNo = _ReturnLog.InvoiceNo,
                                  CustomerId = _ReturnLog.CustomerId,
                                  CustomerDisplay = _ReturnLog.TranType == ReturnLogType.Sales ? _CustomerInfo.Name : _Supplier.Name,
                                  TranType = _ReturnLog.TranType,
                                  Note = _ReturnLog.Note,

                                  CreatedDate = _ReturnLog.CreatedDate,
                                  ModifiedDate = _ReturnLog.ModifiedDate,
                                  CreatedBy = _ReturnLog.CreatedBy,
                                  ModifiedBy = _ReturnLog.ModifiedBy,
                                  Cancelled = _ReturnLog.Cancelled,
                              }).OrderByDescending(x => x.Id);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
