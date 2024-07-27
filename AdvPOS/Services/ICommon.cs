using AdvPOS.Models;
using AdvPOS.Models.CommonViewModel;
using AdvPOS.Models.CustomerInfoViewModel;
using AdvPOS.Models.DashboardViewModel;
using AdvPOS.Models.ItemsHistoryViewModel;
using AdvPOS.Models.ItemsViewModel;
using AdvPOS.Models.UserAccountViewModel;
using Microsoft.AspNetCore.Identity;
using UAParser;

namespace AdvPOS.Services
{
    public interface ICommon
    {
        string UploadedFile(IFormFile ProfilePicture);
        Task<SMTPEmailSetting> GetSMTPEmailSetting();
        Task<SendGridSetting> GetSendGridEmailSetting();
        Task<EmailConfig> GetEmailConfig();
        UserProfile GetByUserProfile(Int64 id);
        Task<UserProfileCRUDViewModel> GetByUserProfileInfo(Int64 id);
        Task<bool> InsertLoginHistory(LoginHistory _LoginHistory, ClientInfo _ClientInfo);
        Task<UpdateRoleViewModel> GetRoleByUser(string _ApplicationUserId, UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager);
        Task CurrentItemsUpdate(ItemTranViewModel _ItemTranViewModel);
        IQueryable<ItemDropdownListViewModel> GetCommonddlData(string strTableName);
        IQueryable<ItemDropdownListViewModel> LoadddlInventoryItem(bool IsVat);
        IQueryable<ItemDropdownListViewModel> LoadddlCustomerInfo();
        IQueryable<ItemDropdownListViewModel> LoadddlPaymentType();
        IQueryable<ItemDropdownListViewModel> LoadddlCurrencyItem();
        IQueryable<ItemDropdownListViewModel> LoadddlCategories();


        Task<ItemsHistoryCRUDViewModel> AddItemHistory(ItemsHistoryCRUDViewModel vm);
        ItemsCRUDViewModel GetViewItemById(Int64 Id);
        IQueryable<ItemCartViewModel> GetAllCartItem();
        IQueryable<List<ItemCartViewModel>> GetAllCartItemForCustomDT();
        IQueryable<List<ItemCartViewModel>> GetItemCartDataList();
        string GenerateDemoSales();
        List<GroupByViewModel> GetItemDemandList();
        List<GroupByViewModel> GetItemEarningList();
        IQueryable<BarcodeViewModel> GetBarcodeList();
        IQueryable<ItemsCRUDViewModel> GetItemsGridList();
        IQueryable<ItemDropdownListViewModel> GetddlEmailConfig();
        IQueryable<ItemDropdownListViewModel> GetddlCustomerEmail();
        IQueryable<ItemDropdownListViewModel> GetddlCustomerType();
        IQueryable<ItemDropdownListViewModel> GetddlPaymentStatus();
        IQueryable<CustomerInfoCRUDViewModel> GetCustomerList();
        Task<Int64> GetBranchIdByUserName(string _UserName);
    }
}
