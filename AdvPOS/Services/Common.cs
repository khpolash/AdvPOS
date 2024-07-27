using AdvPOS.Data;
using AdvPOS.Helpers;
using AdvPOS.Models;
using AdvPOS.Models.CommonViewModel;
using AdvPOS.Models.CompanyInfoViewModel;
using AdvPOS.Models.CustomerInfoViewModel;
using AdvPOS.Models.DashboardViewModel;
using AdvPOS.Models.EmployeeViewModel;
using AdvPOS.Models.ExpenseSummaryViewModel;
using AdvPOS.Models.ItemsHistoryViewModel;
using AdvPOS.Models.ItemsViewModel;
using AdvPOS.Models.UserAccountViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using UAParser;

namespace AdvPOS.Services
{
    public class Common : ICommon
    {
        private readonly IWebHostEnvironment _iHostingEnvironment;
        private readonly ApplicationDbContext _context;
        public Common(IWebHostEnvironment iHostingEnvironment,
            ApplicationDbContext context)
        {
            _iHostingEnvironment = iHostingEnvironment;
            _context = context;
        }
        public string UploadedFile(IFormFile _IFormFile)
        {
            try
            {
                string FileName = null;
                if (_IFormFile != null)
                {
                    string _FileServerDir = Path.Combine(_iHostingEnvironment.ContentRootPath, "wwwroot/upload");
                    if (_FileServerDir.Contains("\n"))
                    {
                        _FileServerDir.Replace("\n", "/");
                    }

                    if (_IFormFile.FileName == null)
                        FileName = Guid.NewGuid().ToString() + "_" + "blank-person.png";
                    else
                        FileName = Guid.NewGuid().ToString() + "_" + _IFormFile.FileName;

                    string filePath = Path.Combine(_FileServerDir, FileName);
                    //using (var fileStream = new FileStream(filePath, FileMode.Create))
                    using (var fileStream = new FileStream(Path.Combine(_FileServerDir, FileName), FileMode.Create))
                    {
                        _IFormFile.CopyTo(fileStream);
                    }
                }
                return FileName;
            }
            catch (Exception ex)
            {
                Syslog.Write(Syslog.Level.Warning, "AdvPOS", ex.Message);
                throw;
            }
        }
        public async Task<SMTPEmailSetting> GetSMTPEmailSetting()
        {
            return await _context.Set<SMTPEmailSetting>().Where(x => x.Id == 1).SingleOrDefaultAsync();
        }
        public async Task<SendGridSetting> GetSendGridEmailSetting()
        {
            return await _context.Set<SendGridSetting>().Where(x => x.Id == 1).SingleOrDefaultAsync();
        }
        public async Task<EmailConfig> GetEmailConfig()
        {
            return await _context.Set<EmailConfig>().Where(x => x.IsDefault == true).SingleOrDefaultAsync();
        }

        public UserProfile GetByUserProfile(Int64 id)
        {
            var _UserProfile = _context.UserProfile.Where(x => x.UserProfileId == id).SingleOrDefault();
            return _UserProfile;
        }
        public async Task<UserProfileCRUDViewModel> GetByUserProfileInfo(Int64 id)
        {
            UserProfileCRUDViewModel _UserProfile = await _context.UserProfile.Where(x => x.UserProfileId == id).SingleOrDefaultAsync();
            var _Branch = _context.Branch.Where(x => x.Id == _UserProfile.BranchId).SingleOrDefault();
            _UserProfile.BranchName = _Branch.Name;
            return _UserProfile;
        }
        public async Task<bool> InsertLoginHistory(LoginHistory _LoginHistory, ClientInfo _ClientInfo)
        {
            try
            {
                _LoginHistory.PublicIP = await GetPublicIP();
                _LoginHistory.CreatedDate = DateTime.Now;
                _LoginHistory.ModifiedDate = DateTime.Now;

                _context.Add(_LoginHistory);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<string> GetPublicIP()
        {
            try
            {
                string url = "http://checkip.dyndns.org/";
                var _HttpClient = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });
                var _GetAsync = await _HttpClient.GetAsync(url);
                var _Stream = await _GetAsync.Content.ReadAsStreamAsync();
                StreamReader _StreamReader = new StreamReader(_Stream);
                string result = _StreamReader.ReadToEnd();

                string[] a = result.Split(':');
                string a2 = a[1].Substring(1);
                string[] a3 = a2.Split('<');
                string a4 = a3[0];
                return a4;
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw new Exception("No network adapters with an IPv4 address in the system!");
            }
        }

        public async Task<UpdateRoleViewModel> GetRoleByUser(string _ApplicationUserId, UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            UpdateRoleViewModel _UpdateRoleViewModel = new UpdateRoleViewModel();
            List<ManageUserRolesViewModel> list = new List<ManageUserRolesViewModel>();

            _UpdateRoleViewModel.ApplicationUserId = _ApplicationUserId;
            var user = await _userManager.FindByIdAsync(_ApplicationUserId);
            if (user != null)
            {
                foreach (var role in _roleManager.Roles.ToList())
                {
                    var userRolesViewModel = new ManageUserRolesViewModel
                    {
                        RoleId = role.Id,
                        RoleName = role.Name
                    };
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        userRolesViewModel.Selected = true;
                    }
                    else
                    {
                        userRolesViewModel.Selected = false;
                    }
                    list.Add(userRolesViewModel);
                }
            }

            _UpdateRoleViewModel.listManageUserRolesViewModel = list.OrderBy(x => x.RoleName).ToList();
            return _UpdateRoleViewModel;
        }
        public async Task CurrentItemsUpdate(ItemTranViewModel _ItemTranViewModel)
        {
            int _OldQuantity = 0;
            int _NewQuantity = 0;
            ItemsHistoryCRUDViewModel _ItemsHistoryCRUDViewModel = new();
            Items _Items = new();

            _Items = _context.Items.Where(or => or.Id == _ItemTranViewModel.ItemId).FirstOrDefault();

            _Items.ModifiedDate = DateTime.Now;
            _Items.ModifiedBy = _ItemTranViewModel.CurrentUserName;
            ItemsCRUDViewModel vm = _Items;
            _ItemsHistoryCRUDViewModel = vm;
            _ItemsHistoryCRUDViewModel.Action = _ItemTranViewModel.ActionMessage;

            if (_ItemTranViewModel.IsAddition)
            {
                _OldQuantity = _Items.Quantity;
                _NewQuantity = _Items.Quantity + _ItemTranViewModel.TranQuantity;
                _Items.Quantity = _Items.Quantity + _ItemTranViewModel.TranQuantity;
            }
            else
            {
                _OldQuantity = _Items.Quantity;
                _NewQuantity = _Items.Quantity - _ItemTranViewModel.TranQuantity;
                _Items.Quantity = _Items.Quantity - _ItemTranViewModel.TranQuantity;
            }

            _context.Update(_Items);
            await _context.SaveChangesAsync();

            _ItemsHistoryCRUDViewModel.TranQuantity = _ItemTranViewModel.TranQuantity;
            _ItemsHistoryCRUDViewModel.OldQuantity = _OldQuantity;
            _ItemsHistoryCRUDViewModel.NewQuantity = _NewQuantity;
            _ItemsHistoryCRUDViewModel.CreatedDate = DateTime.Now;
            _ItemsHistoryCRUDViewModel.CreatedBy = _ItemTranViewModel.CurrentUserName;
            _context.ItemsHistory.Add(_ItemsHistoryCRUDViewModel);
            await _context.SaveChangesAsync();
        }

        public IQueryable<ItemDropdownListViewModel> GetCommonddlData(string strTableName)
        {
            var sql = "select Id, Name from " + strTableName + " where Cancelled = 0";
            var result = _context.ItemDropdownListViewModel.FromSqlRaw(sql);
            return result;
        }
        public IQueryable<ItemDropdownListViewModel> LoadddlInventoryItem(bool IsVat)
        {
            var resultWithVat = (from _Items in _context.Items
                                 where _Items.Cancelled == false && _Items.Quantity > 0
                                 select new ItemDropdownListViewModel
                                 {
                                     Id = _Items.Id,
                                     Name = _Items.Name
                                     + ": Cost: " + _Items.CostPrice
                                     + ": Normal: " + _Items.NormalPrice
                                     + ": Trade: " + _Items.TradePrice
                                     + ": Premium: " + _Items.PremiumPrice
                                     + ": VAT:" + _Items.CostVAT
                                     + " : Avial: " + _Items.Quantity
                                 }).OrderByDescending(x => x.Id);

            var resultWithoutVat = (from _Items in _context.Items
                                    where _Items.Cancelled == false && _Items.Quantity > 0
                                    select new ItemDropdownListViewModel
                                    {
                                        Id = _Items.Id,
                                        Name = _Items.Name
                                        + ": Cost: " + _Items.CostPrice
                                        + ": Normal: " + _Items.NormalPrice
                                        + ": Trade: " + _Items.TradePrice
                                        + ": Premium: " + _Items.PremiumPrice
                                        + " : Avial: " + _Items.Quantity
                                    }).OrderByDescending(x => x.Id);

            if (IsVat)
                return resultWithVat;
            else
                return resultWithoutVat;
        }
        public IQueryable<ItemDropdownListViewModel> LoadddlCustomerInfo()
        {
            return (from tblObj in _context.CustomerInfo.Where(x => x.Cancelled == false && x.Id != 1).OrderByDescending(x => x.Id)
                    select new ItemDropdownListViewModel
                    {
                        Id = tblObj.Id,
                        Name = tblObj.Name + ", Cell: " + tblObj.Phone,
                    });
        }
        public IQueryable<ItemDropdownListViewModel> LoadddlPaymentType()
        {
            return (from tblObj in _context.PaymentType.Where(x => x.Cancelled == false && x.Id != 1).OrderByDescending(x => x.Id)
                    select new ItemDropdownListViewModel
                    {
                        Id = tblObj.Id,
                        Name = tblObj.Name,
                    });
        }
        public IQueryable<ItemDropdownListViewModel> LoadddlCurrencyItem()
        {
            return (from _Currency in _context.Currency.Where(x => x.Cancelled == false).OrderByDescending(x => x.IsDefault)
                    select new ItemDropdownListViewModel
                    {
                        Id = _Currency.Id,
                        Name = _Currency.Name + " <> " + _Currency.Code + " <> " + _Currency.Symbol,
                    });
        }

        public IQueryable<ItemDropdownListViewModel> LoadddlWarehouse()
        {

            return (from _Warehouse in _context.Warehouse.Where(x => x.Cancelled == false).OrderByDescending(x => x.Id)
                    select new ItemDropdownListViewModel
                    {
                        Id = _Warehouse.Id,
                        Name = _Warehouse.Name,
                    });
        }
        public IQueryable<ItemDropdownListViewModel> LoadddlSupplier()
        {

            return (from _Supplier in _context.Supplier.Where(x => x.Cancelled == false).OrderByDescending(x => x.Id)
                    select new ItemDropdownListViewModel
                    {
                        Id = _Supplier.Id,
                        Name = _Supplier.Name,
                    });
        }
        public IQueryable<ItemDropdownListViewModel> LoadddlCategories()
        {
            var result = (from tblObj in _context.Categories.Where(x => x.Cancelled == false).OrderBy(x => x.Id)
                          select new ItemDropdownListViewModel
                          {
                              Id = tblObj.Id,
                              Name = tblObj.Name,
                          });
            return result;
        }


        public async Task<ItemsHistoryCRUDViewModel> AddItemHistory(ItemsHistoryCRUDViewModel vm)
        {
            try
            {
                ItemsHistory _ItemsHistory = new ItemsHistory();
                _ItemsHistory = vm;
                _ItemsHistory.CreatedDate = DateTime.Now;
                _ItemsHistory.ModifiedDate = DateTime.Now;
                _ItemsHistory.CreatedBy = vm.UserName;
                _ItemsHistory.ModifiedBy = vm.UserName;
                _context.Add(_ItemsHistory);
                await _context.SaveChangesAsync();
                return _ItemsHistory;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ItemsCRUDViewModel GetViewItemById(Int64 Id)
        {
            try
            {
                return (from _Items in _context.Items
                        join _Categories in _context.Categories on _Items.CategoriesId equals _Categories.Id
                        into _Categories
                        from listCategories in _Categories.DefaultIfEmpty()
                        join _Supplier in _context.Supplier on _Items.SupplierId equals _Supplier.Id
                        into _Supplier
                        from listSupplier in _Supplier.DefaultIfEmpty()
                        join _Warehouse in _context.Warehouse on _Items.WarehouseId equals _Warehouse.Id
                        into _Warehouse
                        from listWarehouse in _Warehouse.DefaultIfEmpty()
                        join _UnitsofMeasure in _context.UnitsofMeasure on _Items.MeasureId equals _UnitsofMeasure.Id
                        into _UnitsofMeasure
                        from listUnitsofMeasure in _UnitsofMeasure.DefaultIfEmpty()

                        where _Items.Cancelled == false && _Items.Id == Id
                        select new ItemsCRUDViewModel
                        {
                            Id = _Items.Id,
                            Code = _Items.Code,
                            Name = _Items.Name,
                            CostPrice = _Items.CostPrice,
                            NormalPrice = _Items.NormalPrice,
                            OldUnitPrice = _Items.OldUnitPrice,
                            OldSellPrice = _Items.OldSellPrice,
                            Quantity = _Items.Quantity,
                            CategoriesId = _Items.CategoriesId,
                            CategoriesDisplay = listCategories.Name,
                            WarehouseId = _Items.WarehouseId,
                            WarehouseDisplay = listWarehouse.Name,
                            SupplierId = _Items.SupplierId,
                            SupplierDisplay = listSupplier.Name,
                            MeasureId = _Items.MeasureId,
                            MeasureDisplay = listUnitsofMeasure.Name,
                            MeasureValue = _Items.MeasureValue,

                            Note = _Items.Note,
                            UpdateQntType = _Items.UpdateQntType,
                            UpdateQntNote = _Items.UpdateQntNote,
                            StockKeepingUnit = _Items.StockKeepingUnit,
                            ManufactureDate = _Items.ManufactureDate,
                            ExpirationDate = _Items.ExpirationDate,
                            Barcode = _Items.Barcode,
                            ImageURL = _Items.ImageURL,

                            CreatedDate = _Items.CreatedDate,
                            ModifiedDate = _Items.ModifiedDate,
                            CreatedBy = _Items.CreatedBy,
                            ModifiedBy = _Items.ModifiedBy,
                            Cancelled = _Items.Cancelled
                        }).OrderByDescending(x => x.Id).SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IQueryable<ItemCartViewModel> GetAllCartItem()
        {
            try
            {
                var result = (from _Item in _context.Items
                              where _Item.Cancelled == false && _Item.Quantity > 0
                              select new ItemCartViewModel
                              {
                                  Id = _Item.Id,
                                  Name = _Item.Name.Length < 20 ? _Item.Name : _Item.Name.Substring(0, 20) + "..",
                                  ImageURL = _Item.ImageURL,
                                  SellPrice = _Item.NormalPrice,
                                  NormalVAT = _Item.NormalVAT,
                                  Quantity = _Item.Quantity,
                              }).OrderByDescending(x => x.Id);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IQueryable<List<ItemCartViewModel>> GetAllCartItemForCustomDT()
        {
            try
            {
                var result = (from _Item in _context.Items
                              where _Item.Cancelled == false && _Item.Quantity > 0
                              select new ItemCartViewModel
                              {
                                  Id = _Item.Id,
                                  Name = _Item.Name.Length < 20 ? _Item.Name : _Item.Name.Substring(0, 20) + "..",
                                  Barcode = _Item.Code,
                                  ImageURL = _Item.ImageURL,
                                  SellPrice = _Item.NormalPrice,
                                  Quantity = _Item.Quantity,
                              }).OrderByDescending(x => x.Id).ToList();

                var _listBarcodeViewModel = result.Select((x, i) => new { Index = i, Value = x })
                    .GroupBy(x => x.Index / 4)
                    .Select(x => x.Select(v => v.Value).ToList()).ToList();

                var lastItem = _listBarcodeViewModel.LastOrDefault();
                if (lastItem.Count > 0)
                {
                    _listBarcodeViewModel.Remove(lastItem);
                    ItemCartViewModel _ItemCartViewModel = new();
                    _ItemCartViewModel.Name = "Empty";
                    _ItemCartViewModel.Barcode = "Empty";
                    _ItemCartViewModel.ImageURL = "/upload/blank-item.png";
                    if (lastItem.Count < 2)
                    {
                        lastItem.Add(_ItemCartViewModel);
                        lastItem.Add(_ItemCartViewModel);
                        lastItem.Add(_ItemCartViewModel);
                    }
                    else if (lastItem.Count < 3)
                    {
                        lastItem.Add(_ItemCartViewModel);
                        lastItem.Add(_ItemCartViewModel);
                    }
                    else if (lastItem.Count < 4)
                    {
                        lastItem.Add(_ItemCartViewModel);
                    }
                    _listBarcodeViewModel.Add(lastItem);
                }

                return _listBarcodeViewModel.AsQueryable();
            }
            catch (Exception) { throw; }
        }

        public IQueryable<List<ItemCartViewModel>> GetItemCartDataList()
        {
            try
            {
                var result = (from _Item in _context.Items
                              join _Categories in _context.Categories on _Item.CategoriesId equals _Categories.Id
                              into _Categories
                              from listCategories in _Categories.DefaultIfEmpty()
                              where _Item.Cancelled == false && _Item.Quantity > 0
                              select new ItemCartViewModel
                              {
                                  Id = _Item.Id,
                                  CategoriesId = _Item.CategoriesId,
                                  CategoriesName = listCategories.Name == null ? "Common" : listCategories.Name,
                                  //Name = _Item.Name,
                                  Name = _Item.Name.Length < 20 ? _Item.Name : _Item.Name.Substring(0, 20) + "..",
                                  Barcode = _Item.Code,
                                  ImageURL = _Item.ImageURL,
                                  SellPrice = _Item.NormalPrice,
                                  Quantity = _Item.Quantity,
                              }).OrderByDescending(x => x.Id).ToList();

                var _listBarcodeViewModel = result.Select((x, i) => new { Index = i, Value = x })
                    .GroupBy(x => x.Index / 3)
                    .Select(x => x.Select(v => v.Value).ToList()).ToList();

                var lastItem = _listBarcodeViewModel.LastOrDefault();
                if (lastItem.Count > 0)
                {
                    _listBarcodeViewModel.Remove(lastItem);
                    ItemCartViewModel _ItemCartViewModel = new();
                    _ItemCartViewModel.Name = "Empty";
                    _ItemCartViewModel.Barcode = "Empty";
                    _ItemCartViewModel.CategoriesName = "Empty";
                    _ItemCartViewModel.ImageURL = "/upload/blank-item.png";
                    if (lastItem.Count < 2)
                    {
                        lastItem.Add(_ItemCartViewModel);
                        lastItem.Add(_ItemCartViewModel);
                    }
                    else if (lastItem.Count < 3)
                    {
                        lastItem.Add(_ItemCartViewModel);
                    }
                    _listBarcodeViewModel.Add(lastItem);
                }
                return _listBarcodeViewModel.AsQueryable();
            }
            catch (Exception) { throw; }
        }
        public IQueryable<ItemGridViewModel> GetJoinDataItemsAndTranDetails()
        {
            try
            {
                var result = _context.ItemGridViewModel.FromSqlRaw(@"select A.Id,A.Code, A.Name, C.Name SupplierName, D.Name MeasureName, A.StockKeepingUnit, A.CostPrice,A.NormalPrice,A.Quantity,B.TranQuantity TotalSold,
                (B.TranQuantity)*(A.CostPrice) TotalEarned,  
                A.CreatedDate,A.ImageURL from Items A left join (SELECT ItemId, sum(Quantity)TranQuantity 
                FROM PaymentDetail GROUP BY ItemId) B ON A.Id = B.ItemId 
                full join Supplier C on A.SupplierId=c.Id
                full join UnitsofMeasure D on A.MeasureId=D.Id
                where A.Cancelled=0");
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GenerateDemoSales()
        {
            try
            {
                string _FileServerDir = Path.Combine(_iHostingEnvironment.ContentRootPath, "wwwroot/upload/SQLScript/");
                var sqlPayment = System.IO.File.ReadAllText(_FileServerDir + "tblPayment.sql");
                var result1 = _context.Database.ExecuteSqlRaw(sqlPayment);

                var sqlPaymentDetail = System.IO.File.ReadAllText(_FileServerDir + "tblPaymentDetail.sql");
                var result2 = _context.Database.ExecuteSqlRaw(sqlPaymentDetail);

                var sqlcommon = System.IO.File.ReadAllText(_FileServerDir + "tblcommon.sql");
                var result3 = _context.Database.ExecuteSqlRaw(sqlcommon);

                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
        }
        public IQueryable<ItemsCRUDViewModel> GetItemsGridList()
        {
            try
            {
                var result = (from _Items in _context.Items
                              join _Categories in _context.Categories on _Items.CategoriesId equals _Categories.Id
                              into _Categories
                              from listCategories in _Categories.DefaultIfEmpty()
                              join _Supplier in _context.Supplier on _Items.SupplierId equals _Supplier.Id
                              into _Supplier
                              from listSupplier in _Supplier.DefaultIfEmpty()
                              join _UnitsofMeasure in _context.UnitsofMeasure on _Items.MeasureId equals _UnitsofMeasure.Id
                              into _UnitsofMeasure
                              from listUnitsofMeasure in _UnitsofMeasure.DefaultIfEmpty()
                              where _Items.Cancelled == false
                              select new ItemsCRUDViewModel
                              {
                                  Id = _Items.Id,
                                  ImageURL = _Items.ImageURL,
                                  Name = _Items.Name,
                                  Code = _Items.Code,
                                  StockKeepingUnit = _Items.StockKeepingUnit,
                                  CategoriesDisplay = listCategories.Name,
                                  SupplierDisplay = listSupplier.Name,
                                  MeasureDisplay = listUnitsofMeasure.Name,
                                  CostPrice = _Items.CostPrice,
                                  NormalPrice = _Items.NormalPrice,
                                  Quantity = _Items.Quantity,
                                  CreatedDate = _Items.CreatedDate,
                              }).OrderByDescending(x => x.Id);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<GroupByViewModel> GetItemDemandList()
        {
            var PaymentDetailGroupBy = _context.PaymentDetail.Where(x => x.Cancelled == false).GroupBy(p => p.ItemId).Select(g => new
            {
                ItemId = g.Key,
                TranQuantity = g.Sum(t => t.Quantity),
            }).ToList();

            var result = (from _PaymentDetail in PaymentDetailGroupBy
                          join _Items in _context.Items on _PaymentDetail.ItemId equals _Items.Id
                          where _PaymentDetail.ItemId == _Items.Id
                          select new GroupByViewModel
                          {
                              ItemName = _Items.Id + "-" + _Items.Name,
                              ItemTotal = _PaymentDetail.TranQuantity
                          }).ToList();

            return result;
        }
        public List<GroupByViewModel> GetItemEarningList()
        {
            var PaymentDetailGroupBy = _context.PaymentDetail.Where(x => x.Cancelled == false).GroupBy(p => p.ItemId).Select(g => new
            {
                ItemId = g.Key,
                TranQuantity = g.Sum(t => t.Quantity)
            }).ToList();

            var result = (from _TranDetails in PaymentDetailGroupBy
                          join _Items in _context.Items
                          on _TranDetails.ItemId equals _Items.Id
                          where (_TranDetails.ItemId == _Items.Id)
                          select new GroupByViewModel
                          {
                              ItemName = _Items.Id + "-" + _Items.Name,
                              ItemTotal = _TranDetails.TranQuantity * _Items.NormalPrice,
                          }).ToList();

            return result;
        }
        public IQueryable<BarcodeViewModel> GetBarcodeList()
        {
            var result = (from _Items in _context.Items
                          where _Items.Cancelled == false
                          select new BarcodeViewModel
                          {
                              Id = _Items.Id,
                              ItemName = _Items.Name,
                              Barcode = _Items.Barcode,
                          }).OrderBy(x => x.Id);
            return result;
        }
        public CompanyInfoCRUDViewModel GetCompanyInfo()
        {
            CompanyInfoCRUDViewModel vm = _context.CompanyInfo.FirstOrDefault(m => m.Id == 1);
            return vm;
        }
        public IQueryable<ItemDropdownListViewModel> GetddlEmailConfig()
        {
            return (from tblObj in _context.EmailConfig.Where(x => x.Cancelled == false)
                    select new ItemDropdownListViewModel
                    {
                        Id = tblObj.Id,
                        Name = tblObj.Email
                    }).OrderBy(x => x.Id);
        }
        public IQueryable<ItemDropdownListViewModel> GetddlCustomerEmail()
        {
            return (from tblObj in _context.CustomerInfo.Where(x => x.Cancelled == false)
                    select new ItemDropdownListViewModel
                    {
                        Id = tblObj.Id,
                        Name = tblObj.Email
                    }).OrderByDescending(x => x.Id);
        }
        public IQueryable<ItemDropdownListViewModel> GetddlCustomerType()
        {
            return (from tblObj in _context.CustomerType.Where(x => x.Cancelled == false)
                    select new ItemDropdownListViewModel
                    {
                        Id = tblObj.Id,
                        Name = tblObj.Name
                    }).OrderByDescending(x => x.Id);
        }
        public IQueryable<ItemDropdownListViewModel> GetddlPaymentStatus()
        {
            return (from tblObj in _context.PaymentStatus.Where(x => x.Cancelled == false)
                    select new ItemDropdownListViewModel
                    {
                        Id = tblObj.Id,
                        Name = tblObj.Name
                    }).OrderByDescending(x => x.Id);
        }
        public IQueryable<CustomerInfoCRUDViewModel> GetCustomerList()
        {
            try
            {
                var result = (from _CustomerInfo in _context.CustomerInfo
                              join _CustomerType in _context.CustomerType on _CustomerInfo.Type equals _CustomerType.Id
                              into _CustomerType
                              from listCustomerType in _CustomerType.DefaultIfEmpty()
                              where _CustomerInfo.Cancelled == false
                              select new CustomerInfoCRUDViewModel
                              {
                                  Id = _CustomerInfo.Id,
                                  Name = _CustomerInfo.Name,
                                  CompanyName = _CustomerInfo.CompanyName,
                                  Type = _CustomerInfo.Type,
                                  TypeDisplay = listCustomerType.Name,
                                  Phone = _CustomerInfo.Phone,
                                  Email = _CustomerInfo.Email,
                                  AccountNo = _CustomerInfo.AccountNo,
                                  Notes = _CustomerInfo.Notes,
                                  Address = _CustomerInfo.Address,
                                  BillingAddress = _CustomerInfo.BillingAddress,
                                  CreatedDate = _CustomerInfo.CreatedDate,
                                  ModifiedDate = _CustomerInfo.ModifiedDate,
                                  CreatedBy = _CustomerInfo.CreatedBy,
                                  ModifiedBy = _CustomerInfo.ModifiedBy,
                              }).OrderByDescending(x => x.Id);

                return result;
            }
            catch (Exception) { throw; }
        }
        public async Task<Int64> GetBranchIdByUserName(string _UserName)
        {
            try
            {
                var _UserProfile = await _context.UserProfile.FirstOrDefaultAsync(x => x.Email == _UserName);

                if (_UserProfile != null)
                    return _UserProfile.BranchId;
                else
                    return 1;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}