using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models.Entities.ViewEntities
{
    public class LoginChangePasswordViewModel
    {
        [DisplayName("ユーザーID")]
        [Required(ErrorMessage = nameof(RequiredAttribute))]
        public string UserId { get; set; }

        [DisplayName("現在のパスワード")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = nameof(RequiredAttribute))]
        public string CurrentPassword { get; set; }

        [DisplayName("新しいパスワード")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = nameof(RequiredAttribute))]
        public string NewPassword { get; set; }

        [DisplayName("新しいパスワード（確認用）")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = nameof(RequiredAttribute))]
        public string NewPasswordConfirm { get; set; }
    }
}
