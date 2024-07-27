using AdvPOS.Data;
using AdvPOS.Models;
using AdvPOS.Models.CategoriesViewModel;
using AdvPOS.Models.CommonViewModel;
using AdvPOS.Services;
using DataTablesParser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace AdvPOS.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommon _iCommon;

        public CategoriesController(ApplicationDbContext context, ICommon iCommon)
        {
            _context = context;
            _iCommon = iCommon;
        }

        [Authorize(Roles = Pages.MainMenu.Categories.RoleName)]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetDataTabelData()
        {
            var _GetGridItem = GetGridItem();
            var parser = new Parser<CategoriesCRUDViewModel>(Request.Form, _GetGridItem);
            return Json(parser.Parse());
        }

        [HttpPost]
        public IActionResult GetDataTabelDataOLD()
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
                    || obj.CreatedDate.ToString().Contains(searchValue)
                    || obj.ModifiedDate.ToString().Contains(searchValue)
                    || obj.CreatedBy.ToLower().Contains(searchValue)
                    || obj.ModifiedBy.ToLower().Contains(searchValue));
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

        private IQueryable<CategoriesCRUDViewModel> GetGridItem()
        {
            try
            {
                return (from _Categories in _context.Categories
                        where _Categories.Cancelled == false
                        select new CategoriesCRUDViewModel
                        {
                            Id = _Categories.Id,
                            Name = _Categories.Name,
                            Description = _Categories.Description,
                            CreatedDate = _Categories.CreatedDate,
                            ModifiedDate = _Categories.ModifiedDate,
                            CreatedBy = _Categories.CreatedBy,
                            ModifiedBy = _Categories.ModifiedBy,

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
            CategoriesCRUDViewModel vm = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            if (vm == null) return NotFound();
            return PartialView("_Details", vm);
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            CategoriesCRUDViewModel vm = new CategoriesCRUDViewModel();
            if (id > 0) vm = await _context.Categories.Where(x => x.Id == id).SingleOrDefaultAsync();
            return PartialView("_AddEdit", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(CategoriesCRUDViewModel vm)
        {
            JsonResultViewModel _JsonResultViewModel = new();
            try
            {
                if (ModelState.IsValid)
                {
                    Categories _Categories = new();
                    if (vm.Id > 0)
                    {
                        _Categories = await _context.Categories.FindAsync(vm.Id);

                        vm.CreatedDate = _Categories.CreatedDate;
                        vm.CreatedBy = _Categories.CreatedBy;
                        vm.ModifiedDate = DateTime.Now;
                        vm.ModifiedBy = HttpContext.User.Identity.Name;
                        _context.Entry(_Categories).CurrentValues.SetValues(vm);
                        await _context.SaveChangesAsync();

                        _JsonResultViewModel.AlertMessage = "Categories Updated Successfully. ID: " + _Categories.Id;
                        _JsonResultViewModel.CurrentURL = vm.CurrentURL;
                        _JsonResultViewModel.IsSuccess = true;
                        return new JsonResult(_JsonResultViewModel);
                    }
                    else
                    {
                        _Categories = vm;
                        _Categories.CreatedDate = DateTime.Now;
                        _Categories.ModifiedDate = DateTime.Now;
                        _Categories.CreatedBy = HttpContext.User.Identity.Name;
                        _Categories.ModifiedBy = HttpContext.User.Identity.Name;
                        _context.Add(_Categories);
                        await _context.SaveChangesAsync();

                        _JsonResultViewModel.AlertMessage = "Categories Created Successfully. ID: " + _Categories.Id;
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
                var _Categories = await _context.Categories.FindAsync(id);
                _Categories.ModifiedDate = DateTime.Now;
                _Categories.ModifiedBy = HttpContext.User.Identity.Name;
                _Categories.Cancelled = true;

                _context.Update(_Categories);
                await _context.SaveChangesAsync();
                return new JsonResult(_Categories);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
