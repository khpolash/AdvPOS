using AdvPOS.Data;
using AdvPOS.Models;
using AdvPOS.Models.PaymentTypeViewModel;
using AdvPOS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace AdvPOS.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class PaymentTypeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommon _iCommon;

        public PaymentTypeController(ApplicationDbContext context, ICommon iCommon)
        {
            _context = context;
            _iCommon = iCommon;
        }

        [Authorize(Roles = Pages.MainMenu.PaymentType.RoleName)]
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

                var _GetGridItem = GetGridItem();
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
                    || obj.Description.ToLower().Contains(searchValue)
                    || obj.CreatedDate.ToString().ToLower().Contains(searchValue)
                    || obj.ModifiedDate.ToString().ToLower().Contains(searchValue)
                    || obj.CreatedBy.ToLower().Contains(searchValue)
                    || obj.ModifiedBy.ToLower().Contains(searchValue)

                    || obj.CreatedDate.ToString().Contains(searchValue));
                }

                resultTotal = _GetGridItem.Count();

                var result = _GetGridItem.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = resultTotal, recordsTotal = resultTotal, data = result });

            }
            catch (Exception) { throw; }
        }

        private IQueryable<PaymentTypeCRUDViewModel> GetGridItem()
        {
            try
            {
                return (from _PaymentType in _context.PaymentType
                        where _PaymentType.Cancelled == false
                        select new PaymentTypeCRUDViewModel
                        {
                            Id = _PaymentType.Id,
                            Name = _PaymentType.Name,
                            Description = _PaymentType.Description,
                            CreatedDate = _PaymentType.CreatedDate,
                            ModifiedDate = _PaymentType.ModifiedDate,
                            CreatedBy = _PaymentType.CreatedBy,
                            ModifiedBy = _PaymentType.ModifiedBy,

                        }).OrderByDescending(x => x.Id);
            }
            catch (Exception) { throw; }
        }
        [HttpGet]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null) return NotFound();
            PaymentTypeCRUDViewModel vm = await _context.PaymentType.FirstOrDefaultAsync(m => m.Id == id);
            if (vm == null) return NotFound();
            return PartialView("_Details", vm);
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            PaymentTypeCRUDViewModel vm = new PaymentTypeCRUDViewModel();
            if (id > 0) vm = await _context.PaymentType.Where(x => x.Id == id).SingleOrDefaultAsync();
            return PartialView("_AddEdit", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(PaymentTypeCRUDViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        PaymentType _PaymentType = new PaymentType();
                        if (vm.Id > 0)
                        {
                            _PaymentType = await _context.PaymentType.FindAsync(vm.Id);

                            vm.CreatedDate = _PaymentType.CreatedDate;
                            vm.CreatedBy = _PaymentType.CreatedBy;
                            vm.ModifiedDate = DateTime.Now;
                            vm.ModifiedBy = HttpContext.User.Identity.Name;
                            _context.Entry(_PaymentType).CurrentValues.SetValues(vm);
                            await _context.SaveChangesAsync();

                            var _AlertMessage = "Payment Type Updated Successfully. ID: " + _PaymentType.Id;
                            return new JsonResult(_AlertMessage);
                        }
                        else
                        {
                            _PaymentType = vm;
                            _PaymentType.CreatedDate = DateTime.Now;
                            _PaymentType.ModifiedDate = DateTime.Now;
                            _PaymentType.CreatedBy = HttpContext.User.Identity.Name;
                            _PaymentType.ModifiedBy = HttpContext.User.Identity.Name;
                            _context.Add(_PaymentType);
                            await _context.SaveChangesAsync();

                            var _AlertMessage = "Payment Type Created Successfully. ID: " + _PaymentType.Id;
                            return new JsonResult(_AlertMessage);
                        }
                    }
                    return new JsonResult("Operation failed.");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    return new JsonResult(ex.Message);
                    throw;
                }
            }
            return View(vm);
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(Int64 id)
        {
            try
            {
                var _PaymentType = await _context.PaymentType.FindAsync(id);
                _PaymentType.ModifiedDate = DateTime.Now;
                _PaymentType.ModifiedBy = HttpContext.User.Identity.Name;
                _PaymentType.Cancelled = true;

                _context.Update(_PaymentType);
                await _context.SaveChangesAsync();
                return new JsonResult(_PaymentType);
            }
            catch (Exception) { throw; }
        }
    }
}
