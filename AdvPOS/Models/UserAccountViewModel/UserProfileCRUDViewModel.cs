﻿using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace AdvPOS.Models.UserAccountViewModel
{
    public class UserProfileCRUDViewModel : EntityBase
    {
        [Display(Name = "User Profile Id")]
        public Int64 UserProfileId { get; set; }

        public string ApplicationUserId { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        [Required]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }

        [Display(Name = "Profile Picture")]
        public IFormFile ProfilePicture { get; set; }
        public string ProfilePicturePath { get; set; }
        public Int64 BranchId { get; set; }
        public string BranchName { get; set; }


        public static implicit operator UserProfileCRUDViewModel(UserProfile _UserProfile)
        {
            return new UserProfileCRUDViewModel
            {
                UserProfileId = _UserProfile.UserProfileId,
                ApplicationUserId = _UserProfile.ApplicationUserId,
                FirstName = _UserProfile.FirstName,
                LastName = _UserProfile.LastName,
                PhoneNumber = _UserProfile.PhoneNumber,
                Email = _UserProfile.Email,
                Address = _UserProfile.Address,
                Country = _UserProfile.Country,
                ProfilePicturePath = _UserProfile.ProfilePicture,
                BranchId = _UserProfile.BranchId,

                CreatedDate = _UserProfile.CreatedDate,
                ModifiedDate = _UserProfile.ModifiedDate,
                CreatedBy = _UserProfile.CreatedBy,
                ModifiedBy = _UserProfile.ModifiedBy
            };
        }

        public static implicit operator UserProfile(UserProfileCRUDViewModel vm)
        {
            return new UserProfile
            {
                UserProfileId = vm.UserProfileId,
                ApplicationUserId = vm.ApplicationUserId,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                PhoneNumber = vm.PhoneNumber,
                Email = vm.Email,
                Address = vm.Address,
                Country = vm.Country,
                ProfilePicture = vm.ProfilePicturePath,
                BranchId = vm.BranchId,

                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
            };
        }
    }
}
