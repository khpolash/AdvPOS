using AdvPOS.ConHelper;
using AdvPOS.Data;
using AdvPOS.Helpers;
using AdvPOS.Models;
using AdvPOS.Models.CommonViewModel;
using AdvPOS.Models.DashboardViewModel;
using AdvPOS.Models.ItemsHistoryViewModel;
using AdvPOS.Models.ItemsViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AdvPOS.Services
{
    public class Functional : IFunctional
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly IRoles _roles;
        private readonly SuperAdminDefaultOptions _superAdminDefaultOptions;
        private readonly ApplicationInfo _applicationInfo;
        private readonly IAccount _iAccount;
        private readonly ICommon _iCommon;
        private readonly SeedData _SeedData = new SeedData();

        public Functional(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
           ApplicationDbContext context,
           IRoles roles,
           IOptions<SuperAdminDefaultOptions> superAdminDefaultOptions,
           IOptions<ApplicationInfo> applicationInfo,
           IAccount iAccount,
           ICommon iCommon)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _roles = roles;
            _superAdminDefaultOptions = superAdminDefaultOptions.Value;
            _applicationInfo = applicationInfo.Value;
            _iAccount = iAccount;
            _iCommon = iCommon;
        }

        public async Task SendEmailBySendGridAsync(string apiKey,
            string fromEmail,
            string fromFullName,
            string subject,
            string message,
            string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(fromEmail, fromFullName),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email, email));
            await client.SendEmailAsync(msg);

        }

        public async Task SendEmailByGmailAsync(string fromEmail,
            string fromFullName,
            string subject,
            string messageBody,
            string toEmail,
            string toFullName,
            string smtpUser,
            string smtpPassword,
            string smtpHost,
            int smtpPort,
            bool smtpSSL)
        {
            var body = messageBody;
            var message = new MailMessage();
            message.To.Add(new MailAddress(toEmail, toFullName));
            message.From = new MailAddress(fromEmail, fromFullName);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                smtp.UseDefaultCredentials = false;
                var credential = new NetworkCredential
                {
                    UserName = smtpUser,
                    Password = smtpPassword
                };
                smtp.Credentials = credential;
                smtp.Host = smtpHost;
                smtp.Port = smtpPort;
                smtp.EnableSsl = smtpSSL;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                await smtp.SendMailAsync(message);

            }

        }
        public async Task InitAppData()
        {
            var _GetCategoriesList = _SeedData.GetCategoriesList();
            foreach (var item in _GetCategoriesList)
            {
                item.CreatedDate = DateTime.Now;
                item.ModifiedDate = DateTime.Now;
                item.CreatedBy = "Admin";
                item.ModifiedBy = "Admin";
                _context.Categories.Add(item);
                await _context.SaveChangesAsync();
            }
            var _GetUnitsofMeasureList = _SeedData.GetUnitsofMeasureList();
            foreach (var item in _GetUnitsofMeasureList)
            {
                item.CreatedDate = DateTime.Now;
                item.ModifiedDate = DateTime.Now;
                item.CreatedBy = "Admin";
                item.ModifiedBy = "Admin";
                _context.UnitsofMeasure.Add(item);
                await _context.SaveChangesAsync();
            }

            var _GetSupplierList = _SeedData.GetSupplierList();
            foreach (var item in _GetSupplierList)
            {
                item.CreatedDate = DateTime.Now;
                item.ModifiedDate = DateTime.Now;
                item.CreatedBy = "Admin";
                item.ModifiedBy = "Admin";
                _context.Supplier.Add(item);
                await _context.SaveChangesAsync();
            }

            var _GetWarehouseList = _SeedData.GetWarehouseList();
            foreach (var item in _GetWarehouseList)
            {
                item.CreatedDate = DateTime.Now;
                item.ModifiedDate = DateTime.Now;
                item.CreatedBy = "Admin";
                item.ModifiedBy = "Admin";
                _context.Warehouse.Add(item);
                await _context.SaveChangesAsync();
            }
            var _GetVatPercentageList = _SeedData.GetVatPercentageList();
            foreach (var item in _GetVatPercentageList)
            {
                item.CreatedDate = DateTime.Now;
                item.ModifiedDate = DateTime.Now;
                item.CreatedBy = "Admin";
                item.ModifiedBy = "Admin";
                _context.VatPercentage.Add(item);
                await _context.SaveChangesAsync();
            }

            var _GetCustomerInfoList = _SeedData.GetCustomerInfoList();
            foreach (var item in _GetCustomerInfoList)
            {
                item.Phone = "+" + StaticData.RandomDigits(9);
                item.CreatedDate = DateTime.Now;
                item.ModifiedDate = DateTime.Now;
                item.CreatedBy = "Admin";
                item.ModifiedBy = "Admin";
                _context.CustomerInfo.Add(item);
                await _context.SaveChangesAsync();
            }
            var _GetCustomerTypeList = _SeedData.GetCustomerTypeList();
            foreach (var item in _GetCustomerTypeList)
            {
                item.CreatedDate = DateTime.Now;
                item.ModifiedDate = DateTime.Now;
                item.CreatedBy = "Admin";
                item.ModifiedBy = "Admin";
                _context.CustomerType.Add(item);
                await _context.SaveChangesAsync();
            }

            var _GetCurrencyList = _SeedData.GetCurrencyList();
            foreach (var item in _GetCurrencyList)
            {
                item.CreatedDate = DateTime.Now;
                item.ModifiedDate = DateTime.Now;
                item.CreatedBy = "Admin";
                item.ModifiedBy = "Admin";
                _context.Currency.Add(item);
                await _context.SaveChangesAsync();
            }

            var _GetPaymentTypeList = _SeedData.GetPaymentTypeList();
            foreach (var item in _GetPaymentTypeList)
            {
                item.CreatedDate = DateTime.Now;
                item.ModifiedDate = DateTime.Now;
                item.CreatedBy = "Admin";
                item.ModifiedBy = "Admin";
                _context.PaymentType.Add(item);
                await _context.SaveChangesAsync();
            }
            var _GetPaymentStatusList = _SeedData.GetPaymentStatusList();
            foreach (var item in _GetPaymentStatusList)
            {
                item.CreatedDate = DateTime.Now;
                item.ModifiedDate = DateTime.Now;
                item.CreatedBy = "Admin";
                item.ModifiedBy = "Admin";
                _context.PaymentStatus.Add(item);
                await _context.SaveChangesAsync();
            }
            var _GetEmailConfigList = _SeedData.GetEmailConfigList();
            foreach (var item in _GetEmailConfigList)
            {
                item.SenderFullName = "Admin: Business ERP";
                item.CreatedDate = DateTime.Now;
                item.ModifiedDate = DateTime.Now;
                item.CreatedBy = "Admin";
                item.ModifiedBy = "Admin";
                _context.EmailConfig.Add(item);
                await _context.SaveChangesAsync();
            }

            var _GetBranchList = _SeedData.GetBranchList();
            foreach (var item in _GetBranchList)
            {
                item.CreatedDate = DateTime.Now;
                item.ModifiedDate = DateTime.Now;
                item.CreatedBy = "Admin";
                item.ModifiedBy = "Admin";
                _context.Branch.Add(item);
                await _context.SaveChangesAsync();
            }

            var _GetCompanyInfo = _SeedData.GetCompanyInfo();
            _GetCompanyInfo.CreatedDate = DateTime.Now;
            _GetCompanyInfo.ModifiedDate = DateTime.Now;
            _GetCompanyInfo.CreatedBy = "Admin";
            _GetCompanyInfo.ModifiedBy = "Admin";
            _context.CompanyInfo.Add(_GetCompanyInfo);
            await _context.SaveChangesAsync();

            //Generate Demo Sales
            var result = _iCommon.GenerateDemoSales();
        }
        public async Task CreateSingleRole(string roleName)
        {
            await _roleManager.CreateAsync(new IdentityRole(roleName));
        }

        public async Task CreateItem()
        {
            try
            {
                var _GetItemList = _SeedData.GetItemList();
                foreach (var item in _GetItemList)
                {
                    item.Code = "ITM" + StaticData.RandomDigits(6);
                    item.Barcode = SampleBarcode.Default;

                    item.SupplierId = 1;
                    item.WarehouseId = 1;
                    item.StockKeepingUnit = "SKU" + StaticData.RandomDigits(6);
                    item.Note = "Your item details are noted here. Please write notes if any.";

                    item.OldUnitPrice = item.CostPrice;
                    item.NormalPrice = item.CostPrice + (item.CostPrice) * (0.05);
                    item.OldSellPrice = item.CostPrice + (item.CostPrice) * (0.05);

                    item.TradePrice = item.CostPrice + (item.CostPrice) * (0.10);
                    item.PremiumPrice = item.CostPrice + (item.CostPrice) * (0.15);
                    item.OtherPrice = item.CostPrice + (item.CostPrice) * (0.20);

                    item.CostVAT = Math.Round(0.05 * item.CostPrice, 2);
                    item.NormalVAT = Math.Round(0.05 * item.NormalPrice, 2);
                    item.TradeVAT = Math.Round(0.05 * (double)item.TradePrice, 2);
                    item.PremiumVAT = Math.Round(0.05 * (double)item.PremiumPrice, 2);
                    item.OtherVAT = Math.Round(0.05 * (double)item.OtherPrice, 2);

                    item.CreatedDate = DateTime.Now;
                    item.ModifiedDate = DateTime.Now;
                    item.CreatedBy = "Admin";
                    item.ModifiedBy = "Admin";
                    _context.Items.Add(item);
                    await _context.SaveChangesAsync();

                    ItemsCRUDViewModel _ItemsCRUDViewModel = item;
                    ItemsHistoryCRUDViewModel vm = _ItemsCRUDViewModel;
                    vm.ItemId = item.Id;
                    vm.Id = 0;
                    vm.Action = "Create New Item-" + item.Name;
                    vm.TranQuantity = 0;
                    vm.OldQuantity = item.Quantity;
                    vm.NewQuantity = item.Quantity;

                    vm.CreatedBy = "Admin";
                    vm.ModifiedBy = "Admin";
                    var result = await _iCommon.AddItemHistory(vm);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task CreateDefaultSuperAdmin()
        {
            try
            {
                await _roles.GenerateRolesFromPagesAsync();

                ApplicationUser superAdmin = new ApplicationUser();
                superAdmin.Email = _superAdminDefaultOptions.Email;
                superAdmin.UserName = superAdmin.Email;
                superAdmin.EmailConfirmed = true;

                var result = await _userManager.CreateAsync(superAdmin, _superAdminDefaultOptions.Password);

                if (result.Succeeded)
                {
                    //add to user profile
                    UserProfile profile = new UserProfile();
                    profile.ApplicationUserId = superAdmin.Id;
                    profile.PasswordHash = superAdmin.PasswordHash;
                    profile.ConfirmPassword = superAdmin.PasswordHash;

                    profile.FirstName = "Super";
                    profile.LastName = "Admin";
                    profile.PhoneNumber = "+8801674411603";
                    profile.Email = superAdmin.Email;
                    profile.Address = "R/A, Dhaka";
                    profile.Country = "Bangladesh";
                    profile.ProfilePicture = "/images/UserIcon/Admin.png";
                    profile.BranchId = 1;

                    profile.CreatedDate = DateTime.Now;
                    profile.ModifiedDate = DateTime.Now;
                    profile.CreatedBy = "Admin";
                    profile.ModifiedBy = "Admin";

                    await _context.UserProfile.AddAsync(profile);
                    await _context.SaveChangesAsync();

                    await _roles.AddToRoles(superAdmin);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task CreateDefaultOtherUser()
        {
            var _GetUserProfileList = _SeedData.GetUserProfileList();

            foreach (var item in _GetUserProfileList)
            {
                var _ApplicationUser = await _iAccount.CreateUserProfile(item, "Admin");
            }
        }
        public async Task<string> UploadFile(List<IFormFile> files, IWebHostEnvironment env, string uploadFolder)
        {
            var result = "";

            var webRoot = env.WebRootPath;
            var uploads = Path.Combine(webRoot, uploadFolder);
            var extension = "";
            var filePath = "";
            var fileName = "";


            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    extension = Path.GetExtension(formFile.FileName);
                    fileName = Guid.NewGuid().ToString() + extension;
                    filePath = Path.Combine(uploads, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    result = fileName;

                }
            }

            return result;
        }
        public async Task CreateDefaultEmailSettings()
        {
            //SMTP
            var CountSMTPEmailSetting = _context.SMTPEmailSetting.Count();
            if (CountSMTPEmailSetting < 1)
            {
                SMTPEmailSetting _SMTPEmailSetting = new SMTPEmailSetting
                {
                    UserName = "devmlbd@gmail.com",
                    Password = "",
                    Host = "smtp.gmail.com",
                    Port = 587,
                    IsSSL = true,
                    FromEmail = "devmlbd@gmail.com",
                    FromFullName = "Web Admin Notification",
                    IsDefault = true,

                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    CreatedBy = "Admin",
                    ModifiedBy = "Admin",
                };
                _context.Add(_SMTPEmailSetting);
                await _context.SaveChangesAsync();
            }
            //SendGridOptions
            var CountSendGridSetting = _context.SendGridSetting.Count();
            if (CountSendGridSetting < 1)
            {
                SendGridSetting _SendGridOptions = new SendGridSetting
                {
                    SendGridUser = "",
                    SendGridKey = "",
                    FromEmail = "devmlbd@gmail.com",
                    FromFullName = "Web Admin Notification",
                    IsDefault = false,

                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    CreatedBy = "Admin",
                    ModifiedBy = "Admin",
                };
                _context.Add(_SendGridOptions);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<SharedUIDataViewModel> GetSharedUIData(ClaimsPrincipal _ClaimsPrincipal)
        {
            SharedUIDataViewModel _SharedUIDataViewModel = new();
            ApplicationUser _ApplicationUser = await _userManager.GetUserAsync(_ClaimsPrincipal);
            _SharedUIDataViewModel.UserProfile = _context.UserProfile.SingleOrDefault(x => x.ApplicationUserId.Equals(_ApplicationUser.Id));
            _SharedUIDataViewModel.MainMenuViewModel = await _roles.RolebaseMenuLoad(_ApplicationUser);
            _SharedUIDataViewModel.ApplicationInfo = _applicationInfo;
            return _SharedUIDataViewModel;
        }
        public async Task<DefaultIdentityOptions> GetDefaultIdentitySettings()
        {
            return await _context.DefaultIdentityOptions.Where(x => x.Id == 1).SingleOrDefaultAsync();
        }
        public async Task CreateDefaultIdentitySettings()
        {
            if (_context.DefaultIdentityOptions.Count() < 1)
            {
                DefaultIdentityOptions _DefaultIdentityOptions = new DefaultIdentityOptions
                {
                    PasswordRequireDigit = false,
                    PasswordRequiredLength = 3,
                    PasswordRequireNonAlphanumeric = false,
                    PasswordRequireUppercase = false,
                    PasswordRequireLowercase = false,
                    PasswordRequiredUniqueChars = 0,
                    LockoutDefaultLockoutTimeSpanInMinutes = 30,
                    LockoutMaxFailedAccessAttempts = 5,
                    LockoutAllowedForNewUsers = false,
                    UserRequireUniqueEmail = true,
                    SignInRequireConfirmedEmail = false,

                    CookieHttpOnly = true,
                    CookieExpiration = 150,
                    CookieExpireTimeSpan = 120,
                    LoginPath = "/Account/Login",
                    LogoutPath = "/Account/Logout",
                    AccessDeniedPath = "/Account/AccessDenied",
                    SlidingExpiration = true,

                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    CreatedBy = "Admin",
                    ModifiedBy = "Admin",
                };
                _context.Add(_DefaultIdentityOptions);
                await _context.SaveChangesAsync();
            }
        }
    }
}
