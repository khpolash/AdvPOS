namespace AdvPOS.Pages
{
    public class MainMenuViewModel
    {
        public bool Admin { get; set; }
        public bool SuperAdmin { get; set; }
        public bool Settings { get; set; }


        public bool Dashboard { get; set; }
        public bool UserManagement { get; set; }
        public bool UserInfoFromBrowser { get; set; }
        public bool AuditLogs { get; set; }
        public bool UserProfile { get; set; }
        public bool ManagePageAccess { get; set; }
        public bool EmailSetting { get; set; }
        public bool IdentitySetting { get; set; }
        public bool LoginHistory { get; set; }
        public bool SendEmailHistory { get; set; }
        public bool RefreshToken { get; set; }
        public bool CompanyInfo { get; set; }


        //Sales Pro
        public bool Items { get; set; }
        public bool ItemCart { get; set; }
        public bool ItemCartSideInvoice { get; set; }
        public bool Categories { get; set; }
        public bool UnitsofMeasure { get; set; }
        public bool OutofStock { get; set; }
        public bool LowInStock { get; set; }
        public bool DamageItemDetails { get; set; }
        public bool ItemsHistory { get; set; }


        public bool Branch { get; set; }


        public bool Currency { get; set; }
        public bool CustomerInfo { get; set; }
        public bool CustomerType { get; set; }
        public bool Supplier { get; set; }
        public bool Warehouse { get; set; }

        public bool Payment { get; set; }
        public bool PaymentDraft { get; set; }
        public bool PaymentQuote { get; set; }
        public bool SalesReturnLog { get; set; }


        //Sales Report
        public bool SalesReport { get; set; }
        public bool ProductWiseSale { get; set; }
        public bool PaymentSummaryReport { get; set; }
        public bool PaymentDetailReport { get; set; }
        public bool TransactionByDay { get; set; }
        public bool TransactionByMonth { get; set; }
        public bool TransactionByYear { get; set; }


        //Item Report
        public bool ItemReport { get; set; }
        public bool HighInDemand { get; set; }
        public bool LowInDemand { get; set; }
        public bool HighestEarning { get; set; }
        public bool LowestEarning { get; set; }

        //Other Report
        public bool OtherReport { get; set; }
        public bool PrintBarcode { get; set; }


        public bool PaymentType { get; set; }
        public bool PaymentStatus { get; set; }
        public bool EmailConfig { get; set; }
        public bool VatPercentage { get; set; }
    }
}