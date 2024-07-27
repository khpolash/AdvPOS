﻿using AdvPOS.Data;
using AdvPOS.Helpers;
using AdvPOS.Models;
using AdvPOS.Models.AccountViewModels;
using AdvPOS.Models.UserAccountViewModel;
using AdvPOS.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace AdvPOS.ConHelper
{
    public class Account : IAccount
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICommon _iCommon;
        public Account(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ICommon iCommon)
        {
            _context = context;
            _userManager = userManager;
            _iCommon = iCommon;
        }
        public async Task<Tuple<ApplicationUser, IdentityResult>> CreateUserAccount(CreateUserAccountViewModel _CreateUserAccountViewModel)
        {
            IdentityResult _IdentityResult = null;
            ApplicationUser _ApplicationUser = _CreateUserAccountViewModel;

            try
            {
                if (_CreateUserAccountViewModel.PasswordHash.Equals(_CreateUserAccountViewModel.ConfirmPassword))
                {
                    _IdentityResult = await _userManager.CreateAsync(_ApplicationUser, _CreateUserAccountViewModel.PasswordHash);
                }
                return Tuple.Create(_ApplicationUser, _IdentityResult);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Tuple<ApplicationUser, string>> CreateUserProfile(UserProfileViewModel vm, string LoginUser)
        {
            UserProfile _UserProfile = new UserProfile();
            string errorMessage = string.Empty;
            try
            {
                IdentityResult _IdentityResult = null;
                ApplicationUser _ApplicationUser = new ApplicationUser()
                {
                    UserName = vm.Email,
                    PhoneNumber = vm.PhoneNumber,
                    PhoneNumberConfirmed = true,
                    Email = vm.Email,
                    EmailConfirmed = true
                };
                if (vm.PasswordHash.Equals(vm.ConfirmPassword))
                {
                    _IdentityResult = await _userManager.CreateAsync(_ApplicationUser, vm.PasswordHash);
                }

                if (_IdentityResult.Succeeded)
                {
                    vm.ApplicationUserId = _ApplicationUser.Id;
                    vm.PasswordHash = _ApplicationUser.PasswordHash;
                    vm.ConfirmPassword = _ApplicationUser.PasswordHash;
                    vm.CreatedDate = DateTime.Now;
                    vm.ModifiedDate = DateTime.Now;
                    vm.CreatedBy = LoginUser;
                    vm.ModifiedBy = LoginUser;

                    _UserProfile = vm;
                    if (vm.ProfilePictureDetails == null)
                        _UserProfile.ProfilePicture = vm.ProfilePicture;
                    else
                        _UserProfile.ProfilePicture = "/upload/" + _iCommon.UploadedFile(vm.ProfilePictureDetails);

                    await _context.UserProfile.AddAsync(_UserProfile);
                    var result = await _context.SaveChangesAsync();

                    for (int i = 0; i < DefaultUserPage.PageCollection.Length; i++)
                    {
                        await _userManager.AddToRoleAsync(_ApplicationUser, DefaultUserPage.PageCollection[i]);
                    }
                }
                else
                {
                    foreach (var item in _IdentityResult.Errors)
                    {
                        errorMessage = errorMessage + " " + item.Description;
                    }
                    return Tuple.Create(_ApplicationUser, errorMessage);
                }
                return Tuple.Create(_ApplicationUser, "Success");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
