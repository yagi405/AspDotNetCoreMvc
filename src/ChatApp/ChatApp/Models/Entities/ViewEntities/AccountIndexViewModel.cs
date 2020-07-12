using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models.Entities.ViewEntities
{
    public class AccountIndexViewModel
    {
        [DisplayName("名前")]
        [Required(ErrorMessage = nameof(RequiredAttribute))]
        public string Name { get; set; }
    }
}
