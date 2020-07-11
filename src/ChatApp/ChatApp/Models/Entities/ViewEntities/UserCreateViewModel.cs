using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models.Entities.ViewEntities
{
    public class UserCreateViewModel
    {
        [DisplayName("ユーザーID")]
        [Required(ErrorMessage = nameof(RequiredAttribute))]
        public string UserId { get; set; }

        [DisplayName("ユーザー名")]
        [Required(ErrorMessage = nameof(RequiredAttribute))]
        public string Name { get; set; }

        [DisplayName("管理者")]
        public bool IsAdministrator { get; set; }
    }
}
