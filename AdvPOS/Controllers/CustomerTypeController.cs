using AdvPOS.Data;
using AdvPOS.Models;
using AdvPOS.Models.CustomerTypeViewModel;
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
    public class CustomerTypeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommon _iCommon;

        public CustomerTypeController(ApplicationDbContext context, ICommon iCommon)
        {
            _context = context;
            _iCommon = iCommon;
        }

        [Authorize(Roles = Pages.MainMenu.CustomerType.RoleName)]
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

        private IQueryable<CustomerTypeCRUDViewModel> GetGridItem()
        {
            try
            {
                return (from _CustomerType in _context.CustomerType
                        where _CustomerType.Cancelled == false
                        select new CustomerTypeCRUDViewModel
                        {
                            Id = _CustomerType.Id,
                            Name = _CustomerType.Name,
                            Description = _CustomerType.Description,
                            CreatedDate = _CustomerType.CreatedDate,
                            ModifiedDate = _CustomerType.ModifiedDate,
                            CreatedBy = _CustomerType.CreatedBy,
                            ModifiedBy = _CustomerType.ModifiedBy,

                        }).OrderByDescending(x => x.Id);
            }
            catch (Exception) { throw; }
        }
        [HttpGet]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null) return NotFound();
            CustomerTypeCRUDViewModel vm = await _context.CustomerType.FirstOrDefaultAsync(m => m.Id == id);
            if (vm == null) return NotFound();
            return PartialView("_Details", vm);
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            CustomerTypeCRUDViewModel vm = new CustomerTypeCRUDViewModel();
            if (id > 0) vm = await _context.CustomerType.Where(x => x.Id == id).SingleOrDefaultAsync();
            return PartialView("_AddEdit", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(CustomerTypeCRUDViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        CustomerType _CustomerType = new CustomerType();
                        if (vm.Id > 0)
                        {
                            _CustomerType = await _context.CustomerType.FindAsync(vm.Id);

                            vm.CreatedDate = _CustomerType.CreatedDate;
                            vm.CreatedBy = _CustomerType.CreatedBy;
                            vm.ModifiedDate = DateTime.Now;
                            vm.ModifiedBy = HttpContext.User.Identity.Name;
                            _context.Entry(_CustomerType).CurrentValues.SetValues(vm);
                            await _context.SaveChangesAsync();

                            var _AlertMessage = "Customer Type Updated Successfully. ID: " + _CustomerType.Id;
                            return new JsonResult(_AlertMessage);
                        }
                        else
                        {
                            _CustomerType = vm;
                            _CustomerType.CreatedDate = DateTime.Now;
                            _CustomerType.ModifiedDate = DateTime.Now;
                            _CustomerType.CreatedBy = HttpContext.User.Identity.Name;
                            _CustomerType.ModifiedBy = HttpContext.User.Identity.Name;
                            _context.Add(_CustomerType);
                            await _context.SaveChangesAsync();

                            var _AlertMessage = "Customer Type Created Successfully. ID: " + _CustomerType.Id;
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
                var _CustomerType = await _context.CustomerType.FindAsync(id);
                _CustomerType.ModifiedDate = DateTime.Now;
                _CustomerType.ModifiedBy = HttpContext.User.Identity.Name;
                _CustomerType.Cancelled = true;

                _context.Update(_CustomerType);
                await _context.SaveChangesAsync();
                return new JsonResult(_CustomerType);
            }
            catch (Exception) { throw; }
        }
    }
}
