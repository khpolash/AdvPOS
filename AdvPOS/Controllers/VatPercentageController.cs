using AdvPOS.Data;
using AdvPOS.Models;
using AdvPOS.Models.VatPercentageViewModel;
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
    public class VatPercentageController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommon _iCommon;

        public VatPercentageController(ApplicationDbContext context, ICommon iCommon)
        {
            _context = context;
            _iCommon = iCommon;
        }

        [Authorize(Roles = Pages.MainMenu.VatPercentage.RoleName)]
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
                    || obj.Percentage.ToString().Contains(searchValue)                  
                    || obj.CreatedBy.ToLower().Contains(searchValue)

                    || obj.CreatedDate.ToString().Contains(searchValue));
                }

                resultTotal = _GetGridItem.Count();

                var result = _GetGridItem.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = resultTotal, recordsTotal = resultTotal, data = result });

            }
            catch (Exception) { throw; }

        }

        private IQueryable<VatPercentageCRUDViewModel> GetGridItem()
        {
            try
            {
                return (from _VatPercentage in _context.VatPercentage
                        where _VatPercentage.Cancelled == false
                        select new VatPercentageCRUDViewModel
                        {
                            Id = _VatPercentage.Id,
                            Name = _VatPercentage.Name,
                            Percentage = _VatPercentage.Percentage,
                            IsDefault = _VatPercentage.IsDefault,
                            CreatedDate = _VatPercentage.CreatedDate,
                            ModifiedDate = _VatPercentage.ModifiedDate,
                            CreatedBy = _VatPercentage.CreatedBy,

                        }).OrderByDescending(x => x.Id);
            }
            catch (Exception) { throw; }
        }
        [HttpGet]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null) return NotFound();
            VatPercentageCRUDViewModel vm = await _context.VatPercentage.FirstOrDefaultAsync(m => m.Id == id);
            if (vm == null) return NotFound();
            return PartialView("_Details", vm);
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            VatPercentageCRUDViewModel vm = new VatPercentageCRUDViewModel();
            if (id > 0) vm = await _context.VatPercentage.Where(x => x.Id == id).SingleOrDefaultAsync();
            return PartialView("_AddEdit", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(VatPercentageCRUDViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        VatPercentage _VatPercentage = new VatPercentage();
                        if (vm.Id > 0)
                        {
                            _VatPercentage = await _context.VatPercentage.FindAsync(vm.Id);

                            vm.CreatedDate = _VatPercentage.CreatedDate;
                            vm.CreatedBy = _VatPercentage.CreatedBy;
                            vm.ModifiedDate = DateTime.Now;
                            vm.ModifiedBy = HttpContext.User.Identity.Name;
                            _context.Entry(_VatPercentage).CurrentValues.SetValues(vm);
                            await _context.SaveChangesAsync();

                            var _AlertMessage = "VatPercentage Updated Successfully. ID: " + _VatPercentage.Id;
                            return new JsonResult(_AlertMessage);
                        }
                        else
                        {
                            _VatPercentage = vm;
                            _VatPercentage.CreatedDate = DateTime.Now;
                            _VatPercentage.ModifiedDate = DateTime.Now;
                            _VatPercentage.CreatedBy = HttpContext.User.Identity.Name;
                            _VatPercentage.ModifiedBy = HttpContext.User.Identity.Name;
                            _context.Add(_VatPercentage);
                            await _context.SaveChangesAsync();

                            var _AlertMessage = "VatPercentage Created Successfully. ID: " + _VatPercentage.Id;
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

        [HttpPost]
        public async Task<JsonResult> Delete(Int64 id)
        {
            try
            {
                var _VatPercentage = await _context.VatPercentage.FindAsync(id);
                _VatPercentage.ModifiedDate = DateTime.Now;
                _VatPercentage.ModifiedBy = HttpContext.User.Identity.Name;
                _VatPercentage.Cancelled = true;

                _context.Update(_VatPercentage);
                await _context.SaveChangesAsync();
                return new JsonResult(_VatPercentage);
            }
            catch (Exception) { throw; }
        }      
    }
}
