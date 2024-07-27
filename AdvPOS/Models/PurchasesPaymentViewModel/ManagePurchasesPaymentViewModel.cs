using AdvPOS.Models.CustomerInfoViewModel;
using AdvPOS.Models.PaymentModeHistoryViewModel;
using AdvPOS.Models.PurchasesPaymentDetailViewModel;
using AdvPOS.Models.SupplierViewModel;
using System.Collections.Generic;

namespace AdvPOS.Models.PurchasesPaymentViewModel
{
    public class ManagePurchasesPaymentViewModel
    {
        public PurchasesPaymentCRUDViewModel PurchasesPaymentCRUDViewModel { get; set; }
        public PurchasesPaymentDetailCRUDViewModel PurchasesPaymentDetailCRUDViewModel { get; set; }
        public List<PurchasesPaymentDetailCRUDViewModel> listPurchasesPaymentDetailCRUDViewModel { get; set; }
        public List<PaymentModeHistoryCRUDViewModel> listPaymentModeHistoryCRUDViewModel { get; set; }
        public SupplierCRUDViewModel SupplierCRUDViewModel { get; set; }
    }
}
