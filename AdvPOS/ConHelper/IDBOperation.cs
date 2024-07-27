using AdvPOS.Models;
using AdvPOS.Models.ExpenseSummaryViewModel;
using AdvPOS.Models.PaymentDetailViewModel;
using AdvPOS.Models.PaymentModeHistoryViewModel;
using AdvPOS.Models.PaymentViewModel;
using AdvPOS.Models.PurchasesPaymentDetailViewModel;
using AdvPOS.Models.PurchasesPaymentViewModel;
using AdvPOS.Models.ReturnLogViewModel;
using AdvPOS.Models.SendEmailHistoryViewModel;
using System;
using System.Threading.Tasks;

namespace AdvPOS.ConHelper
{
    public interface IDBOperation
    {
        Task<Payment> CreatePayment(AddPaymentViewModel _AddPaymentViewModel, string strUserName);
        Task<PaymentDetail> CreatePaymentsDetail(PaymentDetail _PaymentDetail, string UserName);
        Task<PaymentDetail> UpdatePaymentDetail(PaymentDetailCRUDViewModel vm);
        Task<PaymentModeHistory> CreatePaymentModeHistory(PaymentModeHistoryCRUDViewModel vm);
        string GetInvoiceNo(int _Category);
        string GetQuoteNo(int _Category);
        CustomerHistoryViewModel GetCustomerHistory(Int64 _CustomerId);
        Task<PaymentCRUDViewModel> UpdatePayment(PaymentCRUDViewModel vm, bool IsCustomerInfo);
        Task<Payment> CreateDraftInvoice(PaymentCRUDViewModel vm);
        Task<SendEmailHistoryCRUDViewModel> AddSendEmailHistory(SendEmailHistoryCRUDViewModel vm);
        Task<ReturnLogCRUDViewModel> AddReturnLog(ReturnLogCRUDViewModel vm);
    }
}
