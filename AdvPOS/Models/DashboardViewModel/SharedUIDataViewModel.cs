using AdvPOS.Models.AccountViewModels;
using AdvPOS.Models.CommonViewModel;
using AdvPOS.Pages;

namespace AdvPOS.Models.DashboardViewModel
{
    public class SharedUIDataViewModel
    {
        public UserProfile UserProfile { get; set; }
        public ApplicationInfo ApplicationInfo { get; set; }
        public MainMenuViewModel MainMenuViewModel { get; set; }
        public LoginViewModel LoginViewModel { get; set; }
    }
}
