using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models.Entities.ViewEntities
{
    public class UserEditViewModel
    {
        [DisplayName("ユーザーID")]
        public string UserId { get; set; }

        [DisplayName("名前")]
        [Required(ErrorMessage = nameof(RequiredAttribute))]
        public string Name { get; set; }
        
        [DisplayName("管理者")]
        public bool IsAdministrator { get; set; }
    }
}
