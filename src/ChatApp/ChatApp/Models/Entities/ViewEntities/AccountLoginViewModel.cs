using System.ComponentModel;

namespace ChatApp.Models.Entities.ViewEntities
{
    public class AccountLoginViewModel
    {
        [DisplayName("ID")]
        public string UserId { get; set; }

        [DisplayName("Password")]
        public string Password { get; set; }
    }
}
