using AdvPOS.Data;
using AdvPOS.Models;
using AdvPOS.Models.CommonViewModel;
using AdvPOS.Models.WarehouseViewModel;
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
    public class WarehouseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommon _iCommon;

        public WarehouseController(ApplicationDbContext context, ICommon iCommon)
        {
            _context = context;
            _iCommon = iCommon;
        }

        [Authorize(Roles = Pages.MainMenu.Warehouse.RoleName)]
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

        private IQueryable<WarehouseCRUDViewModel> GetGridItem()
        {
            try
            {
                return (from _Warehouse in _context.Warehouse
                        where _Warehouse.Cancelled == false
                        select new WarehouseCRUDViewModel
                        {
                            Id = _Warehouse.Id,
                            Name = _Warehouse.Name,
                            Description = _Warehouse.Description,
                            CreatedDate = _Warehouse.CreatedDate,
                            ModifiedDate = _Warehouse.ModifiedDate,
                            CreatedBy = _Warehouse.CreatedBy,
                            ModifiedBy = _Warehouse.ModifiedBy,

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
            WarehouseCRUDViewModel vm = await _context.Warehouse.FirstOrDefaultAsync(m => m.Id == id);
            if (vm == null) return NotFound();
            return PartialView("_Details", vm);
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            WarehouseCRUDViewModel vm = new WarehouseCRUDViewModel();
            if (id > 0) vm = await _context.Warehouse.Where(x => x.Id == id).SingleOrDefaultAsync();
            return PartialView("_AddEdit", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(WarehouseCRUDViewModel vm)
        {
            JsonResultViewModel _JsonResultViewModel = new();
            try
            {
                if (ModelState.IsValid)
                {
                    Warehouse _Warehouse = new Warehouse();
                    if (vm.Id > 0)
                    {
                        _Warehouse = await _context.Warehouse.FindAsync(vm.Id);

                        vm.CreatedDate = _Warehouse.CreatedDate;
                        vm.CreatedBy = _Warehouse.CreatedBy;
                        vm.ModifiedDate = DateTime.Now;
                        vm.ModifiedBy = HttpContext.User.Identity.Name;
                        _context.Entry(_Warehouse).CurrentValues.SetValues(vm);
                        await _context.SaveChangesAsync();

                        _JsonResultViewModel.AlertMessage = "Warehouse Updated Successfully. ID: " + _Warehouse.Id;
                        _JsonResultViewModel.CurrentURL = vm.CurrentURL;
                        _JsonResultViewModel.IsSuccess = true;
                        return new JsonResult(_JsonResultViewModel);
                    }
                    else
                    {
                        _Warehouse = vm;
                        _Warehouse.CreatedDate = DateTime.Now;
                        _Warehouse.ModifiedDate = DateTime.Now;
                        _Warehouse.CreatedBy = HttpContext.User.Identity.Name;
                        _Warehouse.ModifiedBy = HttpContext.User.Identity.Name;
                        _context.Add(_Warehouse);
                        await _context.SaveChangesAsync();

                        _JsonResultViewModel.AlertMessage = "Warehouse Created Successfully. ID: " + _Warehouse.Id;
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
                var _Warehouse = await _context.Warehouse.FindAsync(id);
                _Warehouse.ModifiedDate = DateTime.Now;
                _Warehouse.ModifiedBy = HttpContext.User.Identity.Name;
                _Warehouse.Cancelled = true;

                _context.Update(_Warehouse);
                await _context.SaveChangesAsync();
                return new JsonResult(_Warehouse);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
