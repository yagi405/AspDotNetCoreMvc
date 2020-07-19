using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models.ViewModels
{
    public class AccountIndexViewModel
    {
        [DisplayName("名前")]
        [Required(ErrorMessage = nameof(RequiredAttribute))]
        public string Name { get; set; }
    }
}
