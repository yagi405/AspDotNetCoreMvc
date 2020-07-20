using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models.ViewModels
{
    public class AccountChangeIconViewModel
    {
        [DisplayName("新しいアイコン")]
        [Required(ErrorMessage = nameof(RequiredAttribute))]
        public IFormFile Icon { get; set; }
    }
}
