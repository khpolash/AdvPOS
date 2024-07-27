using AdvPOS.Data;
using AdvPOS.Helpers;
using AdvPOS.Models;
using AdvPOS.Models.ItemsViewModel;
using AdvPOS.Models.PaymentDetailViewModel;
using AdvPOS.Models.PaymentModeHistoryViewModel;
using AdvPOS.Models.PaymentViewModel;
using AdvPOS.Models.ReturnLogViewModel;
using AdvPOS.Models.SendEmailHistoryViewModel;
using AdvPOS.Services;

namespace AdvPOS.ConHelper
{
    public class DBOperation : IDBOperation
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommon _iCommon;
        public DBOperation(ApplicationDbContext context, ICommon iCommon)
        {
            _context = context;
            _iCommon = iCommon;
        }

        public async Task<Payment> CreatePayment(AddPaymentViewModel _AddPaymentViewModel, string strUserName)
        {
            var _CurrencyId = _context.Currency.Where(x => x.IsDefault == true).SingleOrDefault().Id;
            Payment _Payments = new Payment();
            _Payments.CustomerId = _AddPaymentViewModel.CustomerId;
            _Payments.CurrencyId = _CurrencyId;

            _Payments.CreatedDate = DateTime.Now;
            _Payments.ModifiedDate = DateTime.Now;
            _Payments.CreatedBy = strUserName;
            _Payments.ModifiedBy = strUserName;
            _context.Add(_Payments);
            await _context.SaveChangesAsync();
            return _Payments;
        }
        public async Task<PaymentCRUDViewModel> UpdatePayment(PaymentCRUDViewModel vm, bool IsCustomerInfo)
        {
            var _Payment = await _context.Payment.FindAsync(vm.Id);
            var _PaymentDetail = _context.PaymentDetail.Where(x => x.PaymentId == vm.Id && x.Cancelled == false).ToList();
            var _PaymentModeHistory = _context.PaymentModeHistory.Where(x => x.PaymentId == vm.Id && x.Cancelled == false).ToList();

            var _ItemDiscountAmount = (double)_PaymentDetail.Sum(x => x.ItemDiscountAmount);
            var _ItemVATAmount = (double)_PaymentDetail.Sum(x => x.ItemVATAmount);
            var _TotalAmount = (double)_PaymentDetail.Sum(x => x.TotalAmount);

            vm.DiscountAmount = Math.Round(_ItemDiscountAmount, 2);
            vm.VATAmount = Math.Round(_ItemVATAmount, 2);

            vm.SubTotal = Math.Round(((_TotalAmount + _ItemDiscountAmount) - _ItemVATAmount), 2);
            vm.GrandTotal = Math.Round((double)(_TotalAmount + vm.CommonCharge), 2);

            vm.PaidAmount = Math.Round((double)_PaymentModeHistory.Sum(x => x.Amount), 2);
            if (vm.PaidAmount >= vm.GrandTotal)
            {
                vm.DueAmount = 0;
                vm.PaymentStatus = PaymentStatusInfo.Paid;
                vm.ChangedAmount = Math.Round((double)(vm.PaidAmount - vm.GrandTotal), 2);
            }
            else
            {
                vm.DueAmount = Math.Round((double)(vm.GrandTotal - vm.PaidAmount), 2);
                vm.ChangedAmount = 0;
            }

            vm.ModifiedDate = DateTime.Now;
            vm.ModifiedBy = vm.UserName;
            _context.Entry(_Payment).CurrentValues.SetValues(vm);
            await _context.SaveChangesAsync();

            if (IsCustomerInfo)
            {
                var _CustomerInfoInfo = await _context.CustomerInfo.FindAsync(vm.CustomerId);
                _CustomerInfoInfo.Notes = vm.CustomerNote;
                _CustomerInfoInfo.CreatedDate = _CustomerInfoInfo.CreatedDate;
                _CustomerInfoInfo.CreatedBy = _CustomerInfoInfo.CreatedBy;
                _CustomerInfoInfo.ModifiedDate = DateTime.Now;
                _CustomerInfoInfo.ModifiedBy = vm.UserName;
                _context.Update(_CustomerInfoInfo);
                await _context.SaveChangesAsync();
            }

            return vm;
        }
        public async Task<PaymentDetail> CreatePaymentsDetail(PaymentDetail _PaymentDetail, string UserName)
        {
            var _Total = _PaymentDetail.Quantity * _PaymentDetail.UnitPrice;
            var _ItemDiscountAmount = (_PaymentDetail.ItemDiscount / 100) * _Total;
            var WithoutDiscountTotal = _Total - _ItemDiscountAmount;
            var _ItemVATAmount = (_PaymentDetail.ItemVAT / 100) * WithoutDiscountTotal;


            _PaymentDetail.ItemVATAmount = Math.Round((double)_ItemVATAmount, 2);
            _PaymentDetail.ItemDiscountAmount = Math.Round((double)_ItemDiscountAmount, 2);
            _PaymentDetail.TotalAmount = Math.Round((double)(WithoutDiscountTotal + _ItemVATAmount), 2);

            _PaymentDetail.CreatedDate = DateTime.Now;
            _PaymentDetail.ModifiedDate = DateTime.Now;
            _PaymentDetail.CreatedBy = UserName;
            _PaymentDetail.ModifiedBy = UserName;
            _context.Add(_PaymentDetail);
            var result = await _context.SaveChangesAsync();
            return _PaymentDetail;
        }
        public async Task<PaymentDetail> UpdatePaymentDetail(PaymentDetailCRUDViewModel vm)
        {
            var _PaymentDetail = await _context.PaymentDetail.FindAsync(vm.Id);
            double CurrentQuantity = _PaymentDetail.Quantity;

            var _Total = vm.Quantity * vm.UnitPrice;
            var _ItemDiscountAmount = (vm.ItemDiscount / 100) * _Total;
            var WithoutDiscountTotal = _Total - _ItemDiscountAmount;
            var _ItemVATAmount = (vm.ItemVAT / 100) * WithoutDiscountTotal;
            _PaymentDetail.ItemVATAmount = Math.Round((double)_ItemVATAmount, 2);
            _PaymentDetail.ItemDiscountAmount = Math.Round((double)_ItemDiscountAmount, 2);
            _PaymentDetail.TotalAmount = Math.Round((double)(WithoutDiscountTotal + _ItemVATAmount), 2);

            _PaymentDetail.ItemName = vm.ItemName;
            _PaymentDetail.Quantity = vm.Quantity;
            _PaymentDetail.UnitPrice = vm.UnitPrice;
            _PaymentDetail.ItemDiscount = vm.ItemDiscount;
            _PaymentDetail.ModifiedDate = DateTime.Now;
            _PaymentDetail.ModifiedBy = vm.UserName;

            _context.Update(_PaymentDetail);
            await _context.SaveChangesAsync();

            if (CurrentQuantity != vm.Quantity)
            {
                ItemTranViewModel _ItemTranViewModel = new();
                if (CurrentQuantity < vm.Quantity)
                {
                    _ItemTranViewModel.TranQuantity = (int)(vm.Quantity - CurrentQuantity);
                    _ItemTranViewModel.IsAddition = false;
                    _ItemTranViewModel.ActionMessage = "Update Sell Items by addition. Payment Detail Id: " + _PaymentDetail.Id;
                }
                else
                {
                    _ItemTranViewModel.TranQuantity = (int)(CurrentQuantity - vm.Quantity);
                    _ItemTranViewModel.IsAddition = true;
                    _ItemTranViewModel.ActionMessage = "Update Sell Items by return. Payment Detail Id: " + _PaymentDetail.Id;
                }

                _ItemTranViewModel.ItemId = _PaymentDetail.ItemId;
                _ItemTranViewModel.CurrentUserName = vm.UserName;
                await _iCommon.CurrentItemsUpdate(_ItemTranViewModel);
            }

            return _PaymentDetail;
        }
        public async Task<PaymentModeHistory> CreatePaymentModeHistory(PaymentModeHistoryCRUDViewModel vm)
        {
            try
            {
                PaymentModeHistory _PaymentModeHistory = new();
                _PaymentModeHistory = vm;
                _PaymentModeHistory.CreatedDate = DateTime.Now;
                _PaymentModeHistory.ModifiedDate = DateTime.Now;
                _context.Add(_PaymentModeHistory);
                await _context.SaveChangesAsync();
                return _PaymentModeHistory;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<PaymentModeHistory> UpdatePaymentModeHistory(PaymentModeHistoryCRUDViewModel vm)
        {
            try
            {
                var _PaymentModeHistory = await _context.PaymentModeHistory.FindAsync(vm.Id);
                _PaymentModeHistory.ModifiedDate = DateTime.Now;
                _context.Entry(_PaymentModeHistory).CurrentValues.SetValues(vm);
                await _context.SaveChangesAsync();
                return _PaymentModeHistory;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GetInvoiceNo(int _Category)
        {
            string _InvoiceNo = null;
            var _Payment = _context.Payment.Where(x => x.Cancelled == false && x.Category == _Category);
            var _InvoiceNoPrefix = _context.CompanyInfo.Where(x => x.Id == 1).SingleOrDefault().InvoiceNoPrefix;
            if (_Category == InvoiceType.DraftInvoice)
            {
                _InvoiceNoPrefix = "D" + _InvoiceNoPrefix;
            }

            var _PaymentCount = _Payment.Count();
            if (_PaymentCount == 0)
            {
                _InvoiceNo = _InvoiceNoPrefix + 1;
            }
            else
            {
                _InvoiceNo = _InvoiceNoPrefix + (_PaymentCount + 1);
            }

            return _InvoiceNo;
        }
        public string GetQuoteNo(int _Category)
        {
            string _InvoiceNo = null;
            var _Payment = _context.Payment.Where(x => x.Cancelled == false && x.Category == _Category);
            var _QuoteNoPrefix = _context.CompanyInfo.Where(x => x.Id == 1).SingleOrDefault().QuoteNoPrefix;

            var _PaymentCount = _Payment.Count();
            if (_PaymentCount < 1)
            {
                _InvoiceNo = _QuoteNoPrefix + 1;
            }
            else
            {
                _InvoiceNo = _QuoteNoPrefix + (_PaymentCount + 1);
            }

            return _InvoiceNo;
        }

        public CustomerHistoryViewModel GetCustomerHistory(Int64 _CustomerId)
        {
            CustomerHistoryViewModel _CustomerHistoryViewModel = new();

            var _Payment = _context.Payment.Where(x => x.CustomerId == _CustomerId);
            var _PaymentCount = _Payment.Count();
            if (_PaymentCount < 1)
            {
                _CustomerHistoryViewModel.PrevousBalance = 0;
            }
            else
            {
                _CustomerHistoryViewModel.PrevousBalance = _Payment.Sum(x => x.DueAmount);
            }

            _CustomerHistoryViewModel.CustomerNote = _context.CustomerInfo.Where(x => x.Id == _CustomerId).SingleOrDefault().Notes;
            return _CustomerHistoryViewModel;
        }
        public async Task<Payment> CreateDraftInvoice(PaymentCRUDViewModel vm)
        {
            try
            {
                Payment _Payment = new();
                _Payment = vm;
                _Payment.QuoteNo = null;
                _Payment.GrandTotal = 0;
                _Payment.Category = InvoiceType.DraftInvoice;
                _Payment.CustomerId = 1;
                _Payment.CreatedDate = DateTime.Now;
                _Payment.ModifiedDate = DateTime.Now;
                _Payment.CreatedBy = vm.UserName;
                _Payment.ModifiedBy = vm.UserName;
                _context.Add(_Payment);
                await _context.SaveChangesAsync();
                return _Payment;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<SendEmailHistoryCRUDViewModel> AddSendEmailHistory(SendEmailHistoryCRUDViewModel vm)
        {
            SendEmailHistory _SendEmailHistory = new();
            _SendEmailHistory = vm;
            _SendEmailHistory.CreatedDate = DateTime.Now;
            _SendEmailHistory.ModifiedDate = DateTime.Now;
            _SendEmailHistory.CreatedBy = vm.UserName;
            _SendEmailHistory.ModifiedBy = vm.UserName;
            _context.Add(_SendEmailHistory);
            var result = await _context.SaveChangesAsync();
            vm = _SendEmailHistory;
            return vm;
        }

        public async Task<ReturnLogCRUDViewModel> AddReturnLog(ReturnLogCRUDViewModel vm)
        {
            try
            {
                ReturnLog _ReturnLog = new();
                _ReturnLog = vm;
                _ReturnLog.CreatedDate = DateTime.Now;
                _ReturnLog.ModifiedDate = DateTime.Now;
                _ReturnLog.CreatedBy = vm.UserName;
                _ReturnLog.ModifiedBy = vm.UserName;
                _context.Add(_ReturnLog);
                var result = await _context.SaveChangesAsync();
                vm = _ReturnLog;
                return vm;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
