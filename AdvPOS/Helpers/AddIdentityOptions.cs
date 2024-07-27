﻿using AdvPOS.Data;
using AdvPOS.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AdvPOS.Helpers
{
    public static class AddIdentityOptions
    {
        public static void SetOptions(IServiceCollection services, DefaultIdentityOptions _DefaultIdentityOptions)
        {
            try
            {
                services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = _DefaultIdentityOptions.PasswordRequireDigit;
                    options.Password.RequiredLength = _DefaultIdentityOptions.PasswordRequiredLength;
                    options.Password.RequireNonAlphanumeric = _DefaultIdentityOptions.PasswordRequireNonAlphanumeric;
                    options.Password.RequireUppercase = _DefaultIdentityOptions.PasswordRequireUppercase;
                    options.Password.RequireLowercase = _DefaultIdentityOptions.PasswordRequireLowercase;
                    options.Password.RequiredUniqueChars = _DefaultIdentityOptions.PasswordRequiredUniqueChars;

                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(_DefaultIdentityOptions.LockoutDefaultLockoutTimeSpanInMinutes);
                    options.Lockout.MaxFailedAccessAttempts = _DefaultIdentityOptions.LockoutMaxFailedAccessAttempts;
                    options.Lockout.AllowedForNewUsers = _DefaultIdentityOptions.LockoutAllowedForNewUsers;

                    options.User.RequireUniqueEmail = _DefaultIdentityOptions.UserRequireUniqueEmail;

                    options.SignIn.RequireConfirmedEmail = _DefaultIdentityOptions.SignInRequireConfirmedEmail;
                }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

                services.Configure<CookieAuthenticationOptions>(options =>
                {
                    options.Cookie.HttpOnly = _DefaultIdentityOptions.CookieHttpOnly;
                    options.Cookie.Expiration = TimeSpan.FromDays(_DefaultIdentityOptions.CookieExpiration);
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(_DefaultIdentityOptions.CookieExpireTimeSpan);
                    options.LoginPath = _DefaultIdentityOptions.LoginPath;
                    options.LogoutPath = _DefaultIdentityOptions.LogoutPath;
                    options.AccessDeniedPath = _DefaultIdentityOptions.AccessDeniedPath;
                    options.SlidingExpiration = _DefaultIdentityOptions.SlidingExpiration;
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
