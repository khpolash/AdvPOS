using AdvPOS.Models.CompanyInfoViewModel;
using AdvPOS.Models.CustomerInfoViewModel;
using AdvPOS.Models.EmailConfigViewModel;
using AdvPOS.Models.PaymentModeHistoryViewModel;
using AdvPOS.Models.PurchasesPaymentDetailViewModel;
using System.Collections.Generic;

namespace AdvPOS.Models.PurchasesPaymentViewModel
{
    public class PurchasesPaymentReportViewModel
    {
        public PurchasesPaymentCRUDViewModel PurchasesPaymentCRUDViewModel { get; set; }
        public List<PurchasesPaymentDetailCRUDViewModel> listPurchasesPaymentDetailCRUDViewModel { get; set; }
        public List<PaymentModeHistoryCRUDViewModel> listPaymentModeHistoryCRUDViewModel { get; set; }
        public CustomerInfoCRUDViewModel CustomerInfoCRUDViewModel { get; set; }
        public CompanyInfoCRUDViewModel CompanyInfoCRUDViewModel { get; set; }
        public SendEmailViewModel SendEmailViewModel { get; set; }
    }
}
