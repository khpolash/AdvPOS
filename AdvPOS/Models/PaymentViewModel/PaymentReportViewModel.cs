using AdvPOS.Models.CompanyInfoViewModel;
using AdvPOS.Models.CustomerInfoViewModel;
using AdvPOS.Models.EmailConfigViewModel;
using AdvPOS.Models.PaymentDetailViewModel;
using AdvPOS.Models.PaymentModeHistoryViewModel;
using System.Collections.Generic;

namespace AdvPOS.Models.PaymentViewModel
{
    public class PaymentReportViewModel
    {
        public PaymentCRUDViewModel PaymentCRUDViewModel { get; set; }
        public List<PaymentDetailCRUDViewModel> listPaymentDetailCRUDViewModel { get; set; }
        public List<PaymentModeHistoryCRUDViewModel> listPaymentModeHistoryCRUDViewModel { get; set; }
        public CustomerInfoCRUDViewModel CustomerInfoCRUDViewModel { get; set; }
        public CompanyInfoCRUDViewModel CompanyInfoCRUDViewModel { get; set; }
        public SendEmailViewModel SendEmailViewModel { get; set; }
    }
}
