using System.ComponentModel;

namespace ChatApp.Models.ViewModels
{
    public class UserDeleteViewModel
    {
        [DisplayName("ユーザーID")]
        public string UserId { get; set; }

        [DisplayName("名前")]
        public string Name { get; set; }
        
        [DisplayName("管理者")]
        public bool IsAdministrator { get; set; }
    }
}
