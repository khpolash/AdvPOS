using AdvPOS.Data;
using AdvPOS.Models;
using AdvPOS.Models.CustomerInfoViewModel;
using AdvPOS.Services;
using Microsoft.AspNetCore.Authorization;
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
    public class CustomerInfoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommon _iCommon;
        private readonly ISalesService _iSalesService;

        public CustomerInfoController(ApplicationDbContext context, ICommon iCommon, ISalesService iSalesService)
        {
            _context = context;
            _iCommon = iCommon;
            _iSalesService = iSalesService;
        }

        [Authorize(Roles = Pages.MainMenu.CustomerInfo.RoleName)]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetDataTabelData()
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

                var _GetGridItem = _iCommon.GetCustomerList();
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
                    || obj.Name.ToLower().Contains(searchValue)
                    || obj.Phone.ToLower().Contains(searchValue)
                    || obj.Email.ToLower().Contains(searchValue)
                    || obj.BillingAddress.ToLower().Contains(searchValue)
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
            CustomerInfoCRUDViewModel vm = await _iCommon.GetCustomerList().Where(x => x.Id == id).SingleOrDefaultAsync();
            if (vm == null) return NotFound();
            return PartialView("_Details", vm);
        }
        [HttpGet]
        public IActionResult CustomerTranHistory(long? id)
        {
            if (id == null) return NotFound();
            var result = _iSalesService.GetPaymentGridData().Where(x => x.CustomerId == id);
            if (result == null) return NotFound();
            return PartialView("_CustomerTranHistory", result);
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            CustomerInfoCRUDViewModel vm = new CustomerInfoCRUDViewModel();
            ViewBag.GetddlCustomerType = new SelectList(_iCommon.GetddlCustomerType(), "Id", "Name");
            if (id > 0) vm = await _context.CustomerInfo.Where(x => x.Id == id).SingleOrDefaultAsync();
            return PartialView("_AddEdit", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(CustomerInfoCRUDViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        if (vm.UseThisAsBillingAddress)
                        {
                            vm.BillingAddress = vm.Address;
                            vm.BillingAddressPostcode = vm.AddressPostcode;
                        }

                        CustomerInfo _CustomerInfoInfo = new CustomerInfo();
                        if (vm.Id > 0)
                        {
                            _CustomerInfoInfo = await _context.CustomerInfo.FindAsync(vm.Id);

                            vm.CreatedDate = _CustomerInfoInfo.CreatedDate;
                            vm.CreatedBy = _CustomerInfoInfo.CreatedBy;
                            vm.ModifiedDate = DateTime.Now;
                            vm.ModifiedBy = HttpContext.User.Identity.Name;
                            _context.Entry(_CustomerInfoInfo).CurrentValues.SetValues(vm);
                            await _context.SaveChangesAsync();

                            vm.AlertMessage = "Customer Info Updated Successfully. ID: " + _CustomerInfoInfo.Id;
                            return new JsonResult(vm);
                        }
                        else
                        {                                                          
                            _CustomerInfoInfo = vm;
                            _CustomerInfoInfo.CreatedDate = DateTime.Now;
                            _CustomerInfoInfo.ModifiedDate = DateTime.Now;
                            _CustomerInfoInfo.CreatedBy = HttpContext.User.Identity.Name;
                            _CustomerInfoInfo.ModifiedBy = HttpContext.User.Identity.Name;
                            _context.Add(_CustomerInfoInfo);
                            await _context.SaveChangesAsync();

                            vm.Id = _CustomerInfoInfo.Id;
                            vm.AlertMessage = "Customer Info Created Successfully. ID: " + _CustomerInfoInfo.Id;
                            return new JsonResult(vm);
                        }
                    }
                    return new JsonResult("Operation failed.");
                }
                catch (Exception ex)
                {
                    return new JsonResult(ex.Message);
                    throw;
                }
            }
            return View(vm);
        }

        [HttpGet]
        public JsonResult GetAllCustomerForDDL()
        {
            try
            {
                var _LoadddlCustomerInfo = _iCommon.LoadddlCustomerInfo();
                ViewBag._LoadddlCustomerInfo = new SelectList(_iCommon.LoadddlCustomerInfo(), "Id", "Name");
                return new JsonResult(_LoadddlCustomerInfo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(Int64 id)
        {
            try
            {
                var _CustomerInfoInfo = await _context.CustomerInfo.FindAsync(id);
                _CustomerInfoInfo.ModifiedDate = DateTime.Now;
                _CustomerInfoInfo.ModifiedBy = HttpContext.User.Identity.Name;
                _CustomerInfoInfo.Cancelled = true;

                _context.Update(_CustomerInfoInfo);
                await _context.SaveChangesAsync();
                return new JsonResult(_CustomerInfoInfo);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
