using AdvPOS.Models.PaymentDetailViewModel;
using AdvPOS.Models.PaymentModeHistoryViewModel;
using AdvPOS.Models.PaymentViewModel;
using AdvPOS.Models.DashboardViewModel;

namespace AdvPOS.Services
{
    public interface ISalesService
    {
        Task<ManagePaymentViewModel> GetByPaymentDetail(Int64 id);
        Task<ManagePaymentViewModel> GetByPaymentDetailInReturn(Int64 id);
        Task<PaymentReportViewModel> PrintPaymentInvoice(Int64 id);
        IQueryable<PaymentCRUDViewModel> GetPaymentGridData();
        IQueryable<PaymentCRUDViewModel> GetPaymentList();
        IQueryable<PaymentDetailCRUDViewModel> GetPaymentDetailList();
        IQueryable<PaymentModeHistoryCRUDViewModel> GetPaymentModeHistory(int _InvoicePaymentType);
        IQueryable<PaymentGridViewModel> GetPaymentSummaryReportList();
        List<TransactionByViewModel> SalesTransactionBy(string DateType);
        IQueryable<ProductWiseSaleReportViewModel> GetProductWiseSaleList();
    }
}