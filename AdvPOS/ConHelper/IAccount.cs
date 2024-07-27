using AdvPOS.Models;
using AdvPOS.Models.AccountViewModels;
using AdvPOS.Models.UserAccountViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace AdvPOS.ConHelper
{
    public interface IAccount
    {
        Task<Tuple<ApplicationUser, IdentityResult>> CreateUserAccount(CreateUserAccountViewModel _CreateUserAccountViewModel);
        Task<Tuple<ApplicationUser, string>> CreateUserProfile(UserProfileViewModel vm, string LoginUser);       
    }
}
