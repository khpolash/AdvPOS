using AdvPOS.Data;
using AdvPOS.Models;
using AdvPOS.Models.BranchViewModel;
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
    public class BranchController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommon _iCommon;

        public BranchController(ApplicationDbContext context, ICommon iCommon)
        {
            _context = context;
            _iCommon = iCommon;
        }

        [Authorize(Roles = Pages.MainMenu.Branch.RoleName)]
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
                    || obj.ContactPerson.ToLower().Contains(searchValue)
                    || obj.PhoneNumber.ToLower().Contains(searchValue)
                    || obj.Address.ToLower().Contains(searchValue)
                    || obj.ShortDescription.ToLower().Contains(searchValue)
                    || obj.CreatedDate.ToString().ToLower().Contains(searchValue)

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

        private IQueryable<BranchCRUDViewModel> GetGridItem()
        {
            try
            {
                return (from _Branch in _context.Branch
                        where _Branch.Cancelled == false
                        select new BranchCRUDViewModel
                        {
                            Id = _Branch.Id,
                            Name = _Branch.Name,
                            ContactPerson = _Branch.ContactPerson,
                            PhoneNumber = _Branch.PhoneNumber,
                            Address = _Branch.Address,
                            
                            CreatedDate = _Branch.CreatedDate,
                            ModifiedDate = _Branch.ModifiedDate,
                            CreatedBy = _Branch.CreatedBy,
                            ModifiedBy = _Branch.ModifiedBy,
                            Cancelled = _Branch.Cancelled,
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
            BranchCRUDViewModel vm = await _context.Branch.FirstOrDefaultAsync(m => m.Id == id);
            if (vm == null) return NotFound();
            return PartialView("_Details", vm);
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            BranchCRUDViewModel vm = new BranchCRUDViewModel();
            if (id > 0) vm = await _context.Branch.Where(x => x.Id == id).SingleOrDefaultAsync();
            return PartialView("_AddEdit", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(BranchCRUDViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        Branch _Branch = new Branch();
                        if (vm.Id > 0)
                        {
                            _Branch = await _context.Branch.FindAsync(vm.Id);

                            vm.CreatedDate = _Branch.CreatedDate;
                            vm.CreatedBy = _Branch.CreatedBy;
                            vm.ModifiedDate = DateTime.Now;
                            vm.ModifiedBy = HttpContext.User.Identity.Name;
                            _context.Entry(_Branch).CurrentValues.SetValues(vm);
                            await _context.SaveChangesAsync();

                            var _AlertMessage = "Branch Updated Successfully. ID: " + _Branch.Id;
                            return new JsonResult(_AlertMessage);
                        }
                        else
                        {
                            _Branch = vm;
                            _Branch.CreatedDate = DateTime.Now;
                            _Branch.ModifiedDate = DateTime.Now;
                            _Branch.CreatedBy = HttpContext.User.Identity.Name;
                            _Branch.ModifiedBy = HttpContext.User.Identity.Name;
                            _context.Add(_Branch);
                            await _context.SaveChangesAsync();

                            var _AlertMessage = "Branch Created Successfully. ID: " + _Branch.Id;
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
                var _Branch = await _context.Branch.FindAsync(id);
                _Branch.ModifiedDate = DateTime.Now;
                _Branch.ModifiedBy = HttpContext.User.Identity.Name;
                _Branch.Cancelled = true;

                _context.Update(_Branch);
                await _context.SaveChangesAsync();
                return new JsonResult(_Branch);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool IsExists(long id)
        {
            return _context.Branch.Any(e => e.Id == id);
        }
    }
}
