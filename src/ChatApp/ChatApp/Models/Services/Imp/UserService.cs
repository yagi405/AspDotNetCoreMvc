using System.Linq;
using ChatApp.Common;
using ChatApp.Infrastructure.Persistence.Entities;
using ChatApp.Infrastructure.Persistence.Repositories;
using ChatApp.Models.ViewModels;

namespace ChatApp.Models.Services.Imp
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string GetUserId(string userId)
        {
            Args.NotEmpty(userId, nameof(userId));
            return _userRepository.GetById(userId)?.UserId;
        }

        public UserIndexViewModel GetIndexViewModel()
        {
            var users = _userRepository.GetAll();
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

        public void Create(UserCreateViewModel model)
        {
            Args.NotNull(model, nameof(model));

            var user = new User(
                model.UserId,
                model.Name,
                PasswordType.PlainText,
                null,
                model.UserId,
                model.IsAdministrator
            );

            _userRepository.Create(user);
        }

        public UserEditViewModel GetNewEditViewModel(string userId)
        {
            Args.NotEmpty(userId, nameof(userId));

            var user = _userRepository.GetById(userId);
            return new UserEditViewModel()
            {
                UserId = user.UserId,
                Name = user.UserName,
                IsAdministrator = user.IsAdministrator,
            };
        }

        public void Edit(UserEditViewModel model)
        {
            Args.NotNull(model, nameof(model));
            _userRepository.Edit(model.UserId, model.Name, model.IsAdministrator);
        }

        public UserDeleteViewModel GetNewDeleteViewModel(string userId)
        {
            Args.NotEmpty(userId, nameof(userId));

            var user = _userRepository.GetById(userId);
            return new UserDeleteViewModel()
            {
                UserId = user.UserId,
                Name = user.UserName,
                IsAdministrator = user.IsAdministrator,
            };
        }

        public void Delete(UserDeleteViewModel model)
        {
            Args.NotNull(model, nameof(model));
            _userRepository.Delete(model.UserId);
        }
    }
}