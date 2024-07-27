using AdvPOS.Data;
using AdvPOS.Models;
using AdvPOS.Models.UserAccountViewModel;
using AdvPOS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;

namespace AdvPOS.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class UserProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommon _iCommon;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserProfileController(UserManager<ApplicationUser> userManager, ICommon iCommon,
            ApplicationDbContext context)
        {
            _context = context;
            _iCommon = iCommon;
            _userManager = userManager;
        }

        [Authorize(Roles = Pages.MainMenu.UserProfile.RoleName)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var _UserName = User.Identity.Name;
            var _UserProfile = _context.UserProfile.FirstOrDefault(x => x.Email == _UserName);
            var result = await _iCommon.GetByUserProfileInfo(_UserProfile.UserProfileId);
            return View(result);
        }

        [HttpGet]
        public IActionResult ResetPasswordGeneral(Int64 id)
        {
            ResetPasswordViewModel _ResetPasswordViewModel = new();
            UserProfile _UserProfile = _iCommon.GetByUserProfile(id);
            _ResetPasswordViewModel.ApplicationUserId = _UserProfile.ApplicationUserId;
            return PartialView("_ResetPasswordGeneral", _ResetPasswordViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPasswordGeneral(ResetPasswordViewModel vm)
        {
            try
            {
                string AlertMessage = string.Empty;
                var _ApplicationUser = await _userManager.FindByIdAsync(vm.ApplicationUserId);
                if (vm.NewPassword.Equals(vm.ConfirmPassword))
                {
                    var result = await _userManager.ChangePasswordAsync(_ApplicationUser, vm.OldPassword, vm.NewPassword);
                    if (result.Succeeded)
                        AlertMessage = "Change Password Succeeded. User name: " + _ApplicationUser.Email;
                    else
                    {
                        string errorMessage = string.Empty;
                        foreach (var item in result.Errors)
                        {
                            errorMessage = errorMessage + " " + item.Description;
                        }
                        AlertMessage = "error" + errorMessage;
                    }
                }
                return new JsonResult(AlertMessage);
            }
            catch (Exception ex)
            {
                return new JsonResult("error" + ex.Message);
                throw;
            }
        }
    }
}
