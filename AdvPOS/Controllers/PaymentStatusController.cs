using AdvPOS.Data;
using AdvPOS.Models;
using AdvPOS.Models.PaymentStatusViewModel;
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
    public class PaymentStatusController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommon _iCommon;

        public PaymentStatusController(ApplicationDbContext context, ICommon iCommon)
        {
            _context = context;
            _iCommon = iCommon;
        }

        [Authorize(Roles = Pages.MainMenu.PaymentStatus.RoleName)]
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

        private IQueryable<PaymentStatusCRUDViewModel> GetGridItem()
        {
            try
            {
                return (from _PaymentStatus in _context.PaymentStatus
                        where _PaymentStatus.Cancelled == false
                        select new PaymentStatusCRUDViewModel
                        {
                            Id = _PaymentStatus.Id,
                            Name = _PaymentStatus.Name,
                            Description = _PaymentStatus.Description,
                            CreatedDate = _PaymentStatus.CreatedDate,
                            ModifiedDate = _PaymentStatus.ModifiedDate,
                            CreatedBy = _PaymentStatus.CreatedBy,
                            ModifiedBy = _PaymentStatus.ModifiedBy,

                        }).OrderByDescending(x => x.Id);
            }
            catch (Exception) { throw; }
        }
        [HttpGet]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null) return NotFound();
            PaymentStatusCRUDViewModel vm = await _context.PaymentStatus.FirstOrDefaultAsync(m => m.Id == id);
            if (vm == null) return NotFound();
            return PartialView("_Details", vm);
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            PaymentStatusCRUDViewModel vm = new PaymentStatusCRUDViewModel();
            if (id > 0) vm = await _context.PaymentStatus.Where(x => x.Id == id).SingleOrDefaultAsync();
            return PartialView("_AddEdit", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(PaymentStatusCRUDViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        PaymentStatus _PaymentStatus = new PaymentStatus();
                        if (vm.Id > 0)
                        {
                            _PaymentStatus = await _context.PaymentStatus.FindAsync(vm.Id);

                            vm.CreatedDate = _PaymentStatus.CreatedDate;
                            vm.CreatedBy = _PaymentStatus.CreatedBy;
                            vm.ModifiedDate = DateTime.Now;
                            vm.ModifiedBy = HttpContext.User.Identity.Name;
                            _context.Entry(_PaymentStatus).CurrentValues.SetValues(vm);
                            await _context.SaveChangesAsync();

                            var _AlertMessage = "Payment Status Updated Successfully. ID: " + _PaymentStatus.Id;
                            return new JsonResult(_AlertMessage);
                        }
                        else
                        {
                            _PaymentStatus = vm;
                            _PaymentStatus.CreatedDate = DateTime.Now;
                            _PaymentStatus.ModifiedDate = DateTime.Now;
                            _PaymentStatus.CreatedBy = HttpContext.User.Identity.Name;
                            _PaymentStatus.ModifiedBy = HttpContext.User.Identity.Name;
                            _context.Add(_PaymentStatus);
                            await _context.SaveChangesAsync();

                            var _AlertMessage = "Payment Status Created Successfully. ID: " + _PaymentStatus.Id;
                            return new JsonResult(_AlertMessage);
                        }
                    }
                    return new JsonResult("Operation failed.");
                }
                catch (Exception) { throw; }
            }
            return View(vm);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(Int64 id)
        {
            try
            {
                var _PaymentStatus = await _context.PaymentStatus.FindAsync(id);
                _PaymentStatus.ModifiedDate = DateTime.Now;
                _PaymentStatus.ModifiedBy = HttpContext.User.Identity.Name;
                _PaymentStatus.Cancelled = true;

                _context.Update(_PaymentStatus);
                await _context.SaveChangesAsync();
                return new JsonResult(_PaymentStatus);
            }
            catch (Exception) { throw; }
        }
    }
}
