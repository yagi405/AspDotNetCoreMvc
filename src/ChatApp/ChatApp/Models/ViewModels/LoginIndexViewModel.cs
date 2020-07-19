using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models.ViewModels
{
    public class LoginIndexViewModel
    {
        [DisplayName("ユーザーID")]
        [Required(ErrorMessage = nameof(RequiredAttribute))]
        public string UserId { get; set; }

        [DisplayName("パスワード")]
        [Required(ErrorMessage = nameof(RequiredAttribute))]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
