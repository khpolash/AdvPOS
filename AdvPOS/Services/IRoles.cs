using AdvPOS.Models;
using AdvPOS.Pages;
using System.Threading.Tasks;

namespace AdvPOS.Services
{
    public interface IRoles
    {
        Task GenerateRolesFromPagesAsync();
        Task AddToRoles(ApplicationUser _ApplicationUser);
        Task<MainMenuViewModel> RolebaseMenuLoad(ApplicationUser _ApplicationUser);
    }
}
