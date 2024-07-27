using System;
using System.Collections.Generic;

namespace AdvPOS.Models.ItemsViewModel
{
    public class BarcodeViewModel
    {
        public Int64 Id { get; set; }
        public string ItemName { get; set; }
        public string Barcode { get; set; }
        public List<BarcodeViewModel> listBarcodeViewModel { get; set; }
    }
}
