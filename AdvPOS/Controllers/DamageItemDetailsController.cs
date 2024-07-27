using AdvPOS.Data;
using AdvPOS.Helpers;
using AdvPOS.Models;
using AdvPOS.Models.DamageItemDeatilsViewModel;
using AdvPOS.Models.ItemsViewModel;
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
    public class DamageItemDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommon _iCommon;

        public DamageItemDetailsController(ApplicationDbContext context, ICommon iCommon)
        {
            _context = context;
            _iCommon = iCommon;
        }

        [Authorize(Roles = Pages.MainMenu.DamageItemDetails.RoleName)]
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
                    || obj.ItemId.ToString().ToLower().Contains(searchValue)
                    || obj.TotalDamageItem.ToString().ToLower().Contains(searchValue)
                    || obj.ReasonOfDamage.ToLower().Contains(searchValue)
                    || obj.CreatedDate.ToString().ToLower().Contains(searchValue)
                    || obj.ModifiedDate.ToString().ToLower().Contains(searchValue)
                    || obj.CreatedBy.ToLower().Contains(searchValue)

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

        private IQueryable<DamageItemDeatilsCRUDViewModel> GetGridItem()
        {
            try
            {
                return (from _DamageItemDeatils in _context.DamageItemDeatils
                        where _DamageItemDeatils.Cancelled == false
                        select new DamageItemDeatilsCRUDViewModel
                        {
                            Id = _DamageItemDeatils.Id,
                            ItemId = _DamageItemDeatils.ItemId,
                            TotalDamageItem = _DamageItemDeatils.TotalDamageItem,
                            ReasonOfDamage = _DamageItemDeatils.ReasonOfDamage,
                            CreatedDate = _DamageItemDeatils.CreatedDate,
                            ModifiedDate = _DamageItemDeatils.ModifiedDate,
                            CreatedBy = _DamageItemDeatils.CreatedBy,

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
            DamageItemDeatilsCRUDViewModel vm = await _context.DamageItemDeatils.FirstOrDefaultAsync(m => m.Id == id);
            if (vm == null) return NotFound();
            return PartialView("_Details", vm);
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            ViewBag._LoadddlInventoryItem = new SelectList(_iCommon.LoadddlInventoryItem(true), "Id", "Name");
            DamageItemDeatilsCRUDViewModel vm = new DamageItemDeatilsCRUDViewModel();
            if (id > 0) vm = await _context.DamageItemDeatils.Where(x => x.Id == id).SingleOrDefaultAsync();
            return PartialView("_AddEdit", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(DamageItemDeatilsCRUDViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        DamageItemDeatils _DamageItemDeatils = new DamageItemDeatils();
                        ItemTranViewModel _ItemTranViewModel = new();

                        if (vm.Id > 0)
                        {
                            _DamageItemDeatils = await _context.DamageItemDeatils.FindAsync(vm.Id);
                            int tmpTotalDamageItem = _DamageItemDeatils.TotalDamageItem;

                            vm.ItemId = _DamageItemDeatils.ItemId;
                            vm.CreatedDate = _DamageItemDeatils.CreatedDate;
                            vm.CreatedBy = _DamageItemDeatils.CreatedBy;
                            vm.ModifiedDate = DateTime.Now;
                            vm.ModifiedBy = HttpContext.User.Identity.Name;
                            _context.Entry(_DamageItemDeatils).CurrentValues.SetValues(vm);
                            await _context.SaveChangesAsync();

                            if (vm.TotalDamageItem > tmpTotalDamageItem)
                            {
                                _ItemTranViewModel.ItemId = _DamageItemDeatils.ItemId;
                                _ItemTranViewModel.TranQuantity = vm.TotalDamageItem - tmpTotalDamageItem;
                                _ItemTranViewModel.ActionMessage = "Damage Item added by update";
                                _ItemTranViewModel.IsAddition = false;
                            }
                            else
                            {
                                _ItemTranViewModel.ItemId = _DamageItemDeatils.ItemId;
                                _ItemTranViewModel.TranQuantity = tmpTotalDamageItem - vm.TotalDamageItem;
                                _ItemTranViewModel.ActionMessage = "Damage Item rollback by update";
                                _ItemTranViewModel.IsAddition = true;
                            }
                            await _iCommon.CurrentItemsUpdate(_ItemTranViewModel);
                            _ItemTranViewModel.CurrentUserName = HttpContext.User.Identity.Name;

                            var _AlertMessage = "Damage Item Updated Successfully. ID: " + _DamageItemDeatils.Id;
                            return new JsonResult(_AlertMessage);
                        }
                        else
                        {
                            _DamageItemDeatils = vm;
                            _DamageItemDeatils.CreatedDate = DateTime.Now;
                            _DamageItemDeatils.ModifiedDate = DateTime.Now;
                            _DamageItemDeatils.CreatedBy = HttpContext.User.Identity.Name;
                            _DamageItemDeatils.ModifiedBy = HttpContext.User.Identity.Name;
                            _context.Add(_DamageItemDeatils);
                            await _context.SaveChangesAsync();

                            _ItemTranViewModel.ItemId = vm.ItemId;
                            _ItemTranViewModel.TranQuantity = vm.TotalDamageItem;
                            _ItemTranViewModel.ActionMessage = "Damage Item added by add new";
                            _ItemTranViewModel.IsAddition = false;
                            _ItemTranViewModel.CurrentUserName = HttpContext.User.Identity.Name;
                            await _iCommon.CurrentItemsUpdate(_ItemTranViewModel);

                            var _AlertMessage = "Damage Item Created Successfully. ID: " + _DamageItemDeatils.Id;
                            return new JsonResult(_AlertMessage);
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

        [HttpDelete]
        public async Task<JsonResult> Delete(Int64 id)
        {
            try
            {
                var _DamageItemDeatils = await _context.DamageItemDeatils.FindAsync(id);
                _DamageItemDeatils.ModifiedDate = DateTime.Now;
                _DamageItemDeatils.ModifiedBy = HttpContext.User.Identity.Name;
                _DamageItemDeatils.Cancelled = true;
                _context.Update(_DamageItemDeatils);
                await _context.SaveChangesAsync();

                ItemTranViewModel _ItemTranViewModel = new();
                _ItemTranViewModel.ItemId = _DamageItemDeatils.ItemId;
                _ItemTranViewModel.TranQuantity = _DamageItemDeatils.TotalDamageItem;
                _ItemTranViewModel.ActionMessage = "Damage Item rollback by delete";
                _ItemTranViewModel.IsAddition = true;
                await _iCommon.CurrentItemsUpdate(_ItemTranViewModel);

                return new JsonResult(_DamageItemDeatils);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult GetByItem(int Id)
        {
            ItemsCRUDViewModel result = (from _Items in _context.Items
                                         join _UnitsofMeasure in _context.UnitsofMeasure on _Items.MeasureId equals _UnitsofMeasure.Id
                                         into _UnitsofMeasure
                                         from listUnitsofMeasure in _UnitsofMeasure.DefaultIfEmpty()

                                         where _Items.Id == Id
                                         select new ItemsCRUDViewModel
                                         {
                                             Id = _Items.Id,
                                             Name = _Items.Name,
                                             MeasureDisplay = listUnitsofMeasure.Name,
                                             Quantity = _Items.Quantity,
                                             CostPrice = _Items.CostPrice,
                                             NormalPrice = _Items.NormalPrice,
                                             WarehouseId = _Items.WarehouseId
                                         }).SingleOrDefault();
            return new JsonResult(result);
        }
    }
}
