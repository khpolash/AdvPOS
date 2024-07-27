namespace AdvPOS.Pages
{
    public static class MainMenu
    {
        public static class Admin
        {
            public const string RoleName = "Admin";
        }
        public static class SuperAdmin
        {
            public const string RoleName = "Super Admin";
        }
        public static class Settings
        {
            public const string RoleName = "Settings";
        }

        public static class Dashboard
        {
            public const string PageName = "Dashboard";
            public const string RoleName = "Dashboard";
            public const string Path = "/Dashboard/Index";
            public const string ControllerName = "Dashboard";
            public const string ActionName = "Index";
        }


        public static class UserManagement
        {
            public const string PageName = "User Management";
            public const string RoleName = "User Management";
            public const string Path = "/UserManagement/Index";
            public const string ControllerName = "UserManagement";
            public const string ActionName = "Index";
        }
        public static class UserInfoFromBrowser
        {
            public const string PageName = "UserInfo From Browser";
            public const string RoleName = "UserInfo From Browser";
            public const string Path = "/UserInfoFromBrowser/Index";
            public const string ControllerName = "UserInfoFromBrowser";
            public const string ActionName = "Index";
        }
        public static class AuditLogs
        {
            public const string PageName = "Audit Logs";
            public const string RoleName = "Audit Logs";
            public const string Path = "/AuditLogs/Index";
            public const string ControllerName = "AuditLogs";
            public const string ActionName = "Index";
        }
        public static class UserProfile
        {
            public const string PageName = "User Profile";
            public const string RoleName = "User Profile";
            public const string Path = "/UserProfile/Index";
            public const string ControllerName = "UserProfile";
            public const string ActionName = "Index";
        }
        public static class ManagePageAccess
        {
            public const string PageName = "Manage Page Access";
            public const string RoleName = "Manage Page Access";
            public const string Path = "/UserRole/Index";
            public const string ControllerName = "UserRole";
            public const string ActionName = "Index";
        }
        public static class EmailSetting
        {
            public const string PageName = "Email Setting";
            public const string RoleName = "Email Setting";
            public const string Path = "/EmailSetting/Index";
            public const string ControllerName = "EmailSetting";
            public const string ActionName = "Index";
        }

        public static class IdentitySetting
        {
            public const string PageName = "Identity Setting";
            public const string RoleName = "Identity Setting";
            public const string Path = "/IdentitySetting/Index";
            public const string ControllerName = "IdentitySetting";
            public const string ActionName = "Index";
        }

        public static class LoginHistory
        {
            public const string PageName = "Login History";
            public const string RoleName = "Login History";
            public const string Path = "/LoginHistory/Index";
            public const string ControllerName = "LoginHistory";
            public const string ActionName = "Index";
        }
        public static class SendEmailHistory
        {
            public const string PageName = "Send Email History";
            public const string RoleName = "Send Email History";
            public const string Path = "/SendEmailHistory/Index";
            public const string ControllerName = "SendEmailHistory";
            public const string ActionName = "Index";
        }
        public static class RefreshToken
        {
            public const string PageName = "JWT Token";
            public const string RoleName = "Refresh Token";
            public const string Path = "/RefreshToken/Index";
            public const string ControllerName = "RefreshToken";
            public const string ActionName = "Index";
        }

        public static class CompanyInfo
        {
            public const string PageName = "Company Info";
            public const string RoleName = "Company Info";
            public const string Path = "/CompanyInfo/Index";
            public const string ControllerName = "CompanyInfo";
            public const string ActionName = "Index";
        }

        //Sales Pro
        public static class Payment
        {
            public const string PageNameRoot = "Manage Invoice";
            public const string PageName = "Invoice";
            public const string RoleName = "Payment";
            public const string Path = "/Payment/Index";
            public const string ControllerName = "Payment";
            public const string ActionName = "Index";
        }
        public static class PaymentDraft
        {
            public const string PageName = "Invoice Draft";
            public const string RoleName = "Payment Draft";
            public const string Path = "/PaymentDraft/Index";
            public const string ControllerName = "PaymentDraft";
            public const string ActionName = "Index";
        }
        public static class PaymentQuote
        {
            public const string PageName = "Invoice Quote";
            public const string RoleName = "Payment Quote";
            public const string Path = "/PaymentQuote/Index";
            public const string ControllerName = "PaymentQuote";
            public const string ActionName = "Index";
        }
        

        public static class SalesReturnLog
        {
            public const string PageName = "Sales Return Log";
            public const string RoleName = "Sales Return Log";
            public const string Path = "/ReturnLog/SalesReturnIndex";
            public const string ControllerName = "ReturnLog";
            public const string ActionName = "SalesReturnIndex";
        }

        public static class Items
        {
            public const string PageNameRoot = "Manage Item";
            public const string PageName = "Item";
            public const string RoleName = "Items";
            public const string Path = "/Items/Index";
            public const string ControllerName = "Items";
            public const string ActionName = "Index";
        }
        public static class ItemCart
        {
            public const string PageName = "Item Cart";
            public const string RoleName = "Item Cart";
            public const string Path = "/ItemCart/Index";
            public const string ControllerName = "ItemCart";
            public const string ActionName = "Index";
        }
        public static class ItemCartSideInvoice
        {
            public const string PageName = "Side Invoice";
            public const string RoleName = "Item Cart Side Invoice";
            public const string Path = "/ItemCart/ItemCartSideInvoice";
            public const string ControllerName = "ItemCart";
            public const string ActionName = "ItemCartSideInvoice";
        }
        public static class Categories
        {
            public const string PageName = "Categories";
            public const string RoleName = "Categories";
            public const string Path = "/Categories/Index";
            public const string ControllerName = "Items";
            public const string ActionName = "Index";
        }
        public static class UnitsofMeasure
        {
            public const string PageName = "Units of Measure";
            public const string RoleName = "Units of Measure";
            public const string Path = "/UnitsofMeasure/Index";
            public const string ControllerName = "UnitsofMeasure";
            public const string ActionName = "Index";
        }
        public static class OutofStock
        {
            public const string PageName = "Out of Stock";
            public const string RoleName = "Out of Stock";
            public const string Path = "/Items/OutofStockItem";
            public const string ControllerName = "Items";
            public const string ActionName = "OutofStockItem";
        }
        public static class LowInStock
        {
            public const string PageName = "Low In Stock";
            public const string RoleName = "Low In Stock";
            public const string Path = "/Items/LowInStockItem";
            public const string ControllerName = "Items";
            public const string ActionName = "LowInStockItem";
        }
        public static class DamageItemDetails
        {
            public const string PageName = "Damage Item Details";
            public const string RoleName = "Damage Item Details";
            public const string Path = "/DamageItemDetails/Index";
            public const string ControllerName = "DamageItemDetails";
            public const string ActionName = "Index";
        }
        public static class ItemsHistory
        {
            public const string PageName = "Items History";
            public const string RoleName = "Items History";
            public const string Path = "/ItemsHistory/Index";
            public const string ControllerName = "ItemsHistory";
            public const string ActionName = "Index";
        }
        public static class Branch
        {
            public const string PageName = "Branch";
            public const string RoleName = "Branch";
            public const string Path = "/Branch/Index";
            public const string ControllerName = "Branch";
            public const string ActionName = "Index";
        }


        public static class Currency
        {
            public const string PageName = "Manage Currency";
            public const string RoleName = "Currency";
            public const string Path = "/Currency/Index";
            public const string ControllerName = "Items";
            public const string ActionName = "Index";
        }
        public static class CustomerInfo
        {
            public const string PageName = "Manage Customer Info";
            public const string RoleName = "Customer Info";
            public const string Path = "/CustomerInfo/Index";
            public const string ControllerName = "CustomerInfo";
            public const string ActionName = "Index";
        }
        public static class CustomerType
        {
            public const string PageName = "Customer Type";
            public const string RoleName = "Customer Type";
            public const string Path = "/CustomerType/Index";
            public const string ControllerName = "CustomerType";
            public const string ActionName = "Index";
        }

        public static class Supplier
        {
            public const string PageName = "Manage Supplier";
            public const string RoleName = "Supplier";
            public const string Path = "/Supplier/Index";
            public const string ControllerName = "Supplier";
            public const string ActionName = "Index";
        }
        public static class Warehouse
        {
            public const string PageName = "Manage Warehouse";
            public const string RoleName = "Warehouse";
            public const string Path = "/Warehouse/Index";
            public const string ControllerName = "Warehouse";
            public const string ActionName = "Index";
        }
        public static class PaymentType
        {
            public const string PageName = "Payment Type";
            public const string RoleName = "Payment Type";
            public const string Path = "/PaymentType/Index";
            public const string ControllerName = "PaymentType";
            public const string ActionName = "Index";
        }
        public static class PaymentStatus
        {
            public const string PageName = "Payment Status";
            public const string RoleName = "Payment Status";
            public const string Path = "/PaymentStatus/Index";
            public const string ControllerName = "PaymentStatus";
            public const string ActionName = "Index";
        }
        public static class EmailConfig
        {
            public const string PageName = "Email Config";
            public const string RoleName = "Email Config";
            public const string Path = "/EmailConfig/Index";
            public const string ControllerName = "EmailConfig";
            public const string ActionName = "Index";
        }
        public static class VatPercentage
        {
            public const string PageName = "Vat Percentage";
            public const string RoleName = "Vat Percentage";
            public const string Path = "/VatPercentage/Index";
            public const string ControllerName = "VatPercentage";
            public const string ActionName = "Index";
        }

        //Sales Report
        public static class SalesReport
        {
            public const string PageName = "Sales Report";
            public const string RoleName = "Sales Report";           
        }
        public static class PaymentSummaryReport
        {
            public const string PageName = "Transaction Summary";
            public const string RoleName = "Payment Summary Report";
            public const string Path = "/SalesReport/PaymentSummaryReport";
            public const string ControllerName = "SalesReport";
            public const string ActionName = "PaymentSummaryReport";
        }
        public static class ProductWiseSale
        {
            public const string PageName = "Product Wise Sale";
            public const string RoleName = "Product Wise Sale";
            public const string Path = "/SalesReport/ProductWiseSale";
            public const string ControllerName = "SalesReport";
            public const string ActionName = "ProductWiseSale";
        }
        public static class PaymentDetailReport
        {
            public const string PageName = "Transaction Detail";
            public const string RoleName = "Payment Detail Report";
            public const string Path = "/SalesReport/PaymentDetailReport";
            public const string ControllerName = "SalesReport";
            public const string ActionName = "PaymentDetailReport";
        }
        public static class TransactionByDay
        {
            public const string PageName = "Transaction By Day";
            public const string RoleName = "Transaction By Day";
            public const string Path = "/SalesReport/TransactionByDay";
            public const string ControllerName = "SalesReport";
            public const string ActionName = "TransactionByDay";
        }
        public static class TransactionByMonth
        {
            public const string PageName = "Transaction By Month";
            public const string RoleName = "Transaction By Month";
            public const string Path = "/SalesReport/TransactionByMonth";
            public const string ControllerName = "SalesReport";
            public const string ActionName = "TransactionByMonth";
        }
        public static class TransactionByYear
        {
            public const string PageName = "Transaction By Year";
            public const string RoleName = "Transaction By Year";
            public const string Path = "/SalesReport/TransactionByYear";
            public const string ControllerName = "SalesReport";
            public const string ActionName = "TransactionByYear";
        }       

        //Item Report
        public static class ItemReport
        {
            public const string PageName = "Item Report";
            public const string RoleName = "Item Report";           
        }
        public static class HighInDemand
        {
            public const string PageName = "High In Demand";
            public const string RoleName = "High In Demand";
            public const string Path = "/SalesReport/HighInDemand";
            public const string ControllerName = "SalesReport";
            public const string ActionName = "HighInDemand";
        }
        public static class LowInDemand
        {
            public const string PageName = "Low In Demand";
            public const string RoleName = "Low In Demand";
            public const string Path = "/SalesReport/LowInDemand";
            public const string ControllerName = "SalesReport";
            public const string ActionName = "LowInDemand";
        }
        public static class HighestEarning
        {
            public const string PageName = "Highest Earning";
            public const string RoleName = "Highest Earning";
            public const string Path = "/SalesReport/HighestEarning";
            public const string ControllerName = "SalesReport";
            public const string ActionName = "HighestEarning";
        }
        public static class LowestEarning
        {
            public const string PageName = "Lowest Earning";
            public const string RoleName = "Lowest Earning";
            public const string Path = "/SalesReport/LowestEarning";
            public const string ControllerName = "SalesReport";
            public const string ActionName = "LowestEarning";
        }


        //Other Report
        public static class OtherReport
        {
            public const string PageName = "Other Report";
            public const string RoleName = "Other Report";           
        }
        public static class PrintBarcode
        {
            public const string PageName = "Print Barcode";
            public const string RoleName = "Print Barcode";
            public const string Path = "/SalesReport/PrintBarcode";
            public const string ControllerName = "SalesReport";
            public const string ActionName = "PrintBarcode";
        }
    }
}
