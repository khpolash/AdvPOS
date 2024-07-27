using System;
using System.Collections.Generic;

namespace AdvPOS.Models.ItemsViewModel
{
    public class ItemCartViewModel
    {
        public Int64 Id { get; set; }
        public Int64? CategoriesId { get; set; }
        public string CategoriesName { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string ImageURL { get; set; }
        public double SellPrice { get; set; }
        public double? NormalVAT { get; set; }
        public double Quantity { get; set; }
        public List<ItemCartViewModel> listItemCartViewModel { get; set; }
    }
}