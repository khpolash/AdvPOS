using AdvPOS.Data;
using AdvPOS.Helpers;
using AdvPOS.Models.DashboardViewModel;
using AdvPOS.Models.PaymentDetailViewModel;
using AdvPOS.Models.PaymentModeHistoryViewModel;
using AdvPOS.Models.PaymentViewModel;
using Microsoft.EntityFrameworkCore;
using UAParser;

namespace AdvPOS.Services
{
    public class SalesService : ISalesService
    {
        private readonly ApplicationDbContext _context;
        public SalesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ManagePaymentViewModel> GetByPaymentDetail(Int64 id)
        {
            try
            {
                ManagePaymentViewModel vm = new ManagePaymentViewModel();
                vm.PaymentCRUDViewModel = await GetPaymentList().Where(x => x.Id == id).SingleOrDefaultAsync();
                vm.listPaymentDetailCRUDViewModel = await GetPaymentDetailList().Where(x => x.PaymentId == id).ToListAsync();
                vm.listPaymentModeHistoryCRUDViewModel = GetPaymentModeHistory(InvoicePaymentType.SalesInvoicePayment).Where(x => x.PaymentId == id).ToList();
                return vm;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ManagePaymentViewModel> GetByPaymentDetailInReturn(Int64 id)
        {
            try
            {
                ManagePaymentViewModel vm = new ManagePaymentViewModel();
                vm.PaymentCRUDViewModel = await GetPaymentList().Where(x => x.Id == id).SingleOrDefaultAsync();
                vm.listPaymentDetailCRUDViewModel = await GetPaymentDetailListInReturn().Where(x => x.PaymentId == id).ToListAsync();
                vm.listPaymentModeHistoryCRUDViewModel = GetPaymentModeHistory(InvoicePaymentType.SalesInvoicePayment).Where(x => x.PaymentId == id).ToList();
                return vm;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<PaymentReportViewModel> PrintPaymentInvoice(Int64 id)
        {
            PaymentReportViewModel vm = new();
            vm.PaymentCRUDViewModel = GetPaymentList().Where(x => x.Id == id).SingleOrDefault();
            vm.listPaymentDetailCRUDViewModel = await GetPaymentDetailList().Where(x => x.PaymentId == id).ToListAsync();
            vm.listPaymentModeHistoryCRUDViewModel = await GetPaymentModeHistory(InvoicePaymentType.SalesInvoicePayment).Where(x => x.PaymentId == id).ToListAsync();
            vm.CustomerInfoCRUDViewModel = _context.CustomerInfo.Where(x => x.Id == vm.PaymentCRUDViewModel.CustomerId).SingleOrDefault();
            vm.CompanyInfoCRUDViewModel = _context.CompanyInfo.FirstOrDefault(m => m.Id == 1);
            return vm;
        }
        public IQueryable<PaymentCRUDViewModel> GetPaymentGridData()
        {
            try
            {
                var result = (from _Payments in _context.Payment
                              join _CustomerInfo in _context.CustomerInfo on _Payments.CustomerId equals _CustomerInfo.Id
                              into _CustomerInfo
                              from listCustomerInfo in _CustomerInfo.DefaultIfEmpty()
                              join _PaymentStatus in _context.PaymentStatus on _Payments.PaymentStatus equals _PaymentStatus.Id
                              into _PaymentStatus
                              from listPaymentStatus in _PaymentStatus.DefaultIfEmpty()
                              where _Payments.Cancelled == false && _Payments.ReturnType != TranReturnType.FullReturn
                              select new PaymentCRUDViewModel
                              {
                                  Id = _Payments.Id,
                                  InvoiceNo = _Payments.InvoiceNo,
                                  QuoteNo = _Payments.QuoteNo,
                                  CustomerId = _Payments.CustomerId,
                                  CustomerName = listCustomerInfo.Name,

                                  SubTotal = _Payments.SubTotal,
                                  DiscountAmount = _Payments.DiscountAmount,
                                  VATAmount = _Payments.VATAmount,
                                  GrandTotal = _Payments.GrandTotal,
                                  PaidAmount = _Payments.PaidAmount,
                                  DueAmount = _Payments.DueAmount,
                                  Category = _Payments.Category,
                                  CreatedDate = _Payments.CreatedDate,
                                  PaymentStatus = _Payments.PaymentStatus,
                                  PaymentStatusDisplay = listPaymentStatus.Name,
                              }).OrderByDescending(x => x.Id);

                return result;
            }
            catch (Exception) { throw; }
        }
        public IQueryable<PaymentCRUDViewModel> GetPaymentList()
        {
            try
            {
                var result = (from _Payments in _context.Payment
                              join _CustomerInfo in _context.CustomerInfo on _Payments.CustomerId equals _CustomerInfo.Id
                              into _CustomerInfo
                              from listCustomerInfo in _CustomerInfo.DefaultIfEmpty()
                              join _PaymentStatus in _context.PaymentStatus on _Payments.PaymentStatus equals _PaymentStatus.Id
                              into _PaymentStatus
                              from listPaymentStatus in _PaymentStatus.DefaultIfEmpty()
                              join _Currency in _context.Currency on _Payments.CurrencyId equals _Currency.Id
                              into _Currency
                              from listCurrency in _Currency.DefaultIfEmpty()

                              join _Branch in _context.Branch on _Payments.BranchId equals _Branch.Id
                              into _Branch
                              from listBranch in _Branch.DefaultIfEmpty()
                              where _Payments.Cancelled == false
                              select new PaymentCRUDViewModel
                              {
                                  Id = _Payments.Id,
                                  CustomerId = _Payments.CustomerId,
                                  CustomerName = listCustomerInfo.Name,
                                  InvoiceNo = _Payments.InvoiceNo,
                                  QuoteNo = _Payments.QuoteNo,
                                  CommonCharge = _Payments.CommonCharge,
                                  Discount = _Payments.Discount,
                                  DiscountAmount = _Payments.DiscountAmount,
                                  VAT = _Payments.VAT,
                                  VATAmount = _Payments.VATAmount,
                                  SubTotal = _Payments.SubTotal,
                                  GrandTotal = _Payments.GrandTotal,
                                  PaidAmount = _Payments.PaidAmount,
                                  DueAmount = _Payments.DueAmount,
                                  CurrencyId = _Payments.CurrencyId,
                                  CurrencyName = listCurrency.Name,
                                  CurrencySymbol = listCurrency.Symbol,
                                  BranchId = _Payments.BranchId,
                                  BranchName = listBranch.Name,
                                  PaymentStatus = _Payments.PaymentStatus,
                                  PaymentStatusDisplay = listPaymentStatus.Name,
                                  Category = _Payments.Category,
                                  PurchaseOrderNumber = _Payments.PurchaseOrderNumber,
                                  CustomerNote = _Payments.CustomerNote,
                                  PrivateNote = _Payments.PrivateNote,
                                  ReturnType = _Payments.ReturnType,

                                  CreatedDate = _Payments.CreatedDate,
                                  CreatedBy = _Payments.CreatedBy,
                                  ModifiedBy = _Payments.ModifiedBy,

                              }).OrderByDescending(x => x.Id);

                return result;
            }
            catch (Exception) { throw; }
        }
        public IQueryable<PaymentDetailCRUDViewModel> GetPaymentDetailList()
        {
            try
            {
                var _PaymentDetailCRUDViewModel = (from _PaymentsDetails in _context.PaymentDetail
                                                   join _Items in _context.Items on _PaymentsDetails.ItemId equals _Items.Id
                                                   where _PaymentsDetails.Cancelled == false
                                                   select new PaymentDetailCRUDViewModel
                                                   {
                                                       Id = _PaymentsDetails.Id,
                                                       PaymentId = _PaymentsDetails.PaymentId,
                                                       ItemId = _PaymentsDetails.ItemId,
                                                       ItemCode = _Items.Code,
                                                       ItemName = _PaymentsDetails.ItemName,
                                                       Quantity = _PaymentsDetails.Quantity,
                                                       UnitPrice = _PaymentsDetails.UnitPrice,
                                                       ItemVAT = _PaymentsDetails.ItemVAT,
                                                       ItemVATAmount = _PaymentsDetails.ItemVATAmount,
                                                       ItemDiscount = _PaymentsDetails.ItemDiscount,
                                                       ItemDiscountAmount = _PaymentsDetails.ItemDiscountAmount,
                                                       TotalAmount = _PaymentsDetails.TotalAmount,
                                                       IsReturn = _PaymentsDetails.IsReturn,
                                                       IsReturnDisplay = _PaymentsDetails.IsReturn == true ? "Yes" : "No",
                                                       CreatedDate = _PaymentsDetails.CreatedDate
                                                   }).OrderByDescending(x => x.Id);

                return _PaymentDetailCRUDViewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IQueryable<PaymentDetailCRUDViewModel> GetPaymentDetailListInReturn()
        {
            try
            {
                var _PaymentDetailCRUDViewModel = (from _PaymentsDetails in _context.PaymentDetail
                                                   join _Items in _context.Items on _PaymentsDetails.ItemId equals _Items.Id
                                                   select new PaymentDetailCRUDViewModel
                                                   {
                                                       Id = _PaymentsDetails.Id,
                                                       PaymentId = _PaymentsDetails.PaymentId,
                                                       ItemId = _PaymentsDetails.ItemId,
                                                       ItemCode = _Items.Code,
                                                       ItemName = _PaymentsDetails.ItemName,
                                                       Quantity = _PaymentsDetails.Quantity,
                                                       UnitPrice = _PaymentsDetails.UnitPrice,
                                                       ItemVAT = _PaymentsDetails.ItemVAT,
                                                       ItemVATAmount = _PaymentsDetails.ItemVATAmount,
                                                       ItemDiscount = _PaymentsDetails.ItemDiscount,
                                                       ItemDiscountAmount = _PaymentsDetails.ItemDiscountAmount,
                                                       TotalAmount = _PaymentsDetails.TotalAmount,
                                                       IsReturn = _PaymentsDetails.IsReturn,
                                                       IsReturnDisplay = _PaymentsDetails.IsReturn == true ? "Yes" : "No",
                                                       CreatedDate = _PaymentsDetails.CreatedDate
                                                   }).OrderByDescending(x => x.Id);

                return _PaymentDetailCRUDViewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IQueryable<PaymentModeHistoryCRUDViewModel> GetPaymentModeHistory(int _InvoicePaymentType)
        {
            try
            {
                return (from _PaymentModeHistory in _context.PaymentModeHistory
                        where _PaymentModeHistory.Cancelled == false && _PaymentModeHistory.PaymentType == _InvoicePaymentType
                        select new PaymentModeHistoryCRUDViewModel
                        {
                            Id = _PaymentModeHistory.Id,
                            PaymentId = _PaymentModeHistory.PaymentId,
                            ModeOfPayment = _PaymentModeHistory.ModeOfPayment,
                            Amount = _PaymentModeHistory.Amount,
                            ReferenceNo = _PaymentModeHistory.ReferenceNo,
                            CreatedDate = _PaymentModeHistory.CreatedDate,
                            ModifiedDate = _PaymentModeHistory.ModifiedDate,
                            CreatedBy = _PaymentModeHistory.CreatedBy,
                            ModifiedBy = _PaymentModeHistory.ModifiedBy,
                            Cancelled = _PaymentModeHistory.Cancelled
                        }).OrderBy(x => x.CreatedDate);
            }
            catch (Exception) { throw; }
        }
        public IQueryable<PaymentGridViewModel> GetPaymentSummaryReportList()
        {
            try
            {
                var result = (from _Payment in _context.Payment
                              join _CustomerInfo in _context.CustomerInfo on _Payment.CustomerId equals _CustomerInfo.Id
                              where _Payment.Cancelled == false && _Payment.Category == InvoiceType.RegularInvoice && _Payment.ReturnType != TranReturnType.FullReturn
                              select new PaymentGridViewModel
                              {
                                  Id = _Payment.Id,
                                  CustomerName = _CustomerInfo.Name,
                                  Discount = _Payment.Discount,
                                  VAT = _Payment.VAT,
                                  SubTotal = _Payment.SubTotal,
                                  GrandTotal = _Payment.GrandTotal,
                                  PaidAmount = _Payment.PaidAmount,
                                  DueAmount = _Payment.DueAmount,
                                  BranchId = _Payment.BranchId,
                                  CreatedDate = _Payment.CreatedDate,
                              }).OrderByDescending(x => x.Id);

                return result;
            }
            catch (Exception) { throw; }
        }
        public List<TransactionByViewModel> SalesTransactionBy(string DateType)
        {
            List<TransactionByViewModel> list = new();
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            DateTime[] DateList = null;

            var _Payment = _context.Payment.Where(x => x.Cancelled == false && x.Category == InvoiceType.RegularInvoice && x.ReturnType != TranReturnType.FullReturn);
            var _PaymentDetail = _context.PaymentDetail.Where(x => x.Cancelled == false && x.IsReturn == false);

            if (DateType == "Day")
            {
                DateList = Enumerable.Range(0, 30).Select(i => DateTime.Now.Date.AddDays(-i)).ToArray();
            }
            else if (DateType == "Month")
            {
                DateTime _DateTimeMonth = DateTime.Now.AddMonths(1);
                DateList = Enumerable.Range(1, 12).Select(n => _DateTimeMonth.AddMonths(-n)).ToArray();
            }
            else if (DateType == "Year")
            {
                DateTime _DateTime = DateTime.Now.AddYears(1);
                DateList = Enumerable.Range(1, 10).Select(n => _DateTime.AddYears(-n)).ToArray();
            }


            int SL = 1;
            foreach (var item in DateList)
            {
                TransactionByViewModel vm = new();

                vm.Id = SL;
                if (DateType == "Day")
                {
                    startDate = item;
                    endDate = item.AddDays(1).AddTicks(-1); ;
                    vm.TranDate = item.ToString("dddd, dd MMMM yyyy");
                }
                else if (DateType == "Month")
                {
                    startDate = new DateTime(item.Year, item.Month, 1);
                    endDate = startDate.AddMonths(1).AddTicks(-1);
                    vm.TranDate = item.ToString("MMMM") + "-" + item.Year;
                }
                else if (DateType == "Year")
                {
                    startDate = new DateTime(item.Year, 1, 1);
                    endDate = new DateTime(item.Year, 12, 31);
                    vm.TranDate = item.Year.ToString();
                }

                vm.QuantitySold = _PaymentDetail.Where(x => x.CreatedDate >= startDate && x.CreatedDate <= endDate).Sum(x => x.Quantity);
                var SingleDayPayment = _Payment.Where(x => x.CreatedDate >= startDate && x.CreatedDate <= endDate);
                vm.TotalTran = SingleDayPayment.Count();
                vm.TotalEarned = Math.Round(SingleDayPayment.Sum(x => x.PaidAmount - x.ChangedAmount), 2);
                vm.TotalDue = Math.Round(SingleDayPayment.Sum(x => x.DueAmount), 2);

                list.Add(vm);
                SL++;
            }
            return list;
        }
        public IQueryable<ProductWiseSaleReportViewModel> GetProductWiseSaleList()
        {
            try
            {
                var _PaymentDetailCRUDViewModel = (from _PaymentsDetails in _context.PaymentDetail
                                                   join _Items in _context.Items on _PaymentsDetails.ItemId equals _Items.Id
                                                   join _Payment in _context.Payment on _PaymentsDetails.PaymentId equals _Payment.Id
                                                   join _CustomerInfo in _context.CustomerInfo on _Payment.CustomerId equals _CustomerInfo.Id
                                                   where _PaymentsDetails.Cancelled == false && _PaymentsDetails.IsReturn == false && _Payment.Category == InvoiceType.RegularInvoice
                                                   select new ProductWiseSaleReportViewModel
                                                   {
                                                       Id = _PaymentsDetails.Id,
                                                       SalesDate = _PaymentsDetails.CreatedDate,
                                                       ItemId = _PaymentsDetails.ItemId,
                                                       ItemName = _PaymentsDetails.ItemName,
                                                       InvoiceNo = _Payment.InvoiceNo,
                                                       CustomerName = _CustomerInfo.Name,
                                                       UnitPrice = _PaymentsDetails.UnitPrice,
                                                       Quantity = _PaymentsDetails.Quantity,
                                                       Discount = _PaymentsDetails.ItemDiscount,
                                                       VAT = _PaymentsDetails.ItemVAT,
                                                       Total = _PaymentsDetails.TotalAmount
                                                   }).OrderByDescending(x => x.Id);

                return _PaymentDetailCRUDViewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}