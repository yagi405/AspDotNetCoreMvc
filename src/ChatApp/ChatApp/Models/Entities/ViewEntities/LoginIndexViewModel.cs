using System.ComponentModel;

namespace ChatApp.Models.Entities.ViewEntities
{
    public class LoginIndexViewModel
    {
        [DisplayName("ID")]
        public string UserId { get; set; }

        [DisplayName("Password")]
        public string Password { get; set; }
    }
}
