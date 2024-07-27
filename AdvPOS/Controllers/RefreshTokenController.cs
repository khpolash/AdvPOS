using AdvPOS.Data;
using AdvPOS.Models.RefreshTokenViewModel;
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
    public class RefreshTokenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RefreshTokenController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = Pages.MainMenu.RefreshToken.RoleName)]
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
                    || obj.UserId.ToLower().Contains(searchValue)
                    || obj.Token.ToLower().Contains(searchValue)
                    || obj.JwtId.ToLower().Contains(searchValue)
                    || obj.IsUsed.ToString().ToLower().Contains(searchValue)
                    || obj.IsRevorked.ToString().ToLower().Contains(searchValue)
                    || obj.AddedDate.ToString().ToLower().Contains(searchValue)

                    || obj.ExpiryDate.ToString().Contains(searchValue));
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

        private IQueryable<RefreshTokenCRUDViewModel> GetGridItem()
        {
            try
            {
                return (from _RefreshToken in _context.RefreshToken
                        join _UserProfile in _context.UserProfile on _RefreshToken.UserId equals _UserProfile.ApplicationUserId
                        where _RefreshToken.Cancelled == false
                        select new RefreshTokenCRUDViewModel
                        {
                            Id = _RefreshToken.Id,
                            UserProfileId = _UserProfile.UserProfileId,
                            UserEmail = _UserProfile.Email,
                            Token = _RefreshToken.Token,
                            JwtId = _RefreshToken.JwtId,
                            IsUsed = _RefreshToken.IsUsed,
                            IsRevorked = _RefreshToken.IsRevorked,
                            AddedDate = _RefreshToken.AddedDate,
                            ExpiryDate = _RefreshToken.ExpiryDate,
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
            RefreshTokenCRUDViewModel vm = await _context.RefreshToken.FirstOrDefaultAsync(m => m.Id == id);
            if (vm == null) return NotFound();
            return PartialView("_Details", vm);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(Int64 id)
        {
            try
            {
                var _RefreshToken = await _context.RefreshToken.FindAsync(id);
                _RefreshToken.ModifiedDate = DateTime.Now;
                _RefreshToken.ModifiedBy = HttpContext.User.Identity.Name;
                _RefreshToken.Cancelled = true;

                _context.Update(_RefreshToken);
                await _context.SaveChangesAsync();
                return new JsonResult(_RefreshToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
