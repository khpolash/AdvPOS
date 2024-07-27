using AdvPOS.Models;
using AdvPOS.Pages;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdvPOS.Services
{
    public class Roles : IRoles
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public Roles(RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task GenerateRolesFromPagesAsync()
        {
            Type t = typeof(MainMenu);
            foreach (Type item in t.GetNestedTypes())
            {
                foreach (var itm in item.GetFields())
                {
                    if (itm.Name.Contains("RoleName"))
                    {
                        string roleName = (string)itm.GetValue(item);
                        if (!await _roleManager.RoleExistsAsync(roleName))
                            await _roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }
            }
        }

        public async Task AddToRoles(ApplicationUser _ApplicationUser)
        {
            if (_ApplicationUser != null)
            {
                var roles = _roleManager.Roles;
                List<string> listRoles = new List<string>();
                foreach (var item in roles)
                {
                    listRoles.Add(item.Name);
                }
                await _userManager.AddToRolesAsync(_ApplicationUser, listRoles);
            }
        }

        public async Task<MainMenuViewModel> RolebaseMenuLoad(ApplicationUser _ApplicationUser)
        {
            MainMenuViewModel _MainMenuViewModel = new MainMenuViewModel();
            PropertyInfo[] _PropertyInfo = typeof(MainMenuViewModel).GetProperties();

            var _Roles = _roleManager.Roles.ToList();
            foreach (var role in _Roles)
            {
                var _PropertyName = _PropertyInfo.Where(x => x.Name == Regex.Replace(role.Name, @"\s+", "")).SingleOrDefault();
                var _IsInRoleAsync = await _userManager.IsInRoleAsync(_ApplicationUser, role.Name);

                if (_IsInRoleAsync)
                    _PropertyName.SetValue(_MainMenuViewModel, true);
                else
                    _PropertyName.SetValue(_MainMenuViewModel, false);
            }
            return _MainMenuViewModel;
        }
    }
}
