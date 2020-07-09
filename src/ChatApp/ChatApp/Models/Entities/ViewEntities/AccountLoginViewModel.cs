using System.ComponentModel;

namespace ChatApp.Models.Entities.ViewEntities
{
    public class AccountLoginViewModel
    {
        [DisplayName("User Name")]
        public string UserName { get; set; }
    }
}
