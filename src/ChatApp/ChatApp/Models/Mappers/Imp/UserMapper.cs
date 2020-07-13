using System.Collections.Generic;
using System.Linq;
using ChatApp.Models.Entities;
using ChatApp.Models.Entities.DbEntities;
using ChatApp.Models.Entities.ViewEntities;
using ChatApp.Models.Util;

namespace ChatApp.Models.Mappers.Imp
{
    public class UserMapper : IUserMapper
    {
        public UserIndexViewModel FromUserToIndexViewModel(IList<User> users)
        {
            Args.NotNull(users, nameof(users));

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
            Args.NotNull(user, nameof(user));

            return new UserEditViewModel()
            {
                UserId = user.UserId,
                Name = user.UserName,
                IsAdministrator = user.IsAdministrator,
            };
        }

        public UserDeleteViewModel FromUserToDeleteViewModel(User user)
        {
            Args.NotNull(user, nameof(user));

            return new UserDeleteViewModel()
            {
                UserId = user.UserId,
                Name = user.UserName,
                IsAdministrator = user.IsAdministrator,
            };
        }

        public User FromCreateViewModelToUser(UserCreateViewModel model)
        {
            Args.NotNull(model, nameof(model));

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
