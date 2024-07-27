using AdvPOS.Models.ItemsViewModel;
using System;

namespace AdvPOS.Models.ItemsHistoryViewModel
{
    public class ItemsHistoryCRUDViewModel : EntityBase
    {
        public Int64 Id { get; set; }
        public Int64 ItemId { get; set; }
        public string ItemName { get; set; }
        public double CostPrice { get; set; }
        public double NormalPrice { get; set; }
        public double OldUnitPrice { get; set; }
        public double OldSellPrice { get; set; }
        public int OldQuantity { get; set; }
        public int NewQuantity { get; set; }
        public int TranQuantity { get; set; }
        public string Action { get; set; }
        public string UserName { get; set; }

        public static implicit operator ItemsHistoryCRUDViewModel(ItemsHistory _ItemsHistory)
        {
            return new ItemsHistoryCRUDViewModel
            {
                Id = _ItemsHistory.Id,
                ItemId = _ItemsHistory.ItemId,
                CostPrice = _ItemsHistory.CostPrice,
                NormalPrice = _ItemsHistory.NormalPrice,
                OldUnitPrice = _ItemsHistory.OldUnitPrice,
                OldSellPrice = _ItemsHistory.OldSellPrice,
                OldQuantity = _ItemsHistory.OldQuantity,
                NewQuantity = _ItemsHistory.NewQuantity,
                TranQuantity = _ItemsHistory.TranQuantity,
                Action = _ItemsHistory.Action,

                CreatedDate = _ItemsHistory.CreatedDate,
                ModifiedDate = _ItemsHistory.ModifiedDate,
                CreatedBy = _ItemsHistory.CreatedBy,
                ModifiedBy = _ItemsHistory.ModifiedBy,
                Cancelled = _ItemsHistory.Cancelled
            };
        }

        public static implicit operator ItemsHistory(ItemsHistoryCRUDViewModel vm)
        {
            return new ItemsHistory
            {
                Id = vm.Id,
                ItemId = vm.ItemId,
                CostPrice = vm.CostPrice,
                NormalPrice = vm.NormalPrice,
                OldUnitPrice = vm.OldUnitPrice,
                OldSellPrice = vm.OldSellPrice,
                OldQuantity = vm.OldQuantity,
                NewQuantity = vm.NewQuantity,
                TranQuantity = vm.TranQuantity,
                Action = vm.Action,

                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled
            };
        }

        public static implicit operator ItemsHistoryCRUDViewModel(ItemsCRUDViewModel _ItemsCRUDViewModel)
        {
            return new ItemsHistoryCRUDViewModel
            {
                ItemId = _ItemsCRUDViewModel.Id,
                CostPrice = _ItemsCRUDViewModel.CostPrice,
                NormalPrice = _ItemsCRUDViewModel.NormalPrice,
                OldUnitPrice = _ItemsCRUDViewModel.OldUnitPrice,
                OldSellPrice = _ItemsCRUDViewModel.OldSellPrice,
                OldQuantity = _ItemsCRUDViewModel.Quantity,
                NewQuantity = _ItemsCRUDViewModel.NewQuantity,

                CreatedDate = _ItemsCRUDViewModel.CreatedDate,
                ModifiedDate = _ItemsCRUDViewModel.ModifiedDate,
                CreatedBy = _ItemsCRUDViewModel.CreatedBy,
                ModifiedBy = _ItemsCRUDViewModel.ModifiedBy,
                Cancelled = _ItemsCRUDViewModel.Cancelled
            };
        }

        public static implicit operator Items(ItemsHistoryCRUDViewModel vm)
        {
            return new ItemsCRUDViewModel
            {
                Id = vm.ItemId,
                CostPrice = vm.CostPrice,
                NormalPrice = vm.NormalPrice,
                OldUnitPrice = vm.OldUnitPrice,
                OldSellPrice = vm.OldSellPrice,
                Quantity = vm.OldQuantity,
                NewQuantity = vm.NewQuantity,

                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled
            };
        }
    }
}
