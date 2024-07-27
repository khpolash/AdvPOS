using AdvPOS.Data;
using AdvPOS.Models;
using AdvPOS.Models.EmailConfigViewModel;
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
    public class EmailConfigController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommon _iCommon;

        public EmailConfigController(ApplicationDbContext context, ICommon iCommon)
        {
            _context = context;
            _iCommon = iCommon;
        }

        [Authorize(Roles = Pages.MainMenu.EmailConfig.RoleName)]
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
                    || obj.Email.ToLower().Contains(searchValue)
                    || obj.Password.ToLower().Contains(searchValue)
                    || obj.Hostname.ToLower().Contains(searchValue)
                    || obj.Port.ToString().Contains(searchValue)
                    || obj.CreatedDate.ToString().ToLower().Contains(searchValue)
                    || obj.ModifiedDate.ToString().ToLower().Contains(searchValue)

                    || obj.CreatedDate.ToString().Contains(searchValue));
                }

                resultTotal = _GetGridItem.Count();

                var result = _GetGridItem.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = resultTotal, recordsTotal = resultTotal, data = result });

            }
            catch (Exception) { throw; }
        }

        private IQueryable<EmailConfigCRUDViewModel> GetGridItem()
        {
            try
            {
                return (from _EmailConfig in _context.EmailConfig
                        where _EmailConfig.Cancelled == false
                        select new EmailConfigCRUDViewModel
                        {
                            Id = _EmailConfig.Id,
                            Email = _EmailConfig.Email,
                            Password = _EmailConfig.Password,
                            Hostname = _EmailConfig.Hostname,
                            Port = _EmailConfig.Port,
                            SSLEnabled = _EmailConfig.SSLEnabled,
                            SenderFullName = _EmailConfig.SenderFullName,
                            IsDefaultDisplay = _EmailConfig.IsDefault == true ? "Yes" : "No",
                            CreatedDate = _EmailConfig.CreatedDate,
                            ModifiedDate = _EmailConfig.ModifiedDate,
                        }).OrderByDescending(x => x.Id);
            }
            catch (Exception) { throw; }
        }
        [HttpGet]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null) return NotFound();
            EmailConfigCRUDViewModel vm = await _context.EmailConfig.FirstOrDefaultAsync(m => m.Id == id);
            if (vm == null) return NotFound();
            return PartialView("_Details", vm);
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            EmailConfigCRUDViewModel vm = new EmailConfigCRUDViewModel();
            if (id > 0) vm = await _context.EmailConfig.Where(x => x.Id == id).SingleOrDefaultAsync();
            return PartialView("_AddEdit", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(EmailConfigCRUDViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        EmailConfig _EmailConfig = new EmailConfig();
                        if (vm.Id > 0)
                        {
                            _EmailConfig = await _context.EmailConfig.FindAsync(vm.Id);

                            vm.CreatedDate = _EmailConfig.CreatedDate;
                            vm.CreatedBy = _EmailConfig.CreatedBy;
                            vm.ModifiedDate = DateTime.Now;
                            vm.ModifiedBy = HttpContext.User.Identity.Name;
                            _context.Entry(_EmailConfig).CurrentValues.SetValues(vm);
                            await _context.SaveChangesAsync();

                            var _AlertMessage = "Email Config Updated Successfully. ID: " + _EmailConfig.Id;
                            return new JsonResult(_AlertMessage);
                        }
                        else
                        {
                            _EmailConfig = vm;
                            _EmailConfig.CreatedDate = DateTime.Now;
                            _EmailConfig.ModifiedDate = DateTime.Now;
                            _EmailConfig.CreatedBy = HttpContext.User.Identity.Name;
                            _EmailConfig.ModifiedBy = HttpContext.User.Identity.Name;
                            _context.Add(_EmailConfig);
                            await _context.SaveChangesAsync();

                            var _AlertMessage = "Email Config Created Successfully. ID: " + _EmailConfig.Id;
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
                var _EmailConfig = await _context.EmailConfig.FindAsync(id);
                _EmailConfig.ModifiedDate = DateTime.Now;
                _EmailConfig.ModifiedBy = HttpContext.User.Identity.Name;
                _EmailConfig.Cancelled = true;

                _context.Update(_EmailConfig);
                await _context.SaveChangesAsync();
                return new JsonResult(_EmailConfig);
            }
            catch (Exception) { throw; }
        }
    }
}
