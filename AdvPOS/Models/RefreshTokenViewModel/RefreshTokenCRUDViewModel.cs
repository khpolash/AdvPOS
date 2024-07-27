using System;
using System.ComponentModel.DataAnnotations;


namespace AdvPOS.Models.RefreshTokenViewModel
{
    public class RefreshTokenCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public string JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevorked { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Int64 UserProfileId { get; set; }
        public string UserEmail { get; set; }

        public static implicit operator RefreshTokenCRUDViewModel(RefreshToken _RefreshToken)
        {
            return new RefreshTokenCRUDViewModel
            {
                Id = _RefreshToken.Id,
                UserId = _RefreshToken.UserId,
                Token = _RefreshToken.Token,
                JwtId = _RefreshToken.JwtId,
                IsUsed = _RefreshToken.IsUsed,
                IsRevorked = _RefreshToken.IsRevorked,
                AddedDate = _RefreshToken.AddedDate,
                ExpiryDate = _RefreshToken.ExpiryDate,
                CreatedDate = _RefreshToken.CreatedDate,
                ModifiedDate = _RefreshToken.ModifiedDate,
                CreatedBy = _RefreshToken.CreatedBy,
                ModifiedBy = _RefreshToken.ModifiedBy,
                Cancelled = _RefreshToken.Cancelled,
            };
        }

        public static implicit operator RefreshToken(RefreshTokenCRUDViewModel vm)
        {
            return new RefreshToken
            {
                Id = vm.Id,
                UserId = vm.UserId,
                Token = vm.Token,
                JwtId = vm.JwtId,
                IsUsed = vm.IsUsed,
                IsRevorked = vm.IsRevorked,
                AddedDate = vm.AddedDate,
                ExpiryDate = vm.ExpiryDate,
                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled,
            };
        }
    }
}
