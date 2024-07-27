using AdvPOS.Services;

namespace AdvPOS.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context, IFunctional functional)
        {
            context.Database.EnsureCreated();
            await functional.GetDefaultIdentitySettings();
            //await functional.CreateSingleRole("Test");

            if (context.ApplicationUser.Any())
            {
                return;
            }
            else
            {
                await functional.CreateDefaultSuperAdmin();
                await functional.CreateDefaultOtherUser();
                await functional.CreateDefaultEmailSettings();
                await functional.CreateDefaultIdentitySettings();
                await functional.CreateItem();
                await functional.InitAppData();
            }
        }
    }
}
