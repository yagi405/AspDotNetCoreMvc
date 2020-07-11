using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Models.Entities.ViewEntities
{
    public class UserEditViewModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public bool IsAdministrator { get; set; }
    }
}
