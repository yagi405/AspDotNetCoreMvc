using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models.Entities.ViewEntities
{
    public class LoginIndexViewModel
    {
        [DisplayName("ID")]
        [Required]
        public string UserId { get; set; }

        [DisplayName("Password")]
        [Required]
        public string Password { get; set; }
    }
}
