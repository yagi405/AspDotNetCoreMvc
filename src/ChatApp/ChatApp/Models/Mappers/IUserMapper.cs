using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Models.Entities.DbEntities;
using ChatApp.Models.Entities.ViewEntities;

namespace ChatApp.Models.Mappers
{
    public interface IUserMapper
    {
        UserIndexViewModel FromUserToIndexViewModel(IList<User> users);

        UserEditViewModel FromUserToEditViewModel(User user);

        UserDeleteViewModel FromUserToDeleteViewModel(User user);

        User FromCreateViewModelToUser(UserCreateViewModel model);
    }
}
