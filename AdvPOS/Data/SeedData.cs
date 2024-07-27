using AdvPOS.Helpers;
using AdvPOS.Models;
using AdvPOS.Models.UserAccountViewModel;

namespace AdvPOS.Data
{
    public class SeedData
    {
        public IEnumerable<Items> GetItemList()
        {

            var result = new List<Items>
            {
                new Items { Name = "Lux-soap", MeasureId = 14, MeasureValue = 1, CategoriesId = 7, CostPrice = 50, Quantity = 200, ImageURL = "/upload/DefaultItem/luxsoap.jpg"},
                new Items { Name = "Perfume", MeasureId = 14, MeasureValue = 1, CategoriesId = 7, CostPrice = 550, Quantity = 300, ImageURL = "/upload/DefaultItem/perfume.jpg"},
                new Items { Name = "7up", MeasureId = 14, MeasureValue = 1, CategoriesId = 3, CostPrice = 110, Quantity = 500, ImageURL = "/upload/DefaultItem/7up.jpg"},
                new Items { Name = "Juice", MeasureId = 14, MeasureValue = 1, CategoriesId = 1, CostPrice = 150, Quantity = 200, ImageURL = "/upload/DefaultItem/juice.jpg"},
                new Items { Name = "Mixed-fish", MeasureId = 14, MeasureValue = 1, CategoriesId = 5, CostPrice = 350, Quantity = 100, ImageURL = "/upload/DefaultItem/mixedfish.jpg"},
                new Items { Name = "Multi-plug", MeasureId = 14, MeasureValue = 1, CategoriesId = 9, CostPrice = 550, Quantity = 70, ImageURL = "/upload/DefaultItem/multiplug.jpg"},

                new Items { Name = "Apple", MeasureId = 1, MeasureValue = 1, CategoriesId = 1,  CostPrice = 190, Quantity = 200, ImageURL = "/upload/DefaultItem/apple.jpg"},
                new Items { Name = "Banana", MeasureId = 1, MeasureValue = 1, CategoriesId = 1, CostPrice = 90, Quantity = 100, ImageURL = "/upload/DefaultItem/banana.jpg"},
                new Items { Name = "Beef", MeasureId = 1, MeasureValue = 2, CategoriesId = 5, CostPrice = 1100, Quantity = 50, ImageURL = "/upload/DefaultItem/beef.jpg"},
                new Items { Name = "Chicken", MeasureId = 1, MeasureValue = 2, CategoriesId = 5, CostPrice = 600, Quantity = 50, ImageURL = "/upload/DefaultItem/chicken.jpg"},
                new Items { Name = "Cocacola", MeasureId = 1, MeasureValue = 2, CategoriesId = 3, CostPrice = 90, Quantity = 300, ImageURL = "/upload/DefaultItem/cocacola.jpg"},

                new Items { Name = "Cucumber", MeasureId = 1, MeasureValue = 1, CategoriesId = 6, CostPrice = 40, Quantity = 300, ImageURL = "/upload/DefaultItem/cucumber.jpg"},
                new Items { Name = "Dairy Milk", MeasureId = 14, MeasureValue = 1, CategoriesId = 2, CostPrice = 120, Quantity = 300, ImageURL = "/upload/DefaultItem/dairy_milk.jpg"},
                new Items { Name = "Dove", MeasureId = 14, MeasureValue = 1, CategoriesId = 7, CostPrice = 90, Quantity = 300, ImageURL = "/upload/DefaultItem/dove.jpg"},
                new Items { Name = "Egg", MeasureId = 14, MeasureValue = 12, CategoriesId = 2, CostPrice = 100, Quantity = 300, ImageURL = "/upload/DefaultItem/egg.jpg"},
                new Items { Name = "Garlic", MeasureId = 1, MeasureValue = 1, CategoriesId = 6, CostPrice = 90, Quantity = 300, ImageURL = "/upload/DefaultItem/garlic.jpg"},

                new Items { Name = "Grape", MeasureId = 1, MeasureValue = 1, CategoriesId = 1, CostPrice = 300, Quantity = 300, ImageURL = "/upload/DefaultItem/grape.jpg"},
                new Items { Name = "Mango", MeasureId = 1, MeasureValue = 5, CategoriesId = 1, CostPrice = 120, Quantity = 300, ImageURL = "/upload/DefaultItem/mango.jpg"},
                new Items { Name = "Milk", MeasureId = 1, MeasureValue = 1, CategoriesId = 2, CostPrice = 70, Quantity = 300, ImageURL = "/upload/DefaultItem/milk.jpg"},
                new Items { Name = "Onion", MeasureId = 1, MeasureValue = 5, CategoriesId = 6, CostPrice = 80, Quantity = 300, ImageURL = "/upload/DefaultItem/onion.jpg"},
                new Items { Name = "Orange", MeasureId = 1, MeasureValue = 1, CategoriesId = 1, CostPrice = 180, Quantity = 300, ImageURL = "/upload/DefaultItem/orange.jpg"},

                new Items { Name = "Potato", MeasureId = 1, MeasureValue = 1, CategoriesId = 6, CostPrice = 30, Quantity = 300, ImageURL = "/upload/DefaultItem/potato.jpg"},
                new Items { Name = "Tomatto", MeasureId = 1, MeasureValue = 1, CategoriesId = 6, CostPrice = 50, Quantity = 300, ImageURL = "/upload/DefaultItem/tomatto.jpg"},
                new Items { Name = "Mutton", MeasureId = 1, MeasureValue = 1, CategoriesId = 5, CostPrice = 650, Quantity = 500, ImageURL = "/upload/DefaultItem/mutton.jpg"},
                new Items { Name = "Mixed Nut", MeasureId = 1, MeasureValue = 1, CategoriesId = 1, CostPrice = 200, Quantity = 400, ImageURL = "/upload/DefaultItem/mixednut.jpg"},
                new Items { Name = "Rin Powder", MeasureId = 14, MeasureValue = 1, CategoriesId = 7, CostPrice = 105, Quantity = 400, ImageURL = "/upload/DefaultItem/rinpowder.jpg"},              
             };

            return result;
        }
        public IEnumerable<Categories> GetCategoriesList()
        {
            return new List<Categories>
            {
                new Categories { Name = "Fruits", Description = "Fruits Item"},
                new Categories { Name = "Dairy Products", Description = "Dairy Products"},
                new Categories { Name = "Beverages", Description = "Soft drinks, coffees, teas, etc"},
                new Categories { Name = "Freezer", Description = "Freezer"},
                new Categories { Name = "Meat & Fish", Description = "Meat & Fish"},
                new Categories { Name = "Vegetables", Description = "Vegetables"},
                 new Categories { Name = "Beauty and Cosmetic", Description = "Beauty and Cosmetic"},

                new Categories { Name = "IT", Description = "IT"},
                new Categories { Name = "Electronics", Description = "Electronics"},
                new Categories { Name = "Steels", Description = "Coated Steel Sheet"},
                new Categories { Name = "Common", Description = "For common all items"},
            };
        }

        public IEnumerable<UnitsofMeasure> GetUnitsofMeasureList()
        {
            return new List<UnitsofMeasure>
            {
                new UnitsofMeasure { Name = "Kg", Description = "Kilogram"},
                new UnitsofMeasure { Name = "gr", Description = "Milligram"},
                new UnitsofMeasure { Name = "qt", Description = "Quintal"},
                new UnitsofMeasure { Name = "t", Description = "Tonne"},
                new UnitsofMeasure { Name = "L", Description = "Liter"},
                new UnitsofMeasure { Name = "ML", Description = "Milliliter"},
                new UnitsofMeasure { Name = "bbl", Description = "Barrel"},
                new UnitsofMeasure { Name = "gl", Description = "Gallon"},
                new UnitsofMeasure { Name = "Meter", Description = "Meter"},
                new UnitsofMeasure { Name = "Centimeter", Description = "Centimeter"},
                new UnitsofMeasure { Name = "Kilometer", Description = "Kilometer"},
                new UnitsofMeasure { Name = "Foot", Description = "Foot"},
                new UnitsofMeasure { Name = "Inch", Description = "Inch"},
                new UnitsofMeasure { Name = "Piece", Description = "Piece"}
            };
        }
        public IEnumerable<Supplier> GetSupplierList()
        {
            return new List<Supplier>
            {
                new Supplier { Name = "Walk in Supplier", ContactPerson = "TBD", Email="walkin@gmail.com" ,Phone="01699000",Address="Washington" },
                new Supplier { Name = "Common Supplier", ContactPerson = "TBD", Email="dev@gmail.com" ,Phone="01699000",Address="Dhaka"},
                new Supplier { Name = "Google", ContactPerson = "TBD", Email="dev@gmail.com" ,Phone="01699000",Address="USA"},
                new Supplier { Name = "Amazon", ContactPerson = "TBD", Email="dev@gmail.com" ,Phone="01699000",Address="USA"},
                new Supplier { Name = "Microsoft", ContactPerson = "TBD", Email="dev@gmail.com" ,Phone="01699000",Address="USA"},
                new Supplier { Name = "PHP", ContactPerson = "TBD", Email="dev@gmail.com" ,Phone="01699000",Address="Dhaka"},
                new Supplier { Name = "Unilever", ContactPerson = "TBD", Email="dev@gmail.com" ,Phone="01699000",Address="Dhaka"}
            };
        }

        public IEnumerable<Warehouse> GetWarehouseList()
        {
            return new List<Warehouse>
            {
                new Warehouse { Name = "Dhaka, Bangladesh", Description = "TBD" },
                new Warehouse { Name = "Chittagone,  Bangladesh", Description = "TBD" },
                new Warehouse { Name = "California, USA", Description = "TBD" },
                new Warehouse { Name = "Berlin, Germany", Description = "TBD"  },
                new Warehouse { Name = "Paris , France", Description = "TBD" }
            };
        }
        public IEnumerable<VatPercentage> GetVatPercentageList()
        {
            return new List<VatPercentage>
            {
                new VatPercentage { Name = "VAT: 0%", Percentage = 0, IsDefault = false },
                new VatPercentage { Name = "VAT: 1%", Percentage = 1, IsDefault = false },
                new VatPercentage { Name = "VAT: 2%", Percentage = 2, IsDefault = false },
                new VatPercentage { Name = "VAT: 3%", Percentage = 3, IsDefault = false },
                new VatPercentage { Name = "VAT: 4%", Percentage = 4, IsDefault = false },

                new VatPercentage { Name = "VAT: 5%", Percentage = 5, IsDefault = true },
                new VatPercentage { Name = "VAT: 6%", Percentage = 6, IsDefault = false },
                new VatPercentage { Name = "VAT: 7%", Percentage = 7, IsDefault = false },
                new VatPercentage { Name = "VAT: 8%", Percentage = 8, IsDefault = false },
                new VatPercentage { Name = "VAT: 9%", Percentage = 9, IsDefault = false },
                new VatPercentage { Name = "VAT: 10%", Percentage = 10, IsDefault = false },

                new VatPercentage { Name = "VAT: 10%", Percentage = 10, IsDefault = false },
                new VatPercentage { Name = "VAT: 20%", Percentage = 20, IsDefault = true },
                new VatPercentage { Name = "VAT: 30%", Percentage = 30, IsDefault = false },
                new VatPercentage { Name = "VAT: 40%", Percentage = 40, IsDefault = false },
                new VatPercentage { Name = "VAT: 50%", Percentage = 50, IsDefault = false },
            };
        }

        public IEnumerable<CustomerInfo> GetCustomerInfoList()
        {
            return new List<CustomerInfo>
            {
                new CustomerInfo { Name = "Walk in Customer", Type = 1, Email = "walkincustomer@gmail.com", BillingAddress = "New York, USA" },
                new CustomerInfo { Name = "Mr. Bond", Type = 1, Email = "bond@gmail.com", BillingAddress = "Washing DC, USA" },
                new CustomerInfo { Name = "Alex Hill", Type = 1, Email = "hill@gmail.com", BillingAddress = "Lodon, UK" },
                new CustomerInfo { Name = "Games Underson", Type = 1, Email = "Underson@gmail.com", BillingAddress = "Cardif, UK" },
                new CustomerInfo { Name = "Albert Einstein", Type = 1, Email = "einstein@gmail.com", BillingAddress = "Harburg, Germany" },
                new CustomerInfo { Name = "Zahid Hasan", Type = 1, Email = "hasan@gmail.com", BillingAddress = "Dhaka, Bangladesh" },
                new CustomerInfo { Name = "Shahed", Type = 1, Email = "shahedbddev@gmail.com", BillingAddress = "Dhaka, Bangladesh" },
            };
        }
        public IEnumerable<CustomerType> GetCustomerTypeList()
        {
            return new List<CustomerType>
            {
                new CustomerType { Name = "Normal", Description = "Normal" },
                new CustomerType { Name = "Premium", Description = "Premium" },
                new CustomerType { Name = "Trader", Description = "Trader" },
                new CustomerType { Name = "Other", Description = "Other" },
            };
        }

        public IEnumerable<Currency> GetCurrencyList()
        {
            return new List<Currency>
            {
                new Currency { Name = "US Dollar", Code = "USD", Symbol = "$", Country = "United States",  Description = "US Dollar", IsDefault = false},
                new Currency { Name = "Euro", Code = "EUR", Symbol = "€", Country = "European Union",  Description = "European Union Currency", IsDefault = false},
                new Currency { Name = "Pounds Sterling", Code = "GBD", Symbol = "£", Country = "UK",  Description = "British Pounds", IsDefault = false},
                new Currency { Name = "Yen", Code = "JPY", Symbol = "¥", Country = "Japan",  Description = "Japany Yen", IsDefault = false},
                new Currency { Name = "Taka", Code = "BDT", Symbol = "৳", Country = "Bangladesh",  Description = "Bangladeshi Taka", IsDefault = true},
                new Currency { Name = "Australia Dollars", Code = "AUD", Symbol = "A$", Country = "Australia",  Description = "Australia Dollar (AUD)", IsDefault = false},
            };
        }
        public IEnumerable<PaymentType> GetPaymentTypeList()
        {
            return new List<PaymentType>
            {
                new PaymentType { Name = "Cash", Description = "Cash"},
                new PaymentType { Name = "Bank", Description = "Bank"},
                new PaymentType { Name = "POS", Description = "POS"},
                new PaymentType { Name = "Mobile-Banking", Description = "Mobile-Banking"},
                new PaymentType { Name = "Credit Card", Description = "Credit Card"},

                new PaymentType { Name = "Debit Card", Description = "Debit Card"},
                new PaymentType { Name = "Other", Description = "Other"},
            };
        }
        public IEnumerable<PaymentStatus> GetPaymentStatusList()
        {
            return new List<PaymentStatus>
            {
                new PaymentStatus { Name = "Paid", Description = "Paid"},
                new PaymentStatus { Name = "UnPaid", Description = "UnPaid"},
                new PaymentStatus { Name = "Partially Paid", Description = "Partially Paid"},
                new PaymentStatus { Name = "Deposit", Description = "Deposit"},
                new PaymentStatus { Name = "Pay within 7 Days", Description = "Pay within 7 Days"},

                new PaymentStatus { Name = "Pay within 14 Days", Description = "Pay within 14 Days"},
                new PaymentStatus { Name = "Pay within 30 Days", Description = "Pay within 30 Days"},
                new PaymentStatus { Name = "Custom Date", Description = "Custom Date"},
            };
        }

        public IEnumerable<UserProfileViewModel> GetUserProfileList()
        {
            return new List<UserProfileViewModel>
            {
                new UserProfileViewModel { FirstName = "Shop5", LastName = "User", Email = "Shop5@gmail.com", PasswordHash = "123", ConfirmPassword = "123", PhoneNumber= StaticData.RandomDigits(11), ProfilePicture = "/images/UserIcon/U1.png", Address = "California", Country = "USA", BranchId = 1 },
                new UserProfileViewModel { FirstName = "Shop4", LastName = "User", Email = "Shop4@gmail.com", PasswordHash = "123", ConfirmPassword = "123", PhoneNumber= StaticData.RandomDigits(11), ProfilePicture = "/images/UserIcon/U2.png", Address = "California", Country = "USA", BranchId = 1 },
                new UserProfileViewModel { FirstName = "Shop3", LastName = "User", Email = "Shop3@gmail.com", PasswordHash = "123", ConfirmPassword = "123", PhoneNumber= StaticData.RandomDigits(11), ProfilePicture = "/images/UserIcon/U3.png", Address = "California", Country = "USA", BranchId = 1 },
                new UserProfileViewModel { FirstName = "Shop2", LastName = "User", Email = "Shop2@gmail.com", PasswordHash = "123", ConfirmPassword = "123", PhoneNumber= StaticData.RandomDigits(11), ProfilePicture = "/images/UserIcon/U4.png", Address = "California", Country = "USA", BranchId = 1 },
                new UserProfileViewModel { FirstName = "Shop1", LastName = "User", Email = "Shop1@gmail.com", PasswordHash = "123", ConfirmPassword = "123", PhoneNumber= StaticData.RandomDigits(11), ProfilePicture = "/images/UserIcon/U5.png", Address = "California", Country = "USA", BranchId = 1 },

                new UserProfileViewModel { FirstName = "Accountants1", LastName = "User", Email = "accountants1@gmail.com", PasswordHash = "123", ConfirmPassword = "123", PhoneNumber= StaticData.RandomDigits(11), ProfilePicture = "/images/UserIcon/U6.png", Address = "California", Country = "USA", BranchId = 2 },
                new UserProfileViewModel { FirstName = "Accountants2", LastName = "User", Email = "accountants2@gmail.com", PasswordHash = "123", ConfirmPassword = "123", PhoneNumber= StaticData.RandomDigits(11), ProfilePicture = "/images/UserIcon/U7.png", Address = "California", Country = "USA", BranchId = 3 },
                new UserProfileViewModel { FirstName = "Accountants3", LastName = "User", Email = "accountants3@gmail.com", PasswordHash = "123", ConfirmPassword = "123", PhoneNumber= StaticData.RandomDigits(11), ProfilePicture = "/images/UserIcon/U8.png", Address = "California", Country = "USA", BranchId = 4 },
                new UserProfileViewModel { FirstName = "Accountants4", LastName = "User", Email = "accountants4@gmail.com", PasswordHash = "123", ConfirmPassword = "123", PhoneNumber= StaticData.RandomDigits(11), ProfilePicture = "/images/UserIcon/U9.png", Address = "California", Country = "USA", BranchId = 5 },
                new UserProfileViewModel { FirstName = "Accountants5", LastName = "User", Email = "accountants5@gmail.com", PasswordHash = "123", ConfirmPassword = "123", PhoneNumber= StaticData.RandomDigits(11), ProfilePicture = "/images/UserIcon/U10.png", Address = "California", Country = "USA", BranchId = 6 },
            };
        }
        public IEnumerable<EmailConfig> GetEmailConfigList()
        {
            return new List<EmailConfig>
            {
                new EmailConfig { Email = "devmlbd@gmail.com", Password = "", Hostname = "smtp.gmail.com", Port = 587, SSLEnabled = true, IsDefault = true },
                new EmailConfig { Email = "admin@myinvoicemanager.co.uk", Password = "A7Ga4gTRLQg9ASV@123", Hostname = "www.myinvoicemanager.co.uk", Port = 587, SSLEnabled = false, IsDefault = false },
                new EmailConfig { Email = "exmapl2@gmail.com", Password = "123", Hostname = "smtp.gmail.com", Port = 587, SSLEnabled = false, IsDefault = false },
                new EmailConfig { Email = "exmapl3@gmail.com", Password = "123", Hostname = "smtp.gmail.com", Port = 587, SSLEnabled = false, IsDefault = false },
            };
        }

        public IEnumerable<Branch> GetBranchList()
        {
            return new List<Branch>
            {
                new Branch { Name = "Main Branch", ContactPerson = "Admin", PhoneNumber = "123456789", Address = "Berlin, Germany", ShortDescription = "TBD" },
                
                new Branch { Name = "Branch One", ContactPerson = "Person 01", PhoneNumber = "123456789", Address = "Hamburg, Germany", ShortDescription = "TBD" },
                new Branch { Name = "Branch Two", ContactPerson = "Person 02", PhoneNumber = "123456789", Address = "Munich, Germany", ShortDescription = "TBD" },
                new Branch { Name = "Branch Three", ContactPerson = "Person 03", PhoneNumber = "123456789", Address = "Frankfurt, Germany", ShortDescription = "TBD" },
                new Branch { Name = "Branch Four", ContactPerson = "Person 04", PhoneNumber = "123456789", Address = "Leipzig, Germany", ShortDescription = "TBD" },
                new Branch { Name = "Branch Five", ContactPerson = "Person 05", PhoneNumber = "123456789", Address = "Parish, French", ShortDescription = "TBD" },
            };
        }

        public CompanyInfo GetCompanyInfo()
        {
            return new CompanyInfo
            {
                Name = "XYZ Company Limited",
                Logo = "/upload/company_logo.png",
                InvoiceNoPrefix = "INV",
                QuoteNoPrefix = "QTO",
                Address = "Washington DC, USA",
                City = "Washington DC",
                Country = "USA",
                Phone = "132546789",
                Fax = "123",
                Website = "www.wyx.com",

                ShopNo = "S123",
                StreetName = "ST234",
                PostCode = "P123",
                Office = "123456789",
                Mobile = "123456789",

                Password = "123456789",
                HostName = "123456789",
                TermsAndCondition = "Terms and Conditions – General Site Usage Last Revised: December 16, 2013 Welcome to www.lorem-ipsum.info. This site is provided as a service to our visitors and may be used for informational purposes only. Because the Terms and Conditions contain legal obligations, please read them carefully. 1. YOUR AGREEMENT By using this Site, you agree to be bound by, and to comply with, these Terms and Conditions. If you do not agree to these Terms and Conditions, please do not use this site. PLEASE NOTE: We reserve the right, at our sole discretion, to change, modify or otherwise alter these Terms and Conditions at any time. Unless otherwise indicated, amendments will become effective immediately. Please review these Terms and Conditions periodically. Your continued use of the Site following the posting of changes and/or modifications will constitute your acceptance of the revised Terms and Conditions and the reasonableness of these standards for notice of changes. For your information, this page was last updated as of the date at the top of these terms and conditions. 2. PRIVACY Please review our Privacy Policy, which also governs your visit to this Site, to understand our practices. 3. LINKED SITES This Site may contain links to other independent third-party Web sites ('Linked Sites'). These Linked Sites are provided solely as a convenience to our visitors. Such Linked Sites are not under our control, and we are not responsible for and does not endorse the content of such Linked Sites, including any information or materials contained on such Linked Sites. You will need to make your own independent judgment regarding your interaction with these Linked Sites.",
                TermsAndConditionItemSale = "TBD",
                CompanyNumber = "N123456",

                VatNumber = "V123456",
                CardPercentage = "C123456",
                IsVat = true,
                WhitelistIP = "182.168.0.1",
            };
        }
    }
}
