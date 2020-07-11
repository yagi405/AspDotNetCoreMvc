using System.ComponentModel;

namespace ChatApp.Models.Entities.ViewEntities
{
    public class UserEditViewModel
    {
        [DisplayName("ユーザーID")]
        public string UserId { get; set; }
        [DisplayName("名前")]
        public string Name { get; set; }
        [DisplayName("管理者")]
        public bool IsAdministrator { get; set; }
    }
}
