using AdvPOS.Data;
using AdvPOS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvPOS.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ItemSerialNumberController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemSerialNumberController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = Pages.MainMenu.Payment.RoleName)]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Add(ItemSerialNumber _ItemSerialNumber)
        {
            try
            {
                _ItemSerialNumber.CreatedDate = DateTime.Now;
                _ItemSerialNumber.ModifiedDate = DateTime.Now;
                _ItemSerialNumber.CreatedBy = HttpContext.User.Identity.Name;
                _ItemSerialNumber.ModifiedBy = HttpContext.User.Identity.Name;
                _context.Add(_ItemSerialNumber);
                await _context.SaveChangesAsync();
                return new JsonResult(_ItemSerialNumber);
            }
            catch (Exception) { throw; }
        }

        [HttpGet]
        public JsonResult CountItemSerialNumber(Int64 PaymentDetailId)
        {
            try
            {
                var result = _context.ItemSerialNumber.Where(x => x.PaymentDetailId == PaymentDetailId).Count();
                return new JsonResult(result);
            }
            catch (Exception) { throw; }
        }
    }
}
