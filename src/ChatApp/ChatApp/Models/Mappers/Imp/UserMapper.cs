using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Models.Entities;
using ChatApp.Models.Entities.DbEntities;
using ChatApp.Models.Entities.ViewEntities;

namespace ChatApp.Models.Mappers.Imp
{
    public class UserMapper : IUserMapper
    {
        public UserIndexViewModel FromUserToIndexViewModel(IList<User> users)
        {
            var details = users
                .Select(x => new UserIndexViewModel.Detail()
                {
                    UserId = x.UserId,
                    Name = x.UserName,
                    IsAdministrator = x.IsAdministrator,
                })
                .ToList();

            return new UserIndexViewModel()
            {
                Details = details
            };
        }

        public UserEditViewModel FromUserToEditViewModel(User user)
        {
            return new UserEditViewModel()
            {
                UserId = user.UserId,
                Name = user.UserName,
                IsAdministrator = user.IsAdministrator,
            };
        }

        public UserDeleteViewModel FromUserToDeleteViewModel(User user)
        {
            return new UserDeleteViewModel()
            {
                UserId = user.UserId,
                Name = user.UserName,
                IsAdministrator = user.IsAdministrator,
            };
        }

        public User FromCreateViewModelToUser(UserCreateViewModel model)
        {
            return new User(
                model.UserId,
                model.Name,
                PasswordType.PlainText,
                null,
                model.UserId,
                model.IsAdministrator
            );
        }
    }
}
