using AdvPOS.Data;
using AdvPOS.Models.ItemsHistoryViewModel;
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
    public class ItemsHistoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemsHistoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = Pages.MainMenu.ItemsHistory.RoleName)]
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
                    _GetGridItem = _GetGridItem.Where(obj => obj.ItemId.ToString().Contains(searchValue)
                    || obj.ItemName.ToLower().Contains(searchValue)
                    || obj.CostPrice.ToString().Contains(searchValue)
                    || obj.NormalPrice.ToString().Contains(searchValue)
                    || obj.OldUnitPrice.ToString().Contains(searchValue)
                    || obj.Action.ToLower().Contains(searchValue)
                    || obj.OldQuantity.ToString().Contains(searchValue)
                    || obj.NewQuantity.ToString().Contains(searchValue));
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

        private IQueryable<ItemsHistoryCRUDViewModel> GetGridItem()
        {
            try
            {
                return (from _ItemsHistory in _context.ItemsHistory
                        join _Items in _context.Items on _ItemsHistory.ItemId equals _Items.Id
                        where _ItemsHistory.Cancelled == false
                        select new ItemsHistoryCRUDViewModel
                        {
                            Id = _ItemsHistory.Id,
                            ItemId = _ItemsHistory.ItemId,
                            ItemName = _Items.Name,
                            CostPrice = _ItemsHistory.CostPrice,
                            NormalPrice = _ItemsHistory.NormalPrice,
                            OldUnitPrice = _ItemsHistory.OldUnitPrice,
                            OldSellPrice = _ItemsHistory.OldSellPrice,
                            OldQuantity = _ItemsHistory.OldQuantity,
                            NewQuantity = _ItemsHistory.NewQuantity,
                            TranQuantity = _ItemsHistory.TranQuantity,
                            Action = _ItemsHistory.Action,

                            CreatedDate = _ItemsHistory.CreatedDate,
                            ModifiedDate = _ItemsHistory.ModifiedDate,
                            CreatedBy = _ItemsHistory.CreatedBy,
                            ModifiedBy = _ItemsHistory.ModifiedBy,
                            Cancelled = _ItemsHistory.Cancelled
                        }).OrderByDescending(x => x.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> ViewItemHistory(Int64 ItemId)
        {
            var _ItemsHistory = await GetGridItem().Where(x => x.ItemId == ItemId).ToListAsync();
            return PartialView("_ViewItemHistory", _ItemsHistory);
        }
    }
}

