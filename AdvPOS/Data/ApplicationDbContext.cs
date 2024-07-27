using AdvPOS.Models;
using AdvPOS.Models.CommonViewModel;
using AdvPOS.Models.ItemsViewModel;
using Microsoft.EntityFrameworkCore;

namespace AdvPOS.Data
{
    public class ApplicationDbContext : AuditableIdentityContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ItemGridViewModel>().HasNoKey();
            builder.Entity<ItemDropdownListViewModel>().HasNoKey();
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }

        public DbSet<SMTPEmailSetting> SMTPEmailSetting { get; set; }
        public DbSet<SendGridSetting> SendGridSetting { get; set; }
        public DbSet<DefaultIdentityOptions> DefaultIdentityOptions { get; set; }
        public DbSet<LoginHistory> LoginHistory { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }
        public DbSet<CompanyInfo> CompanyInfo { get; set; }


        //APOS
        public DbSet<Items> Items { get; set; }
        public DbSet<ItemsHistory> ItemsHistory { get; set; }
        public DbSet<DamageItemDeatils> DamageItemDeatils { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
        public DbSet<PaymentType> PaymentType { get; set; }
        public DbSet<EmailConfig> EmailConfig { get; set; }

        public DbSet<CustomerInfo> CustomerInfo { get; set; }
        public DbSet<CustomerType> CustomerType { get; set; }
        public DbSet<UnitsofMeasure> UnitsofMeasure { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<PaymentDetail> PaymentDetail { get; set; }
        public DbSet<PaymentModeHistory> PaymentModeHistory { get; set; }
        public DbSet<PaymentStatus> PaymentStatus { get; set; }
        public DbSet<VatPercentage> VatPercentage { get; set; }
        public DbSet<ItemSerialNumber> ItemSerialNumber { get; set; }


        public DbSet<UserInfoFromBrowser> UserInfoFromBrowser { get; set; }
        public DbSet<SendEmailHistory> SendEmailHistory { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<ReturnLog> ReturnLog { get; set; }


        public DbSet<ItemGridViewModel> ItemGridViewModel { get; set; }
        public DbSet<ItemDropdownListViewModel> ItemDropdownListViewModel { get; set; }
    }
}
