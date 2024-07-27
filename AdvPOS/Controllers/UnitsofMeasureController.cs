using AdvPOS.Data;
using AdvPOS.Models;
using AdvPOS.Models.CommonViewModel;
using AdvPOS.Models.UnitsofMeasureViewModel;
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
    public class UnitsofMeasureController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommon _iCommon;

        public UnitsofMeasureController(ApplicationDbContext context, ICommon iCommon)
        {
            _context = context;
            _iCommon = iCommon;
        }

        [Authorize(Roles = Pages.MainMenu.UnitsofMeasure.RoleName)]
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
            catch (Exception)
            {
                throw;
            }

        }

        private IQueryable<UnitsofMeasureCRUDViewModel> GetGridItem()
        {
            try
            {
                return (from _UnitsofMeasure in _context.UnitsofMeasure
                        where _UnitsofMeasure.Cancelled == false
                        select new UnitsofMeasureCRUDViewModel
                        {
                            Id = _UnitsofMeasure.Id,
                            Name = _UnitsofMeasure.Name,
                            Description = _UnitsofMeasure.Description,
                            CreatedDate = _UnitsofMeasure.CreatedDate,
                            ModifiedDate = _UnitsofMeasure.ModifiedDate,
                            CreatedBy = _UnitsofMeasure.CreatedBy,
                            ModifiedBy = _UnitsofMeasure.ModifiedBy,

                        }).OrderByDescending(x => x.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null) return NotFound();
            UnitsofMeasureCRUDViewModel vm = await _context.UnitsofMeasure.FirstOrDefaultAsync(m => m.Id == id);
            if (vm == null) return NotFound();
            return PartialView("_Details", vm);
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            UnitsofMeasureCRUDViewModel vm = new UnitsofMeasureCRUDViewModel();
            if (id > 0) vm = await _context.UnitsofMeasure.Where(x => x.Id == id).SingleOrDefaultAsync();
            return PartialView("_AddEdit", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(UnitsofMeasureCRUDViewModel vm)
        {
            JsonResultViewModel _JsonResultViewModel = new();
            try
            {
                if (ModelState.IsValid)
                {
                    UnitsofMeasure _UnitsofMeasure = new();
                    if (vm.Id > 0)
                    {
                        _UnitsofMeasure = await _context.UnitsofMeasure.FindAsync(vm.Id);

                        vm.CreatedDate = _UnitsofMeasure.CreatedDate;
                        vm.CreatedBy = _UnitsofMeasure.CreatedBy;
                        vm.ModifiedDate = DateTime.Now;
                        vm.ModifiedBy = HttpContext.User.Identity.Name;
                        _context.Entry(_UnitsofMeasure).CurrentValues.SetValues(vm);
                        await _context.SaveChangesAsync();

                        _JsonResultViewModel.AlertMessage = "Units of Measure Updated Successfully. ID: " + _UnitsofMeasure.Id;
                        _JsonResultViewModel.CurrentURL = vm.CurrentURL;
                        _JsonResultViewModel.IsSuccess = true;
                        return new JsonResult(_JsonResultViewModel);
                    }
                    else
                    {
                        _UnitsofMeasure = vm;
                        _UnitsofMeasure.CreatedDate = DateTime.Now;
                        _UnitsofMeasure.ModifiedDate = DateTime.Now;
                        _UnitsofMeasure.CreatedBy = HttpContext.User.Identity.Name;
                        _UnitsofMeasure.ModifiedBy = HttpContext.User.Identity.Name;
                        _context.Add(_UnitsofMeasure);
                        await _context.SaveChangesAsync();

                        _JsonResultViewModel.AlertMessage = "Units of Measure Created Successfully. ID: " + _UnitsofMeasure.Id;
                        _JsonResultViewModel.CurrentURL = vm.CurrentURL;
                        _JsonResultViewModel.IsSuccess = true;
                        return new JsonResult(_JsonResultViewModel);
                    }
                }
                _JsonResultViewModel.AlertMessage = "Operation failed.";
                _JsonResultViewModel.CurrentURL = vm.CurrentURL;
                _JsonResultViewModel.IsSuccess = false;
                return new JsonResult(_JsonResultViewModel);
            }
            catch (Exception ex)
            {
                _JsonResultViewModel.IsSuccess = false;
                _JsonResultViewModel.AlertMessage = ex.Message;
                return new JsonResult(_JsonResultViewModel);
                throw;
            }
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(Int64 id)
        {
            try
            {
                var _UnitsofMeasure = await _context.UnitsofMeasure.FindAsync(id);
                _UnitsofMeasure.ModifiedDate = DateTime.Now;
                _UnitsofMeasure.ModifiedBy = HttpContext.User.Identity.Name;
                _UnitsofMeasure.Cancelled = true;

                _context.Update(_UnitsofMeasure);
                await _context.SaveChangesAsync();
                return new JsonResult(_UnitsofMeasure);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
