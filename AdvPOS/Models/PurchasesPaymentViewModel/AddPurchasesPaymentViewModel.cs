using System;

namespace AdvPOS.Models.PurchasesPaymentViewModel
{
    public class AddPurchasesPaymentViewModel
    {
        public Int64 CustomerId { get; set; }
        public Int64 PaymentsId { get; set; }
        public Int64 MedicineId { get; set; }
        public Int64 LabTestsId { get; set; }
        public int PaymentCategoriesId { get; set; }
        public int WhentoTakeDayCount { get; set; }
        public int NoofDays { get; set; }
        public Int64 ItemDetailId { get; set; }
    }
}
